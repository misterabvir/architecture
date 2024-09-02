using FluentValidation;

using MediatR;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Application.Users.Queries;

public static class RepeatSendCode
{
    public record Query(string Email) : IRequest<SuccessOrError>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }

    public class Handler(IUnitOfWork unitOfWork, IVerificationService verificationService) : IRequestHandler<Query, SuccessOrError>
    {
        public async Task<SuccessOrError> Handle(Query request, CancellationToken cancellationToken)
        {
            var user  = await unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
            if(user is null)
            {
                return Error.NotFound("RepeatCode.UserNotFound", "User not found");
            }

            return await verificationService.SendVerificationCodeAsync(user.UserId, user.Email, cancellationToken);
        }
    }   
}
