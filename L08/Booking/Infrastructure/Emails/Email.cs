using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using NuGet.Protocol.Plugins;

namespace Booking.Infrastructure.Emails;

public static class Email
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(Settings.SectionName).Get<Settings>() ??
            throw new Exception("Email settings not configured");
        services.AddSingleton(settings);
        services.AddTransient<IEmailSender, Sender>();
        return services;
    }

    public class Settings
    {
        public const string SectionName = "Settings:Emails";
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string Username { get; set; }
        public required string Address { get; set; }
        public required string Password { get; set; }
        public MailboxAddress From => new(Username, Address);
    }


    internal class Sender(Settings settings) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var to = new MailboxAddress("", email);
            var message = new MimeMessage();
            message.From.Add(settings.From);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlMessage };

            using var client = new SmtpClient();
            await client.ConnectAsync(settings.Host, settings.Port, false);
            // await client.AuthenticateAsync(settings.Username, settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
