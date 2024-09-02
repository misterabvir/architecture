using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RobotCloudService.Remotes.Presentation.Endpoints;

public static partial class RemoteEndpoints
{
    private static async Task<IResult> AddRobotHandler(
        [FromBody] Contracts.Robots.Requests.AddRobot request,
        HttpContext context,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(GetUserId(context));
        var result = await sender.Send(command, cancellationToken);
        return result.Match(
            success: () => Results.Ok(Contracts.Robots.Responses.Robot.FromDomain(result.Value)),
            failure: Problem);
    }

    private static async Task<IResult> UpdateRobotHandler(
        [FromBody] Contracts.Robots.Requests.UpdateRobot request,
        HttpContext context,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(GetUserId(context));
        var result = await sender.Send(command, cancellationToken);
        return result.Match(
            success: () => Results.Ok(Contracts.Robots.Responses.Robot.FromDomain(result.Value)),
            failure: Problem);
    }    

    private static async Task<IResult> StartCleanHandler(
        [FromBody] Contracts.Robots.Requests.StartClean request,
        HttpContext context,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(GetUserId(context));
        var result = await sender.Send(command, cancellationToken);
        return result.Match(
            success: () => Results.Ok(Contracts.Robots.Responses.Robot.FromDomain(result.Value)),
            failure: Problem);
    }

}