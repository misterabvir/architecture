using Microsoft.Extensions.Hosting;
using DataAccessLayer;
using BusinessLogicLayer;
using UI;
using Microsoft.Extensions.DependencyInjection;
using UI.Menus;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddDLL().AddBLL().AddUI();

var host = builder.Build();

#pragma warning disable CS4014
host.RunAsync();
#pragma warning restore CS4014

await ConsoleApp.RunAsync(host.Services.GetRequiredService<MainMenu>());

await host.StopAsync();