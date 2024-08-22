namespace CloudService.Domain;

public record Status(string Value){
    public static Status Create(string value) => new (value);
    public static Status Online => new ("Online");
    public static Status Offline => new ("Offline");
}
