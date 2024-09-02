using MediatR;
using Microsoft.AspNetCore.Mvc;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Presentation.Endpoints;

public static class AuthenticationEndoints
{
    public static WebApplication MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost(Contracts.Authentications.Requests.Register.Route, RegisterHandler)
            .WithTags("users")
            .AllowAnonymous();

        app.MapPost(Contracts.Authentications.Requests.Login.Route, LoginHandler)
            .WithTags("users")
            .AllowAnonymous();

        app.MapPost(Contracts.Authentications.Requests.Confirm.Route, ConfirmHandler)
            .WithTags("users")
            .AllowAnonymous();

        app.MapPut(Contracts.Authentications.Requests.ResetPassword.Route, ResetPasswordHandler)
            .WithTags("users")
            .AllowAnonymous();

        app.MapPost(Contracts.Authentications.Requests.ForgotPassword.Route, ForgotPasswordHandler)
            .WithTags("users")
            .AllowAnonymous();

        app.MapPost(Contracts.Authentications.Requests.RepeatSendCode.Route, RepeatSendCodeHandler)
            .WithTags("users")
            .AllowAnonymous();

        return app;
    }

    private static async Task<IResult> RegisterHandler(
        [FromBody] Contracts.Authentications.Requests.Register request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new Application.Users.Commands.Register.Command(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(success: () => Results.Ok(), failure:  Problem);    
    }

    private static async Task<IResult> LoginHandler(
        [FromBody] Contracts.Authentications.Requests.Login request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Application.Users.Queries.Login.Query(request.Email, request.Password);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(success: () => Results.Ok(result.Value), failure: Problem);
    }

    private static async Task<IResult> ConfirmHandler(
        [FromBody] Contracts.Authentications.Requests.Confirm request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new Application.Users.Commands.Confirm.Command(request.Email, request.Code);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(success: () => Results.Ok(result.Value), failure: Problem);
    }

    private static async Task<IResult> ResetPasswordHandler(
        [FromBody] Contracts.Authentications.Requests.ResetPassword request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new Application.Users.Commands.ResetPassword.Command(request.Email, request.NewPassword, request.Code);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(success: () => Results.Ok(), failure: Problem);
    }

    private static async Task<IResult> ForgotPasswordHandler(
        [FromBody] Contracts.Authentications.Requests.ForgotPassword request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Application.Users.Queries.ForgotPassword.Query(request.Email);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(success: () => Results.Ok(), failure: Problem);
    }

    private static async Task<IResult> RepeatSendCodeHandler(
        [FromBody] Contracts.Authentications.Requests.RepeatSendCode request,
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
