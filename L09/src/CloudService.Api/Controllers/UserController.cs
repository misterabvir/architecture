using CloudService.Api.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudService.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(Users.Requests.Register request)
    {
        var command = new Application.Users.Commands.Register.Command(request.Username, request.Password);
        var token = await sender.Send(command);       
        return Ok(new Users.Responses.Token(token));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Users.Requests.Login request)
    {
        var query = new Application.Users.Queries.Login.Query(request.Username, request.Password);
        var token = await sender.Send(query);       
        return Ok(new Users.Responses.Token(token));
    }
}