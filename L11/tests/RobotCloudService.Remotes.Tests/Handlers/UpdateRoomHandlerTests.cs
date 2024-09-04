using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;


using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Application.Users.Commands;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Tests.Handlers;

public class UpdateRoomHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateRoom.Handler _handler;
    private readonly User _user = User.Create(UserId.CreateUnique());

    public UpdateRoomHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new UpdateRoom.Handler(_unitOfWork);
    }

    [Fact]
    public async Task WhenUserNotExist_ReturnNotFoundError()
    {
        //arrange
        var command = new UpdateRoom.Command(UserId.CreateUnique(), RoomId.CreateUnique(), "Room", Area: 1);
        _unitOfWork.Users.GetByIdAsync(UserId.CreateUnique(), default).ReturnsNull();
        //act
        var result = await _handler.Handle(command, default);

        //aserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("UpdateRoom.UserNotFound");
    }

    [Fact]
    public async Task WhenUserExist_ReturnDomainLogicResult()
    {
        //arrange
        var room = _user.AddRoom(Title.Create("Room"), Area.Create(10)).Value;
        var command = new UpdateRoom.Command(_user.UserId, room.RoomId, "Room", Area: 1);
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).Returns(_user);

        //act
        var result = await _handler.Handle(command, default);

        //aserts
        await _unitOfWork.Received(1).SaveChangesAsync(default);
        result.IsSuccess.Should().BeTrue();
    }
}