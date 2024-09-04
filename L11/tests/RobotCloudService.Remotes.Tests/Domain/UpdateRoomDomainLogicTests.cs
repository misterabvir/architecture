using FluentAssertions;

using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users;

public class UpdateRoomDomainLogicTests
{
    private readonly User _user = User.Create(UserId.CreateUnique());

    [Fact]
    public void UpdateRoom_WhenRoomDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var title = Title.Create("Room 1");
        var area = Area.Create(10);
        var roomId = RoomId.CreateUnique();
        // Act
        var result = _user.UpdateRoom(roomId, title, area);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("UpdateRoom.NotFound");
    }

    [Fact]
    public void UpdateRoom_WhenRoomExist_ShouldBeReturnUpdatedRoom()
    {
        // Arrange
        var title = Title.Create("Room 1");
        var area = Area.Create(10);
        var room = _user.AddRoom(title, area).Value;
        var newTitle = Title.Create("Room 2");
        var newArea = Area.Create(20);
        // Act
        var result = _user.UpdateRoom(room.RoomId, newTitle, newArea);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Title.Value.Should().Be(newTitle);
        result.Value.Area.Should().Be(newArea);
        _user.Rooms.Count.Should().Be(1);
        _user.Rooms.Should().Contain(result.Value);
        result.Value.UserId.Should().Be(room.UserId);
        result.Value.RoomId.Should().Be(room.RoomId);
    }

}