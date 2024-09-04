using MediatR;
using Microsoft.AspNetCore.Mvc;
using RobotCloudService.Application.Results;
using RobotCloudService.Authentications.Presentation.Contracts;

namespace RobotCloudService.Authentications.Presentation.Endpoints;

public static class AuthenticationEndoints
{
    public static WebApplication MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapGroup("authentication")
            .MapPost(Requests.Register.Route, RegisterHandler)
            .WithTags("authentication")
            .AllowAnonymous();

        app.MapGroup("authentication")
            .MapPost(Requests.Login.Route, LoginHandler)
            .WithTags("authentication")
            .AllowAnonymous();

        app.MapGroup("authentication")
            .MapPost(Requests.Confirm.Route, ConfirmHandler)
            .WithTags("authentication")
            .AllowAnonymous();

        app.MapGroup("authentication")
            .MapPut(Requests.ResetPassword.Route, ResetPasswordHandler)
            .WithTags("authentication")
            .AllowAnonymous();

        app.MapGroup("authentication")
            .MapPost(Requests.ForgotPassword.Route, ForgotPasswordHandler)
            .WithTags("authentication")
            .AllowAnonymous();

        app.MapGroup("authentication")
            .MapPost(Requests.RepeatSendCode.Route, RepeatSendCodeHandler)
            .WithTags("authentication")
            .AllowAnonymous();

        return app;
    }

    private static async Task<IResult> RegisterHandler(
        [FromBody] Requests.Register request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new Application.Users.Commands.Register.Command(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(success: () => Results.Ok(), failure:  Problem);    
    }

    private static async Task<IResult> LoginHandler(
        [FromBody] Requests.Login request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Application.Users.Queries.Login.Query(request.Email, request.Password);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(success: () => Results.Ok(result.Value), failure: Problem);
    }

    private static async Task<IResult> ConfirmHandler(
        [FromBody] Requests.Confirm request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new Application.Users.Commands.Confirm.Command(request.Email, request.Code);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(success: () => Results.Ok(result.Value), failure: Problem);
    }

    private static async Task<IResult> ResetPasswordHandler(
        [FromBody] Requests.ResetPassword request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new Application.Users.Commands.ResetPassword.Command(request.Email, request.NewPassword, request.Code);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(success: () => Results.Ok(), failure: Problem);
    }

    private static async Task<IResult> ForgotPasswordHandler(
        [FromBody] Requests.ForgotPassword request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Application.Users.Queries.ForgotPassword.Query(request.Email);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(success: () => Results.Ok(), failure: Problem);
    }

    private static async Task<IResult> RepeatSendCodeHandler(
        [FromBody] Requests.RepeatSendCode request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Application.Users.Queries.RepeatSendCode.Query(request.Email);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(success: () => Results.Ok(), failure: Problem);
    }


    private static IResult Problem(Error error)
    {
        return Results.Problem(statusCode: (int)error.StatusCode, title: error.Title, detail: error.Details);
    }
    
}
