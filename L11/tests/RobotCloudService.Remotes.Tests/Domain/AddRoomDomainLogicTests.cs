using FluentAssertions;

using NSubstitute;

using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.Events;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users;

public class AddRoomDomainLogicTests
{
    private readonly User _user = User.Create(UserId.CreateUnique());

    [Fact]
    public void AddRoom_WhenRoomDoesNotExist_ShouldAddRoom()
    {
        // Arrange
        var title = Title.Create("Room 1");
        var area = Area.Create(10);
        // Act
        var result = _user.AddRoom(title, area);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<Room>();
        result.Value.Area.Value.Should().Be(area);
        result.Value.Title.Value.Should().Be(title);
        result.Value.UserId.Value.Should().Be(_user.UserId);
        _user.Rooms.Count.Should().Be(1);
        _user.Rooms.Should().Contain(result.Value);

    }

    [Fact]
    public void AddRoom_WhenRoomExist_ShouldReturnError()
    {
        // Arrange
        var title = Title.Create("Room 1");
        var area = Area.Create(10);
        // Act
        _user.AddRoom(title, area);
        var result = _user.AddRoom(title, area);

        // Assert
        result.IsFailure.Should().BeTrue();
        _user.Rooms.Count.Should().Be(1);
        result.Error.Title.Should().Be("AddRoom.AlreadyExist");
    }
}

public class StartCleanDomainLogicTests
{
    private readonly User _user;
    private readonly Room _room;
    public StartCleanDomainLogicTests()
    {
        _user = User.Create(UserId.CreateUnique());
        _room = _user.AddRoom(Title.Create("Room 1"), Area.Create(10)).Value;
    }

    [Fact]
    public void WhenRobotDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var robotId = RobotId.CreateUnique();
        // Act
        var result = _user.StartClean(robotId, _room.RoomId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("StartClean.RobotNotFound");
    }

    [Fact]
    public void WhenRoomDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var robot = _user.AddRobot(Model.Create("Robot 1"), Speed.Create(10)).Value;
        var roomId = RoomId.CreateUnique();
        // Act
        var result = _user.StartClean(robot.RobotId, roomId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("StartClean.RoomNotFound");
    }

    [Fact]
    public void WhenSuccess_ShouldReturnUpdatedRobot()
    {
        // Arrange
        var robot = _user.AddRobot(Model.Create("Robot 1"), Speed.Create(10)).Value;        
        // Act
        var result = _user.StartClean(robot.RobotId, _room.RoomId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<Robot>();
        result.Value.RoomId.Should().Be(_room.RoomId);
        _user.Logs.Should().HaveCount(1);
        _user.DomainEvents.Last().Should().BeOfType<CleanStarted.Notification>();
    }
}


public class StopCleanDomainLogicTests
{
    private readonly User _user;
    private readonly Room _room;
    public StopCleanDomainLogicTests()
    {
        _user = User.Create(UserId.CreateUnique());
        _room = _user.AddRoom(Title.Create("Room 1"), Area.Create(10)).Value;
    }

    [Fact]
    public void WhenRobotDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var robotId = RobotId.CreateUnique();
        // Act
        var result = _user.StopClean(robotId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("StopClean.RobotNotFound");
    }

    [Fact]
    public void WhenRobotIdle_ShouldReturnError()
    {
        // Arrange
        var model = Model.Create("Robot 1");
        var speed = Speed.Create(1);
        var robot = _user.AddRobot(model, speed).Value;
        // Act
        var result = _user.StopClean(robot.RobotId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("StopCleaning.Idle");
    }

    [Fact]
    public void WhenRobot_ShouldReturnUpdatedRobot()
    {
        // Arrange
        var model = Model.Create("Robot 1");
        var speed = Speed.Create(100000);
        var robot = _user.AddRobot(model, speed).Value;
        _user.StartClean(robot.RobotId, _room.RoomId);
        // Act
        var result = _user.StopClean(robot.RobotId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<Robot>();
        result.Value.RobotState.Should().Be(State.Idle);
        _user.Logs.Should().HaveCount(2);
        _user.DomainEvents.Last().Should().BeOfType<CleanStoped.Notification>();
    }
}