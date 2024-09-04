using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using RobotCloudService.Application.Results;
using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Authentications.Application.Users;
using RobotCloudService.Authentications.Application.Users.Queries;
using RobotCloudService.Authentications.Application.Users.ValueObjects;

namespace RobotCloudService.Authentication.Tests.Handlers;

public class RepeatSendCodeHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVerificationService _verificationService;

    private readonly RepeatSendCode.Handler _handler;

    public RepeatSendCodeHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _verificationService = Substitute.For<IVerificationService>();

        _handler = new RepeatSendCode.Handler(_unitOfWork, _verificationService);
    }

    [Fact]
    public async Task IfEmailNotExist_ShouldBeReturnUserNotFoundError()
    {
        //arrange        
        var query = new RepeatSendCode.Query("email@address.com");
        _unitOfWork.Users.GetByEmailAsync(query.Email, default).ReturnsNull();

        //act
        var result = await _handler.Handle(query, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(query.Email, default);
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("RepeatCode.UserNotFound");
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task IfEmailExist_ShouldBeReturnSendVerificationResult(SuccessOrError successOrError)
    {
        //arrange
        var user = User.Create(Email.Create("email@address.com"), Password.Create("password"));
        var query = new RepeatSendCode.Query(user.Email);
        _unitOfWork.Users.GetByEmailAsync(query.Email, default).Returns(user);
        _verificationService.SendVerificationCodeAsync(user.UserId, user.Email, default).Returns(successOrError);

        //act
        var result = await _handler.Handle(query, default);

        //asserts
        await _unitOfWork.Users.Received(1).GetByEmailAsync(query.Email, default);
        await _verificationService.Received(1).SendVerificationCodeAsync(user.UserId, user.Email, default);
        result.Should().Be(successOrError);
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { SuccessOrError.Success };
        yield return new object[] { Error.Conflict("title", "detail") };
    }
}
