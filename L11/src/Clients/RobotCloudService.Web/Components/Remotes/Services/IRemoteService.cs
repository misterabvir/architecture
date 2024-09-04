using RobotCloudService.Web.Components.Remotes.Models;

namespace RobotCloudService.Web.Components.Remotes.Services;

public interface IRemoteService
{
    Task<UserDataModel?> GetData();
    Task<List<LogModel>?> GetLogs();
    Task<RobotModel?> AddRobot(AddRobotModel model);
    Task<RoomModel?> AddRoom(AddRoomModel model);
    Task<RobotModel?> Start(StartCleanModel model);

}
