using CloudService.Application.Devices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudService.Api.Controllers;

[ApiController]
[Route("devices")]
public class DeviceController(ISender sender) : ControllerBase
{
    [HttpGet("all-cpu")]
    public async Task<IActionResult> GetAllCpu() => Ok(await sender.Send(new AllCpu.Query()));

    [HttpGet("all-ram")]
    public async Task<IActionResult> GetAllRam() => Ok(await sender.Send(new AllRam.Query()));

    [HttpGet("all-rom")]
    public async Task<IActionResult> GetAllRom() => Ok(await sender.Send(new AllRom.Query()));

    [HttpGet("all-ip")]
    public async Task<IActionResult> GetAllIp() => Ok(await sender.Send(new AllIp.Query()));

    [HttpGet("all-os")]
    public async Task<IActionResult> GetAllOs() => Ok(await sender.Send(new AllOs.Query()));

}