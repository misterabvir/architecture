using RemoteCleaner.Server;
using RemoteCleaner.Server.Application;
using RemoteCleaner.Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddServer();

builder.Build().UseServer().Run();

