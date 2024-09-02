using FluentValidation;

using MediatR;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Authentications.Application.Users.ValueObjects;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Application.Users.Commands;

public static class ResetPassword
{
    public record Command(string Email, string NewPassword, string Code) : IRequest<SuccessOrError>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .Must(x => x.Any(c => char.IsDigit(c)))
                .WithMessage("Password must contain at least one digit")
                .Must(x => x.Any(c => char.IsUpper(c)))
                .WithMessage("Password must contain at least one uppercase letter")
                .Must(x => x.Any(c => char.IsLower(c)))
                .WithMessage("Password must contain at least one lowercase letter")
                .Must(x => x.Any(c => !char.IsLetterOrDigit(c)))
                .WithMessage("Password must contain at least one special character");
            RuleFor(x => x.Code).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork,
        IVerificationService verificationService,
        IHashService hashService) : IRequestHandler<Command, SuccessOrError>
    {
        public async Task<SuccessOrError> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
            if(user is null)
            {
                return Error.NotFound("ResetPassword.NotFound", "User not found");
            }

            var result = await verificationService.VerifyCodeAsync(user.UserId, request.Code, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error;
            }

            var passwordHash = hashService.HashPassword(request.NewPassword);
            user.ChangePassword(Password.Create(passwordHash));

            await unitOfWork.Users.UpdateAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return SuccessOrError.Success;
        
        }
    }
}
