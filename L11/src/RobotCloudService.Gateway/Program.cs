using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var ocelotCondiguration = new ConfigurationBuilder()
    .AddJsonFile("ocelot.json")
    .Build();

builder.Services.AddOcelot(ocelotCondiguration);
builder.Services.AddSwaggerForOcelot(ocelotCondiguration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerForOcelotUI(options => options.PathToSwaggerGenerator = "/swagger/docs")
        .UseOcelot()
        .Wait();
}

app.Run();
