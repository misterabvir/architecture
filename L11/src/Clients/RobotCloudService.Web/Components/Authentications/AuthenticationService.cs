using RobotCloudService.Web.Components.Authentications.Models;
using RobotCloudService.Web.Components.Services;

namespace RobotCloudService.Web.Components.Authentications;

public interface IAuthenticationService
{
    Task<string?> Login(LoginAccountModel model);
    Task<bool> Register(RegisterAccountModel model);
    Task<string?> Confirm(ConfirmAccountModel model);
    Task<bool> Forgot(ForgotAccountModel model);
    Task<bool> Reset(ResetAccountModel model);
}


public class AuthenticationService(ISendService sendService, IConfiguration configuration) : IAuthenticationService
{
    private readonly string _host = configuration["Address:Api"]!;

    public async Task<string?> Confirm(ConfirmAccountModel model)
    {
        return await sendService.Command<string>(HttpMethod.Post, $"http://{_host}/authentication/confirm", model);
    }

    public async Task<bool> Forgot(ForgotAccountModel model)
    {
        return await sendService.Command(HttpMethod.Post, $"http://{_host}/authentication/forgot-password", model);
    }

    public async Task<string?> Login(LoginAccountModel model)
    {
        return await sendService.Command<string>(HttpMethod.Post, $"http://{_host}/authentication/login", model);
    }

    public async Task<bool> Register(RegisterAccountModel model)
    {
        return await sendService.Command(HttpMethod.Post, $"http://{_host}/authentication/register", model);
    }

    public async Task<bool> Reset(ResetAccountModel model)
    {
        return await sendService.Command(HttpMethod.Post, $"http://{_host}/authentication/reset-password", model);
    }
}
