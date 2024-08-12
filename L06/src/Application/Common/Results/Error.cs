using System.Net;

namespace Application.Common.Results;

public record Error
{
    public HttpStatusCode Code { get; init; }
    public string Description { get; init; }
    private Error(HttpStatusCode code, string description)
    {
        Code = code;
        Description = description;
    }

    public static Error BadRequest(string description) => new(HttpStatusCode.BadRequest, description);
    public static Error NotFound(string description) => new(HttpStatusCode.NotFound, description);
    public readonly static Error None = new(HttpStatusCode.OK, string.Empty);

}