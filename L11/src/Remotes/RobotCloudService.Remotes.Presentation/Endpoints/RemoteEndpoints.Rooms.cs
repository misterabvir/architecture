using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RobotCloudService.Remotes.Presentation.Endpoints;



public static partial class RemoteEndpoints
{
    private static async Task<IResult> AddRoomHandler(
        [FromBody] Contracts.Rooms.Requests.AddRoom request,
        HttpContext context,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(GetUserId(context));
        var result = await sender.Send(command, cancellationToken);
        return result.Match(
            success: () => Results.Ok(Contracts.Rooms.Responses.Room.FromDomain(result.Value)),
            failure: Problem);
    }

    private static async Task<IResult> UpdateRoomHandler(
    [FromBody] Contracts.Rooms.Requests.UpdateRoom request,
    HttpContext context,
    [FromServices] ISender sender,
    CancellationToken cancellationToken)
    {
        var command = request.ToCommand(GetUserId(context));
        var result = await sender.Send(command, cancellationToken);
        return result.Match(
            success: () => Results.Ok(Contracts.Rooms.Responses.Room.FromDomain(result.Value)),
            failure: Problem);
    }
}
