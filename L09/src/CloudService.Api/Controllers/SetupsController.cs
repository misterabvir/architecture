using System.Security.Claims;
using CloudService.Api.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudService.Api.Controllers;

[ApiController]
[Authorize, Route("setups")]
public class SetupsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSetups()
    {
        var query = new Application.Setups.Queries.GetAll.Query(GetUserId());
        var configs = await sender.Send(query);
        return Ok(Setups.FromDomain(configs));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSetups(Guid id)
    {
        var query = new Application.Setups.Queries.GetById.Query(GetUserId(), id);
        var config = await sender.Send(query);
        return Ok(Setups.FromDomain(config));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Setups.Requests.Create request)
    {
        var command = request.ToCommand(GetUserId());
        await sender.Send(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Setups.Requests.Update request)
    {
        var command = request.ToCommand(GetUserId());
        await sender.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Setups.Requests.Remove request)
    {
        var command = request.ToCommand(GetUserId());
        await sender.Send(command);
        return Ok();
    }

    [HttpPut("run")]
    public async Task<IActionResult> Run(Setups.Requests.Run request)
    {
        var command = request.ToCommand(GetUserId());
        await sender.Send(command);
        return Ok();
    }

    [HttpPut("stop")]
    public async Task<IActionResult> Stop(Setups.Requests.Stop request)
    {
        var command = request.ToCommand(GetUserId());
        await sender.Send(command);
        return Ok();
    }

    private Guid GetUserId()
    {
        var id = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(id);
    }
}