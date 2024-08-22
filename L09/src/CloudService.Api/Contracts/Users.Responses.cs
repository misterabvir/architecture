namespace CloudService.Api.Contracts;

public static partial class Users
{
    public static class Responses
    {
        public record Token(string Value);
    }
}