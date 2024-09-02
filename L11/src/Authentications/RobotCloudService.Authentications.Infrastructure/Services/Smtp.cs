using MailKit.Net.Smtp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MimeKit;

using RobotCloudService.Authentications.Application.Common.Services;

namespace RobotCloudService.Authentications.Infrastructure.Services;

public static class Smtp
{
    public static IServiceCollection AddSmtp(this IServiceCollection services, IConfiguration configuration)
    {
        var smtpSettings = configuration.GetSection(Settings.SectionName).Get<Settings>()
            ?? throw new NullReferenceException($"Configuration section '{Settings.SectionName}' not found.");
        services.AddSingleton(smtpSettings);
        services.AddTransient<ISmtpService, Service>();
        return services;
    }
    
    
    
    internal class Settings
    {
        public const string SectionName = "Settings:Smtp";
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string Username { get; set; }
        public required string Address { get; set; }
        public required string Password { get; set; }
        public MailboxAddress From => new(Username, Address);
    }

    internal class Service(Settings settings) : ISmtpService
    {
        public async Task SendVerificationCodeAsync(string email, string code, CancellationToken cancellationToken)
        {
            var to = new MailboxAddress("", email);
            var message = new MimeMessage();
            message.From.Add(settings.From);
            message.To.Add(to);
            message.Subject = "RobotCloudService: Verify your email address";

            var htmlMessage = $"<p>Please enter the following code to verify your email address: <strong>{code}</strong></p>";

            message.Body = new TextPart("html") { Text = htmlMessage };

            using var client = new SmtpClient();
            await client.ConnectAsync(settings.Host, settings.Port, false, cancellationToken);
            await client.SendAsync(message);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}