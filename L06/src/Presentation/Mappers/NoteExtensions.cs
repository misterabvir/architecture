namespace Presentation.Mappers;

public static class NoteExtensions
{
    public static Contracts.Notes.Responses.Note MapToResponse(this Domain.Notes.Note note) =>
        new(note.Id, note.Title.Value, note.Content.Value, note.CreatedAt, note.UpdatedAt);
}