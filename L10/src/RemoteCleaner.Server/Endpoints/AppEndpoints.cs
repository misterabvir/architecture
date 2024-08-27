using RemoteCleaner.Server.Infrastructure.Repositories;

namespace RemoteCleaner.Server.Endpoints;

public static class AppEndpoints
{
    public static void MapAppEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/log", async (IUnitOfWork unitOfWork) =>
        {
            return Results.Ok(await unitOfWork.Stations.GertLogsAsync());
        });

        routes.MapGet("/rooms", async (IUnitOfWork unitOfWork) =>
        {
            return Results.Ok(await unitOfWork.Rooms.GetRoomsAsync());
        });
        routes.MapGet("station", async (IUnitOfWork unitOfWork) =>
        {
            return Results.Ok(await unitOfWork.Stations.GetStationAsync());
        });
    }

}
