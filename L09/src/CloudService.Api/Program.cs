using CloudService.Api;
using CloudService.Application;
using CloudService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();


builder.Build().UsePresentation().Run();

