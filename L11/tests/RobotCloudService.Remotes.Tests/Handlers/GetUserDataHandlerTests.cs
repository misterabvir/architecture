using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;


using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Application.Users.Commands;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.Queries;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Tests.Handlers;

public class GetUserDataHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetUserData.Handler _handler;
    private readonly User _user = User.Create(UserId.CreateUnique());

    public GetUserDataHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new GetUserData.Handler(_unitOfWork);
    }

    [Fact]
    public async Task WhenUserNotExist_ReturnNotFoundError()
    {
        //arrange
        var command = new GetUserData.Query(UserId.CreateUnique());
        _unitOfWork.Users.GetByIdAsync(UserId.CreateUnique(), default).ReturnsNull();
        //act
        var result = await _handler.Handle(command, default);

        //aserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("GetUserData.UserNotFound");
    }

    [Fact]
    public async Task WhenUserExist_ReturnDomainLogicResult()
    {
        //arrange      
        var command = new GetUserData.Query(_user.UserId);
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).Returns(_user);

        //act
        var result = await _handler.Handle(command, default);

        //aserts
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(_user);
    }
}

public class GetUserLogsHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetUserLogs.Handler _handler;
    private readonly User _user = User.Create(UserId.CreateUnique());

    public GetUserLogsHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new GetUserLogs.Handler(_unitOfWork);
    }

    [Fact]
    public async Task WhenUserNotExist_ReturnNotFoundError()
    {
        //arrange
        var command = new GetUserLogs.Query(UserId.CreateUnique());
        _unitOfWork.Users.GetByIdAsync(UserId.CreateUnique(), default).ReturnsNull();
        //act
        var result = await _handler.Handle(command, default);

        //aserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("GetUserLogs.UserNotFound");
    }

    [Fact]
    public async Task WhenUserExist_ReturnDomainLogicResult()
    {
        //arrange      
        var command = new GetUserLogs.Query(_user.UserId);
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).Returns(_user);

        //act
        var result = await _handler.Handle(command, default);

        //aserts

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<List<Log>>();
    }
}