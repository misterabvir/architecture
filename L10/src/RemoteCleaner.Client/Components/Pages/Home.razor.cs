using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace RemoteCleaner.Client.Components.Pages
{
    public partial class Home
    {
        private HubConnection? HubConnection { get; set; } = null!;
        private HttpClient HttpClient { get; set; } = null!;
        private List<Room> _rooms = [];
        private List<Log> _logs = [];
        private Station? _station;
        private int _selectedRoomId;
        private string? _progressMessage;
        private double _progress;
        private double _charge;
        private bool _disabled;
        private const string _baseAddress = "https://localhost:10000";


        protected override async Task OnInitializedAsync()
        {
            await StartConfigure();
        }

        private async Task StartConfigure()
        {
            HttpClient = new() { BaseAddress = new Uri($"{_baseAddress}") };
            _rooms = (await HttpClient.GetFromJsonAsync<List<Room>>("/rooms")) ?? [];
            _selectedRoomId = _rooms.FirstOrDefault()!.RoomId;
            _station = await HttpClient.GetFromJsonAsync<Station>("/station");

            HubConnection = new HubConnectionBuilder()
                .WithUrl($"{_baseAddress}/remote-hub")
                .Build();

            HubConnection.On<string, double>("Progress", OnProgress);
            HubConnection.On<double>("Battery", OnBattery);
            HubConnection.On("OnStarted", OnStarted);
            HubConnection.On("Completed", OnCompleted);

            await HubConnection.StartAsync();
        }

        private async Task OnProgress(string message, double progress)
        {
            _progress = progress;
            _progressMessage = message;
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnBattery(double charge)
        {
            _charge = charge;
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnStarted()
        {
            _disabled = true;
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnCompleted()
        {
            _disabled = false;
            _progressMessage = null;
            _rooms = (await HttpClient.GetFromJsonAsync<List<Room>>("/rooms")) ?? [];
            _selectedRoomId = _rooms.OrderByDescending(r=>r.CleanedAt).FirstOrDefault()!.RoomId;
            await InvokeAsync(StateHasChanged);
        }

        private async Task StartCleaning()
        {
            await HubConnection!.SendAsync("Start", _selectedRoomId);
        }

        private async Task GetLogs()
        {
            _logs = (await HttpClient.GetFromJsonAsync<List<Log>>("/log")) ?? [];
        }
        
    }
}