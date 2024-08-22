using CloudService.Application.Base.Repositories;
using CloudService.Application.Base.Services;
using CloudService.Application.Exceptions;
using CloudService.Domain;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Users.Commands;

public static class Register
{
    public record Command(string Username, string Password) : IRequest<string>;

    public class Validator : AbstractValidator<Command>
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
        IPasswordHasher passwordHasher) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            if (await unitOfWork.Users.ExistsByUsernameAsync(username: command.Username, cancellationToken))
            {
                throw new UsernameAlreadyExistsException(command.Username);
            }

            var passwordHash = passwordHasher.HashPassword(command.Password);
            var user = new User()
            {
                Username = command.Username,
                Password = passwordHash
            };

            try
            {
                await unitOfWork.Users.AddAsync(user, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (System.Exception)
            {

                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }


            var token = tokenGenerator.GenerateToken(user);

            return token;
        }

    }
}