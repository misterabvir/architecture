using FluentValidation;

using MediatR;

using RobotCloudService.Application.Results;

namespace RobotCloudService.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
: IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : SuccessOrError
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var errors = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .Select(f => Error.Validation(f.PropertyName, f.ErrorMessage))
            .ToList();

        return errors.Count != 0 ? (TResponse)(dynamic)errors.First() : await next();
    }
}

