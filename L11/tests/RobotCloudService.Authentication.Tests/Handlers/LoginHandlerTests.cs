using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Authentications.Application.Users;
using RobotCloudService.Authentications.Application.Users.Queries;
using RobotCloudService.Authentications.Application.Users.ValueObjects;

namespace RobotCloudService.Authentication.Tests.Handlers;

public class LoginHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;

    private readonly Login.Handler _handler;

    public LoginHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _tokenService = Substitute.For<ITokenService>();
        _hashService = Substitute.For<IHashService>();

        _handler = new Login.Handler(_unitOfWork, _tokenService, _hashService);
    }

    [Fact]
    public async Task IfEmailNotFound_ShouldBeReturnInvalidCredentialsError()
    {
        //arrange        
        var query = new Login.Query("email@address.com", "password");
        _unitOfWork.Users.GetByEmailAsync(query.Email, default).ReturnsNull();

        //act
        var result = await _handler.Handle(query, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(query.Email, default);
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("Login.InvalidCredentials");
    }

    [Fact]
    public async Task IfPasswordNotMatch_ShouldBeReturnInvalidCredentialsError()
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        var query = new Login.Query("email@address.com", "password");
        _unitOfWork.Users.GetByEmailAsync(query.Email, default).Returns(user);
        _hashService.VerifyPassword(query.Password, user.Password).Returns(false);

        //act
        var result = await _handler.Handle(query, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(query.Email, default);
        _hashService.Received(1).VerifyPassword(query.Password, user.Password);
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("Login.InvalidCredentials");
    }

    [Fact]
    public async Task IfSuccess_ShouldBeReturnToken()
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        var query = new Login.Query("email@address.com", "password");
        var token = "token";
        _unitOfWork.Users.GetByEmailAsync(query.Email, default).Returns(user);
        _hashService.VerifyPassword(query.Password, user.Password).Returns(true);
        _tokenService.GetToken(user).Returns(token);
        //act
        var result = await _handler.Handle(query, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(query.Email, default);
        _hashService.Received(1).VerifyPassword(query.Password, user.Password);
        _tokenService.Received(1).GetToken(user);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(token);
    }
}
