using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteCleaner.Server.Domain;
using System.Security.Cryptography.X509Certificates;

namespace RemoteCleaner.Server.Infrastructure.Persistence.Configurations;

public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{

    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms");
        builder.HasKey(x => x.RoomId).HasName("pk_rooms");
        builder.Property(x => x.RoomId).HasColumnName("room_id");
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(p => p.X).HasColumnName("x");
        builder.Property(p => p.Y).HasColumnName("y");
        builder.Property(s => s.Width).HasColumnName("width");
        builder.Property(s => s.Length).HasColumnName("length");
        builder.Property(s=>s.CleanedAt).HasColumnName("cleaned_at");

        builder.HasData([
                new Room()
                {
                    Name = "Kitchen",
                    X = 25, Y = 25 ,
                    Width = 100, Length = 100 ,
                    RoomId = 1
                },
                new Room()
                {
                    Name = "Bedroom",
                    X = 25, Y = 75,
                    Width = 200, Length = 200,
                    RoomId = 2
                },
                new Room(){
                    Name = "Bathroom",
                    X = 75, Y = 25,
                    Width = 150, Length = 150,
                    RoomId = 3
                },
                new Room()
                {
                    Name = "Living Room",
                    X = 75, Y = 75,
                    Width = 250, Length = 250,
                    RoomId = 4
                }
            ]);
    }

}

public class StationEntityTypeConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.ToTable("stations");
        builder.HasKey(x => x.StationId).HasName("pk_stations");
        builder.Property(x => x.StationId).HasColumnName("station_id");
        builder.Property(x => x.SerialNumber).HasColumnName("serial_number").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();

        builder.Property(p => p.X).HasColumnName("x");
        builder.Property(p => p.Y).HasColumnName("y");
        

        builder.HasData(new Station()
        {
            Name = "StationCleaner",
            SerialNumber = "#123456789N",
             X = 50, Y = 50,
            StationId = 1
        });

        builder.OwnsMany(x => x.Logs, ConfigureLog);
    }

    private void ConfigureLog(OwnedNavigationBuilder<Station, Log> builder)
    {
        builder.ToTable("station_logs");
        builder.WithOwner().HasForeignKey(x => x.StationId);
        builder.HasKey(x => new { x.StationId, x.LogId }).HasName("pk_logs");

        builder.Property(x => x.LogId).HasColumnName("log_id");
        builder.Property(x => x.StationId).HasColumnName("station_id");
        builder.Property(x => x.Message).HasColumnName("message").IsRequired();
        builder.Property(x => x.Time).HasColumnName("time_at").IsRequired();
    }
}






