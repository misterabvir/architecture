namespace Domain.Notes.ValueObjects;

public record Title (string Value)
{
    public static Title Create(string value) => new (value);
    public const int MaxLength = 100;
    public const int MinLength = 1;
}
