using Application;
using Infrastructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();
builder.Build().UsePresentation().Run();

