using System.Security.Claims;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RobotCloudService.Application.Results;

namespace RobotCloudService.Remotes.Presentation.Endpoints;

public static partial class RemoteEndpoints
{
    internal static WebApplication MapRemoteEndpoints(this WebApplication app)
    {
        app.MapGet(Contracts.Users.Routes.UserData, GetUserDataHandler)
            .WithTags("user")
            .RequireAuthorization();

        app.MapGet(Contracts.Users.Routes.UserLogs, GetUserLogsDataHandler)
            .WithTags("user")
            .RequireAuthorization();

        app.MapPost(Contracts.Rooms.Requests.AddRoom.Route, AddRoomHandler)
            .WithTags("room")
            .RequireAuthorization();

        app.MapPut(Contracts.Rooms.Requests.UpdateRoom.Route, UpdateRoomHandler)
            .WithTags("room")
            .RequireAuthorization();

        app.MapPost(Contracts.Robots.Requests.AddRobot.Route, AddRobotHandler)
            .WithTags("robot")
            .RequireAuthorization();
        
        app.MapPut(Contracts.Robots.Requests.UpdateRobot.Route, UpdateRobotHandler)
            .WithTags("robot")
            .RequireAuthorization();

        app.MapPut(Contracts.Robots.Requests.StartClean.Route, StartCleanHandler)
            .WithTags("robot")
            .RequireAuthorization();

        return app;
    }
    private static IResult Problem(Error error)
    {
        return Results.Problem(statusCode: (int)error.StatusCode, title: error.Title, detail: error.Details);
    }

    private static Ulid GetUserId(HttpContext context) => Ulid.Parse(context.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
