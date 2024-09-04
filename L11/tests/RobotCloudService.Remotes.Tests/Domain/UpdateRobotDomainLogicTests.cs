using FluentAssertions;

using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users;

public class UpdateRobotDomainLogicTests
{
    private readonly User _user = User.Create(UserId.CreateUnique());

    [Fact]
    public void UpdateRobot_WhenRobotDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var model = Model.Create("Model 1");
        var speed = Speed.Create(10);

        var robotId = RobotId.CreateUnique();
        // Act
        var result = _user.UpdateRobot(robotId, model, speed);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("UpdateRobot.NotFound");
    }

    [Fact]
    public void UpdateRobot_WhenRobotExist_ShouldBeReturnUpdatedRobot()
    {
        // Arrange
        var title = Title.Create("Room 1");
        var area = Area.Create(10);
        _user.AddRoom(title, area);
        var model = Model.Create("Model 1");
        var speed = Speed.Create(10);
        var robot = _user.AddRobot(model, speed).Value;
        var newModel = Model.Create("Model 2");
        var newSpeed = Speed.Create(20);
        // Act
        var result = _user.UpdateRobot(robot.RobotId, newModel, newSpeed);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Model.Value.Should().Be(newModel);
        result.Value.Speed.Should().Be(newSpeed);
        _user.Robots.Count.Should().Be(1);
        _user.Robots.Should().Contain(result.Value);
        result.Value.UserId.Should().Be(robot.UserId);
        result.Value.RobotId.Should().Be(robot.RobotId);
    }
}