using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using Newtonsoft.Json;
using RobotCloudService.Web.Components.Authentications;
namespace RobotCloudService.Web.Components.Services;

public interface ISendService
{
    Task<T?> Command<T>(HttpMethod httpMethod, string url, object? body = null);
    Task<bool> Command(HttpMethod httpMethod, string url, object? body = null);
    Task<T?> Query<T>(string url);
}

internal class SendService(HttpClient httpClient, Authentication.Account account, ISnackbar snackbar) : ISendService
{
    public async Task<T?> Command<T>(HttpMethod httpMethod, string url, object? body = null)
    {
        var request = new HttpRequestMessage(httpMethod, url);
        request.Headers.Add("Authorization", $"Bearer {account.Token}");
        request.Content = JsonContent.Create(body);
        var response = await httpClient.SendAsync(request);
        return await GetResult<T>(response);
    }

    public async Task<bool> Command(HttpMethod httpMethod, string url, object? body = null)
    {
        var request = new HttpRequestMessage(httpMethod, url);
        request.Headers.Add("Authorization", $"Bearer {account.Token}");
        request.Content = JsonContent.Create(body);
        var response = await httpClient.SendAsync(request);
        return await GetResult(response);
    }

    public async Task<T?> Query<T>(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {account.Token}");
        var response = await httpClient.SendAsync(request);
        return await GetResult<T>(response);
    }

    private async Task<T?> GetResult<T>(HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var value = JsonConvert.DeserializeObject<T>(json);
            if (value is null)
            {
                return default;
            }
            return value;
        }
        var errors = JsonConvert.DeserializeObject<ProblemDetails>(json);
        if (errors is not null)
        {
            snackbar.Add($"{errors.Title}: {errors.Detail}", Severity.Error);
        }

        return default;
    }

    private async Task<bool> GetResult(HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        var errors = JsonConvert.DeserializeObject<ProblemDetails>(json);
        if (errors is not null)
        {
            snackbar.Add($"{errors.Title}: {errors.Detail}", Severity.Error);
        }

        return response.IsSuccessStatusCode;
    }
}
