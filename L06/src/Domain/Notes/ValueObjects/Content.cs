namespace Domain.Notes.ValueObjects;

public record Content (string Value)
{
    public static Content Create(string value) => new (value);

    public const int MaxLength = 1000;
    public const int MinLength = 1;
}