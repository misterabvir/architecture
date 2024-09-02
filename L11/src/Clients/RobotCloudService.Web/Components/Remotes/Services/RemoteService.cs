using RobotCloudService.Web.Components.Remotes.Models;
using RobotCloudService.Web.Components.Services;

namespace RobotCloudService.Web.Components.Remotes.Services;

public class RemoteService(IConfiguration configuration, ISendService sendService) : IRemoteService
{
    private readonly string _host = configuration["Address:Remote"]!;

    public async Task<Robot?> AddRobot(AddRobot model)
    {
        return await sendService.Command<Robot>(HttpMethod.Post, $"http://{_host}/robots/add", model);
    }

    public async Task<Room?> AddRoom(AddRoom model)
    {
        return await sendService.Command<Room>(HttpMethod.Post, $"http://{_host}/rooms/add", model);
    }
    public async Task<List<Log>?> GetLogs()
    {
        return await sendService.Query<List<Log>>($"http://{_host}/users/logs");
    }

    public async Task<UserData?> GetData()
    {
        return await sendService.Query<UserData>($"http://{_host}/users/data");
    }


    public async Task<Robot?> Start(StartCleanModel model)
    {
        return await sendService.Command<Robot>(HttpMethod.Put, $"http://{_host}/robots/start", model);
    }
}