using RobotCloudService.Web.Components.Remotes.Models;

namespace RobotCloudService.Web.Components.Remotes.Services;

public interface IRemoteService
{
    Task<UserData?> GetData();
    Task<List<Log>?> GetLogs();
    Task<Robot?> AddRobot(AddRobot model);
    Task<Room?> AddRoom(AddRoom model);
    Task<Robot?> Start(StartCleanModel model);

}
