using System.ComponentModel.DataAnnotations;

namespace CloudService.Api.Contracts;

public static partial class Users
{
    public static class Requests
    {
        public record Login([Required] string Username, [Required] string Password);
        public record Register([Required] string Username, [Required] string Password);
    }
}