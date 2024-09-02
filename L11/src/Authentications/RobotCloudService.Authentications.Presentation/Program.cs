using RobotCloudService.Authentications.Application;
using RobotCloudService.Authentications.Infrastructure;
using RobotCloudService.Authentications.Presentation;

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
