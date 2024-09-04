using FluentAssertions;

using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users;

public class AddRobotDomainLogicTests
{
    private readonly User _user = User.Create(UserId.CreateUnique());

    [Fact]
    public void AddRobot_WhenRoomsEmpty_ShouldReturnError()
    {
        // Arrange
        var model = Model.Create("Robot 1");
        var speed = Speed.Create(1);
        // Act
        var result = _user.AddRobot(model, speed);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("AddRobot.MaxCount");
    }


    [Fact]
    public void AddRobot_WhenRobotExist_ShouldBeReturnError()
    {
        // Arrange
        var model = Model.Create("Robot 1");
        var speed = Speed.Create(1);
        // Act
        _user.AddRobot(model, speed);
        var result = _user.AddRobot(model, speed);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("AddRobot.MaxCount");
    }

    [Fact]
    public void AddRobot_ShouldBeAddRobot()
    {
        // Arrange
        var title = Title.Create("Room 1");
        var area = Area.Create(10);
        var model = Model.Create("Robot 1");
        var speed = Speed.Create(1);
        _user.AddRoom(title, area);
        
        // Act
        var result = _user.AddRobot(model, speed);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<Robot>();
        result.Value.Model.Value.Should().Be(model);
        result.Value.Speed.Value.Should().Be(speed);
        _user.Robots.Count.Should().Be(1);
        _user.Robots.Should().Contain(result.Value);
    }
}