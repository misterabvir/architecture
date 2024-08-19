using Booking.Application.Common.Repositories;
using Booking.Application.Domain;
using Dapper;

namespace Booking.Infrastructure.Persistence.Repositories;

public class BookingRepository(DbConnectionFactory factory) : IBookingRepository
{
    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
    {
        const string sql = """
            SELECT 
            r.id AS RestaurantId,
            r.name AS Name,
            r.address AS Address,
            r.phone AS Phone
        FROM restaurants r;
        """;

        using var connection = await factory.CreateConnection();
        return await connection.QueryAsync<Restaurant>(sql: sql);
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId)
    {
        const string sql = """
            SELECT 
            r.id AS RestaurantId,
            r.name AS Name,
            r.address AS Address,
            r.phone AS Phone,
            t.id AS TableId,
            t.restaurant_id AS RestaurantId,
            t.capacity AS Capacity,
            t.table_number AS TableNumber,
            t.capacity AS Capacity,
            t.location AS Location
        FROM restaurants r
        LEFT JOIN tables t ON r.id = t.restaurant_id
        WHERE r.id = @Id;
        """;

        using var connection = await factory.CreateConnection();


        Restaurant? currentRestaurant = null;

        await connection.QueryAsync<Restaurant, Table, Restaurant>(
            sql: sql,
            map: (restaurant, table) =>
            {
                if (currentRestaurant is null)
                {
                    currentRestaurant = restaurant;
                }
                var currentTable = currentRestaurant.Tables.FirstOrDefault(t => t.TableId == table.TableId);
                if (currentTable is null)
                {
                    currentTable = table;
                    currentRestaurant.Tables.Add(table);
                }
                return restaurant;
            },
            splitOn: "TableId",
            param: new { Id = restaurantId });

        return currentRestaurant;
    }


    public async Task<Table?> GetTableByIdAsync(int tableId)
    {
        const string sql = """
            SELECT
            t.id As TableId,
            t.restaurant_id AS RestaurantId,
            t.capacity AS Capacity,
            t.table_number AS TableNumber,
            t.capacity AS Capacity,
            t.location AS Location,
            (SELECT name FROM restaurants WHERE id = t.restaurant_id) AS RestaurantName
        FROM tables t
        WHERE t.id = @TableId;
        """;

        using var connection = await factory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Table>(sql: sql, param: new { TableId = tableId });
    }

    public async Task<bool> IsExistsReservation(DateOnly reservationDate, int tableId)
    {
        const string sql = """
            SELECT
                EXISTS(
                    SELECT 1
                    FROM reservations res
                    WHERE res.table_id = @TableId AND res.reservation_date = @ReservationDate AND res.status <> 'cancelled'
                );
         """;

        using var connection = await factory.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(sql, new { TableId = tableId, ReservationDate = reservationDate });
    }

    public async Task<int> CreateReservation(Reservation reservation)
    {
        const string sql = """
            INSERT INTO reservations (table_id, reservation_date, customer_name, customer_email, customer_phone, guest_count)
            VALUES(@TableId, @ReservationDate, @CustomerName, @CustomerEmail, @CustomerPhone, @GuestCount);
            SELECT LAST_INSERT_ID();
            """;
        using var connection = await factory.CreateConnection();
        var reservationId = await connection.ExecuteScalarAsync<int>(sql, reservation);

        return reservationId;
    }

    public async Task ConfirmReservationAsync(int id)
    {
        const string sql = """
            UPDATE reservations 
            SET 
                status = 'confirmed'
            WHERE id = @ReservationId
            ;
            """;
        using var connection = await factory.CreateConnection();
        await connection.ExecuteAsync(sql, new { ReservationId = id });
    }

    public async Task CancelReservationAsync(int id)
    {
        const string sql = """
            UPDATE reservations 
            SET 
                status = 'cancelled'
            WHERE id = @ReservationId
            ;
            """;
        using var connection = await factory.CreateConnection();
        await connection.ExecuteAsync(sql, new { ReservationId = id });
    }
}
