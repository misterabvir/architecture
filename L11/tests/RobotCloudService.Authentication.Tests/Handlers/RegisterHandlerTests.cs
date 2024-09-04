using FluentAssertions;

using NSubstitute;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Authentications.Application.Users;
using RobotCloudService.Authentications.Application.Users.Commands;

namespace RobotCloudService.Authentication.Tests.Handlers;

public class RegisterHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashService _hashService;
    private readonly Register.Handler _handler;

    public RegisterHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _hashService = Substitute.For<IHashService>();
        _handler = new Register.Handler(_unitOfWork, _hashService);
    }

    [Fact]
    public async Task IfEmailNotUnique_ShouldBeReturnEmailNotUniqueError()
    {
        // Arrange
        var command = new Register.Command("email@address.com", "password");
        _unitOfWork.Users.IsEmailExist(command.Email, default).Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("Register.EmailAlreadyExist");
    }

    [Fact]
    public async Task IfSuccess_ShouldBeReturnSuccess()
    {
        // Arrange
        var hashedPassword = "HASHED";
        var command = new Register.Command("email@address.com", "password");
        _unitOfWork.Users.IsEmailExist(command.Email, default).Returns(false);
        _hashService.HashPassword(command.Password).Returns(hashedPassword);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _unitOfWork.Users.Received(1).AddAsync(
            Arg.Is<User>(u =>
                u.Email.Value == command.Email &&
                u.Password.Value == hashedPassword
            ),
            Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

}
