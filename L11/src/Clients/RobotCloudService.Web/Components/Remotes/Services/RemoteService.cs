using RobotCloudService.Web.Components.Remotes.Models;
using RobotCloudService.Web.Components.Services;

namespace RobotCloudService.Web.Components.Remotes.Services;

public class RemoteService(IConfiguration configuration, ISendService sendService) : IRemoteService
{
    private readonly string _host = configuration["Address:Api"]!;

    public async Task<RobotModel?> AddRobot(AddRobotModel model)
    {
        return await sendService.Command<RobotModel>(HttpMethod.Post, $"http://{_host}/robots/add", model);
    }

    public async Task<RoomModel?> AddRoom(AddRoomModel model)
    {
        return await sendService.Command<RoomModel>(HttpMethod.Post, $"http://{_host}/rooms/add", model);
    }
    public async Task<List<LogModel>?> GetLogs()
    {
        return await sendService.Query<List<LogModel>>($"http://{_host}/users/logs");
    }

    public async Task<UserDataModel?> GetData()
    {
        return await sendService.Query<UserDataModel>($"http://{_host}/users/data");
    }


    public async Task<RobotModel?> Start(StartCleanModel model)
    {
        return await sendService.Command<RobotModel>(HttpMethod.Put, $"http://{_host}/robots/start", model);
    }
}