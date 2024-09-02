using FluentValidation;

using MediatR;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Application.Users.Commands;

public static class Confirm
{
    public record Command(string Email, string Code) : IRequest<DataOrError<string>>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Code).NotEmpty();
        }
    }

    public class Handler(
        IUnitOfWork unitOfWork, 
        IVerificationService verificationService, 
        ITokenService tokenService) : IRequestHandler<Command, DataOrError<string>>
    {
        public async Task<DataOrError<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByEmailAsync(command.Email, cancellationToken);
            if(user is null)
            {
                return Error.NotFound("Confirm.UserNotFound", "User not found");
            }

            if (user.EmailVerified)
            {
                return Error.BadRequest("Confirm.EmailAlreadyVerified", "Email already verified");
            }

            var result = await verificationService.VerifyCodeAsync(user.UserId, command.Code, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error;
            }

            user.ConfirmEmail();

            await unitOfWork.Users.UpdateAsync(user, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var token = tokenService.GetToken(user);

            return token;
        }
    }
}
