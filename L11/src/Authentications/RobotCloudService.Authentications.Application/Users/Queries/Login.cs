using FluentValidation;

using MediatR;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Application.Users.Queries;

public static class Login
{
    public record Query(string Email, string Password) : IRequest<DataOrError<string>>;

    public class Validator : AbstractValidator<Query>
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

    public class Handler(
        IUnitOfWork unitOfWork, 
        ITokenService tokenService, 
        IHashService hashService) : IRequestHandler<Query, DataOrError<string>>
    {
        public async Task<DataOrError<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByEmailAsync(query.Email, cancellationToken);
            if(user is null)
            {
                return Error.Forbidden("Login.InvalidCredentials", "Invalid email or password");
            }

            if(!hashService.VerifyPassword(query.Password, user.Password))
            {
                return Error.Forbidden("Login.InvalidCredentials", "Invalid email or password");
            }

            return tokenService.GetToken(user);
        }
    }
}
