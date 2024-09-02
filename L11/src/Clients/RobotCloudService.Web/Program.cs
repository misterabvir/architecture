using MassTransit;

using Microsoft.AspNetCore.Identity.UI.Services;

using MudBlazor;
using MudBlazor.Services;

using RobotCloudService.Web.Components;
using RobotCloudService.Web.Components.Authentications;
using RobotCloudService.Web.Components.Remotes.Consumers;
using RobotCloudService.Web.Components.Remotes.Services;
using RobotCloudService.Web.Components.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddMudServices(config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.NewestOnTop = false;
        config.SnackbarConfiguration.ShowCloseIcon = true;
        config.SnackbarConfiguration.VisibleStateDuration = 10000;
        config.SnackbarConfiguration.HideTransitionDuration = 500;
        config.SnackbarConfiguration.ShowTransitionDuration = 500;
        config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    }
);
builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<CleaningStartedConsumer>();
    options.AddConsumer<CleaningStoppedConsumer>();
    options.SetKebabCaseEndpointNameFormatter();
    options.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("QueueConnection") ?? throw new Exception("RabbitMq connection string not found"));
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddAuthenticationState(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRemoteService, RemoteService>();
builder.Services.AddScoped<ISendService, SendService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
