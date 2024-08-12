using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Mappers;

namespace Presentation.Controllers;

[Route(Contracts.Notes.Route)]
public class NoteController(ISender sender) : BaseController
{
    [HttpGet(Contracts.Notes.Requests.GetAll.Route)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new Application.Notes.Queries.GetAll.Query();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result.Select(x => x.MapToResponse()));
    }

    [HttpGet(Contracts.Notes.Requests.GetNote.Route)]
    public async Task<IActionResult> GetNote(Guid noteId, CancellationToken cancellationToken)
    {
        var query = new Application.Notes.Queries.GetNote.Query(noteId);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(() => Ok(result.Value.MapToResponse()), Problem);
    }

    [HttpPost(Contracts.Notes.Requests.Create.Route)]
    public async Task<IActionResult> CreateNote(Contracts.Notes.Requests.Create request, CancellationToken cancellationToken)
    {
        var command = new Application.Notes.Commands.Create.Command(request.Title, request.Content);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(() => Created(Contracts.Notes.Requests.GetNote.Route, result.Value.Id), Problem);
    }

    [HttpPut(Contracts.Notes.Requests.UpdateTitle.Route)]
    public async Task<IActionResult> UpdateTitle(Contracts.Notes.Requests.UpdateTitle request, CancellationToken cancellationToken)
    {
        var command = new Application.Notes.Commands.UpdateTitle.Command(request.NoteId, request.Title);
        var result = await sender.Send(command, cancellationToken);
        return result.Match<IActionResult>(Ok, Problem);
    }

    [HttpPut(Contracts.Notes.Requests.UpdateContent.Route)]
    public async Task<IActionResult> UpdateContent(Contracts.Notes.Requests.UpdateContent request, CancellationToken cancellationToken)
    {
        var command = new Application.Notes.Commands.UpdateContent.Command(request.NoteId, request.Content);
        var result = await sender.Send(command, cancellationToken);
        return result.Match<IActionResult>(Ok, Problem);
    }

    [HttpDelete(Contracts.Notes.Requests.Delete.Route)]
    public async Task<IActionResult> Delete(Contracts.Notes.Requests.Delete request, CancellationToken cancellationToken)
    {
        var command = new Application.Notes.Commands.Delete.Command(request.NoteId);
        var result = await sender.Send(command, cancellationToken);
        return result.Match<IActionResult>(Ok, Problem);
    }
}