using RobotCloudService.Remotes.Application;
using RobotCloudService.Remotes.Infrastructure;
using RobotCloudService.Remotes.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder
    .Build()
    .UsePresentation()
    .Run();
