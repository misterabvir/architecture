namespace CloudService.Api.Contracts;

public static partial class Users
{
    public static Application.Users.Commands.Register.Command ToCommand(this Requests.Register request)
        => new(request.Username, request.Password);

    public static Application.Users.Queries.Login.Query ToQuery(this Requests.Login request)
        => new(request.Username, request.Password);
}