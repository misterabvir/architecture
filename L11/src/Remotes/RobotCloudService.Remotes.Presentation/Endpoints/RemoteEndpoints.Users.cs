using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace RobotCloudService.Remotes.Presentation.Endpoints;

public static partial class RemoteEndpoints
{
    private static async Task<IResult> GetUserDataHandler(
        HttpContext context,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Application.Users.Queries.GetUserData.Query(GetUserId(context));
        var result = await sender.Send(query, cancellationToken);
        return result.Match(
            success: () => Results.Ok(Contracts.Users.Responses.UserData.FromDomain(result.Value)), 
            failure: Problem);
    }

    private static async Task<IResult> GetUserLogsDataHandler(
        HttpContext context,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Application.Users.Queries.GetUserLogs.Query(GetUserId(context));
        var result = await sender.Send(query, cancellationToken);
        return result.Match(
            success: () => Results.Ok((result.Value.Select(Contracts.Users.Responses.Log.FromDomain))), 
            failure: Problem);
    }
}
