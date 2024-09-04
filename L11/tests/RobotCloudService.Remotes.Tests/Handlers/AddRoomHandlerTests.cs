using Castle.DynamicProxy;

using FluentAssertions;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Application.Users.Commands;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Tests.Handlers;

public class AddRoomHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AddRoom.Handler _handler;
    private readonly User _user = User.Create(UserId.CreateUnique());

    public AddRoomHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new AddRoom.Handler(_unitOfWork);
    }

    [Fact]
    public async Task WhenUserNotExist_ReturnNotFoundError()
    {
        //arrange
        var command = new AddRoom.Command(UserId.CreateUnique(), "title", Area: 10);
        _unitOfWork.Users.GetByIdAsync(UserId.CreateUnique(), default).ReturnsNull();
        //act

        var result = await _handler.Handle(command, default);
        
        //aserts
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be("AddRoom.UserNotFound");
    }

    [Fact]
    public async Task WhenUserExist_ReturnDomainLogicResult()
    {
        //arrange

        var command = new AddRoom.Command(_user.UserId, "title", Area: 10);
        _unitOfWork.Users.GetByIdAsync(_user.UserId, default).Returns(_user);
        
        //act
        var result = await _handler.Handle(command, default);

        //aserts
        await _unitOfWork.Received(1).SaveChangesAsync(default);
        result.IsSuccess.Should().BeTrue();
    }
}
