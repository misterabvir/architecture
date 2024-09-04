using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Application.Users.Commands;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Tests.Handlers;

public class AddRobotHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AddRobot.Handler _handler;
    private readonly User _user = User.Create(UserId.CreateUnique());

    public AddRobotHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new AddRobot.Handler(_unitOfWork);
        _user.AddRoom(Title.Create("Room"), Area.Create(10));
    }

    [Fact]
    public async Task WhenUserNotExist_ReturnNotFoundError()
    {
        //arrange
        var command = new AddRobot.Command(UserId.CreateUnique(), "model", Speed: 1);
        _unitOfWork.Users.GetByIdAsync(UserId.CreateUnique(), default).ReturnsNull();
        //act

        var result = await _handler.Handle(command, default);
        
        //aserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("AddRobot.UserNotFound");
    }

    [Fact]
    public async Task WhenUserExist_ReturnDomainLogicResult()
    {
        //arrange

        var command = new AddRobot.Command(_user.UserId, "model", Speed: 1);
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).Returns(_user);
        
        //act
        var result = await _handler.Handle(command, default);

        //aserts
        await _unitOfWork.Received(1).SaveChangesAsync(default);
        result.IsSuccess.Should().BeTrue();
    }
}
