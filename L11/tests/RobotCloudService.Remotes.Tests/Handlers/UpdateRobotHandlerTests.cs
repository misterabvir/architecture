using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;


using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Application.Users.Commands;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Tests.Handlers;

public class UpdateRobotHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateRobot.Handler _handler;
    private readonly User _user = User.Create(UserId.CreateUnique());

    public UpdateRobotHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new UpdateRobot.Handler(_unitOfWork);
        _user.AddRoom(Title.Create("Room"), Area.Create(10));
    }

    [Fact]
    public async Task WhenUserNotExist_ReturnNotFoundError()
    {
        //arrange
        var command = new UpdateRobot.Command(UserId.CreateUnique(), RobotId.CreateUnique(), "model", Speed: 1);
        _unitOfWork.Users.GetByIdAsync(UserId.CreateUnique(), default).ReturnsNull();
        //act

        var result = await _handler.Handle(command, default);
        
        //aserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("UpdateRobot.UserNotFound");
    }

    [Fact]
    public async Task WhenUserExist_ReturnDomainLogicResult()
    {
        //arrange
        var robot = _user.AddRobot(Model.Create("model"), Speed.Create(1)).Value;
        var command = new UpdateRobot.Command(_user.UserId, robot.RobotId, "model", Speed: 1);
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).Returns(_user);
        
        //act
        var result = await _handler.Handle(command, default);

        //aserts
        await _unitOfWork.Received(1).SaveChangesAsync(default);
        result.IsSuccess.Should().BeTrue();
    }
}
