namespace Presentation.Contracts;

public static class Notes
{
    public const string Route = "notes";

    public static class Requests
    {
        public record Create(string Title, string Content)
        {
            public const string Route = "create";
        }
        public record UpdateTitle(Guid NoteId, string Title)
        {
            public const string Route = "update-title";
        }
        public record UpdateContent(Guid NoteId, string Content)
        {
            public const string Route = "update-content";
        }
        public record Delete(Guid NoteId)
        {
            public const string Route = "delete";
        }
        public record GetNote
        {
            public const string Route = "get-by-id/{noteId}";
        }

        public record GetAll()
        {
            public const string Route = "";
        }
    }
    public static class Responses
    {
        public record Note(Guid NoteId, string Title, string Content, DateTime CreatedAt, DateTime UpdatedAt);
    }
}