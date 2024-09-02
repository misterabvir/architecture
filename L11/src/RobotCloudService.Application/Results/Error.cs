using System.Net;

namespace RobotCloudService.Application.Results;

public record Error(HttpStatusCode StatusCode, string Title, string Details)
{
    public readonly static Error None = new(HttpStatusCode.OK, string.Empty, string.Empty);

    public static Error Conflict(string title, string message) => new(HttpStatusCode.Conflict, title, message);
    public static Error BadRequest(string title, string message) => new(HttpStatusCode.BadRequest, title, message);
    public static Error NotFound(string title, string message) => new(HttpStatusCode.NotFound, title, message);
    public static Error Forbidden(string title, string message) => new(HttpStatusCode.Forbidden, title, message);
    public static Error Validation(string title, string message) => new(HttpStatusCode.BadRequest, title, message);
}