using CloudService.Application.Base.Repositories;
using CloudService.Application.Base.Services;
using CloudService.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Users.Queries;

public static class Login
{
    public record Query(string Username, string Password) : IRequest<string>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Username).MinimumLength(6).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Password)
                .Must(x => x.Any(char.IsDigit))
                .Must(x => x.Any(char.IsLower))
                .Must(x => x.Any(char.IsUpper))
                .MinimumLength(8)
                .NotEmpty();
        }
    }

    public class Handler(
        IUnitOfWork unitOfWork,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher) : IRequestHandler<Query, string>
    {
        public async Task<string> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByUsernameAsync(query.Username, cancellationToken)
                ?? throw new InvalidCredentialsException();

            if (!passwordHasher.Verify(query.Password, user.Password))
            {
                throw new InvalidCredentialsException();
            }

            return tokenGenerator.GenerateToken(user);
        }
    }
}