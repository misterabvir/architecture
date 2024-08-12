using Domain.Abstractions;
using Domain.Notes.ValueObjects;

namespace Domain.Notes;

public class Note : Entity
{
    private Note() { } // for ORM
    private Note(Guid noteId, Title title, Content content)
    {
        Id = noteId;
        Title = title;
        Content = content;
    }

    public static Note Create(Title title, Content content)
    {
        return new Note(Guid.NewGuid(), title, content);
    }

    public Title Title { get; private set; } = null!;
    public Content Content { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    
    public void UpdateTitle(Title title)
    {
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateContent(Content content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}