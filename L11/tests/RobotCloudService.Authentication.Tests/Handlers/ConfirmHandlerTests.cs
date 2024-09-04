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

public class ConfirmHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVerificationService _verificationService;
    private readonly ITokenService _tokenService;

    private readonly Confirm.Handler _handler;
    public ConfirmHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _verificationService = Substitute.For<IVerificationService>();
        _tokenService = Substitute.For<ITokenService>();
        _handler = new Confirm.Handler(_unitOfWork, _verificationService, _tokenService);
    }

    [Fact]
    public async Task IfEmailNotExist_ShouldBeReturnNotFoundError()
    {
        //arrange
        var command = new Confirm.Command("email@address.com", "verification-code");
        _unitOfWork.Users.GetByEmailAsync(command.Email, default).ReturnsNull();

        //act
        var result = await _handler.Handle(command, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("Confirm.UserNotFound");
    }

    [Fact]
    public async Task IfEmailVerified_ShouldBeReturnEmailAlreadyVerifiedError()
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        user.ConfirmEmail();
        var command = new Confirm.Command(user.Email, "verification-code");
        _unitOfWork.Users.GetByEmailAsync(command.Email, default).Returns(user);

        //act
        var result = await _handler.Handle(command, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());

        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("Confirm.EmailAlreadyVerified");
    }

    [Fact]
    public async Task IfVerificationFail_ShouldBeReturnVerificationError()
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        var command = new Confirm.Command(user.Email, "verification-code");
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
    public async Task IfSuccess_ShouldBeReturnToken()
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        var command = new Confirm.Command(user.Email, "verification-code");
        var token = "token";
        _unitOfWork.Users.GetByEmailAsync(command.Email, default).Returns(user);
        _verificationService.VerifyCodeAsync(user.UserId, command.Code, default).Returns(SuccessOrError.Success);
        _tokenService.GetToken(user).Returns(token);
        //act
        var result = await _handler.Handle(command, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(command.Email, Arg.Any<CancellationToken>());
        await _verificationService.Received(1).VerifyCodeAsync(user.UserId, command.Code, Arg.Any<CancellationToken>());
        await _unitOfWork.Users.Received(1).UpdateAsync(user, Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(token);
        user.EmailVerified.Should().BeTrue();
    }


}
