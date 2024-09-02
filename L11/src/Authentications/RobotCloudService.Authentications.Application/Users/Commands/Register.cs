using FluentValidation;

using MediatR;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Authentications.Application.Users;
using RobotCloudService.Authentications.Application.Users.ValueObjects;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Application.Users.Commands;

public static class Register
{
    public record Command(string Email, string Password) : IRequest<SuccessOrError>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password)
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
        }
    }

    public class Handler(IUnitOfWork unitOfWork, IHashService hashService) : 
        IRequestHandler<Command, SuccessOrError>
    {
        public async Task<SuccessOrError> Handle(Command command, CancellationToken cancellationToken)
        {
            if(await unitOfWork.Users.IsEmailExist(Email.Create(command.Email), cancellationToken))
            {
                return Error.BadRequest("Register.EmailAlreadyExist", "Email already exist");
            }

            var passwordHash = hashService.HashPassword(command.Password);
            
            
            var user = User.Create(Email.Create(command.Email), Password.Create(passwordHash));

            await unitOfWork.Users.AddAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return SuccessOrError.Success;

        }
    }
}
