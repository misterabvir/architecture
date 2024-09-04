using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Application.Users.Commands;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Tests.Handlers;

public class StartCleanHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly StartClean.Handler _handler;
    private readonly User _user = User.Create(UserId.CreateUnique());
    private readonly Room _room;
    private readonly Robot _robot;

    public StartCleanHandler()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new StartClean.Handler(_unitOfWork);
        _room = _user.AddRoom(Title.Create("Room"), Area.Create(10)).Value;
        _robot = _user.AddRobot(Model.Create("Model"), Speed.Create(1)).Value;
    }

    [Fact]
    public async Task WhenUserNotExist_ReturnNotFoundError()
    {
        //arrange
        var command = new StartClean.Command(_user.UserId, _room.RoomId, _robot.RobotId );
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).ReturnsNull();
        //act

        var result = await _handler.Handle(command, default);

        //aserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("StartClean.UserNotFound");
    }

    [Fact]
    public async Task WhenUserExist_ReturnDomainLogicResult()
    {
        //arrange
        var command = new StartClean.Command(_user.UserId, _room.RoomId, _robot.RobotId);
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).Returns(_user);

        //act
        var result = await _handler.Handle(command, default);

        //aserts
        await _unitOfWork.Received(1).SaveChangesAsync(default);
        result.IsSuccess.Should().BeTrue();
    }
}
