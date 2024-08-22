using System.ComponentModel.DataAnnotations;

namespace CloudService.Api.Contracts;

public static class Users
{
    public static class Responses
    {
        public record Token(string Value);
    }

    public static class Requests
    {
        public record Login([Required] string Username, [Required] string Password);
        public record Register([Required] string Username, [Required] string Password);
    }
}