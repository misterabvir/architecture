using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using RobotCloudService.Application.Results;
using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Authentications.Application.Users;
using RobotCloudService.Authentications.Application.Users.Commands;
using RobotCloudService.Authentications.Application.Users.ValueObjects;

namespace RobotCloudService.Authentication.Tests.Handlers;

public class ResetPasswordHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVerificationService _verificationService;
    private readonly IHashService _hashService;

    private readonly ResetPassword.Handler _handler;

    public ResetPasswordHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _verificationService = Substitute.For<IVerificationService>();
        _hashService = Substitute.For<IHashService>();

        _handler = new ResetPassword.Handler(_unitOfWork, _verificationService, _hashService);
    }

    [Fact]
    public async Task IfEmailNotExist_ShouldBeReturnUserNotFoundError()
    {
        //arrange        
        var command = new ResetPassword.Command("email@address.com", "new-password", "verification-code");
        _unitOfWork.Users.GetByEmailAsync(command.Email, default).ReturnsNull();

        //act
        var result = await _handler.Handle(command, default);

        //asserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("ResetPassword.UserNotFound");
    }

    [Fact]
    public async Task IfNotVerified_ShouldBeReturnVerifyError()
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        var command = new ResetPassword.Command(user.Email, "new-password", "verification-code");
        _unitOfWork.Users.GetByEmailAsync(command.Email, default).Returns(user);
        var verify = (SuccessOrError)Error.Forbidden("Verification.NotVerified", "details");
        _verificationService.VerifyCodeAsync(user.UserId, command.Code, default).Returns(verify);

        //act
        var result = await _handler.Handle(command, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        await _verificationService.Received(1).VerifyCodeAsync(user.UserId, command.Code, Arg.Any<CancellationToken>());
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(verify.Error);
    }

    [Fact]
    public async Task IfSucces_ShouldBeReturnSuccess()
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        var command = new ResetPassword.Command(user.Email, "new-password", "verification-code");
        _unitOfWork.Users.GetByEmailAsync(command.Email, default).Returns(user);
        _verificationService.VerifyCodeAsync(user.UserId, command.Code, default).Returns(SuccessOrError.Success);
        var hashedPassword = "HASHED";
        _hashService.HashPassword(command.NewPassword).Returns(hashedPassword);

        //act
        var result = await _handler.Handle(command, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        await _verificationService.Received(1).VerifyCodeAsync(user.UserId, command.Code, Arg.Any<CancellationToken>());
        await _unitOfWork.Users.Received(1).UpdateAsync(
            Arg.Is<User>(u =>
                u.Email.Value == command.Email &&
                u.Password.Value == hashedPassword
            ),
            Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }
}
