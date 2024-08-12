using Application.Common.Results;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator> validators) :
IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
where TResponse : Result
{
public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
{

    var context = new ValidationContext<TRequest>(request);

    var error = validators
        .Select(x => x.Validate(context))
        .SelectMany(x => x.Errors)
        .Where(x => x != null)
        .Select(e => Error.BadRequest($"{e.PropertyName} : {e.ErrorMessage}"))
        .FirstOrDefault();

    return error is not null ? (dynamic)error : await next();
}
}