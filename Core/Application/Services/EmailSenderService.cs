using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class EmailSenderService
{
    private readonly IConfiguration _configuration;

    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendAsync(
        string recipient,
        string subject,
        string body)
    {
        if (string.IsNullOrWhiteSpace(recipient))
            return false;

        var host = _configuration["Email:SmtpHost"];
        var portText = _configuration["Email:SmtpPort"];
        var username = _configuration["Email:Username"];
        var password = _configuration["Email:Password"];
        var from = _configuration["Email:From"];

        if (string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(portText) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(from))
        {
            return false;
        }

        if (!int.TryParse(portText, out var port))
            return false;

        var message = new MimeMessage();

        message.From.Add(
            new MailboxAddress(
                _configuration["Email:DisplayName"] ?? "Real Estate",
                from));

        message.To.Add(
            MailboxAddress.Parse(recipient));

        message.Subject = subject ?? "";

        message.Body = new TextPart("plain")
        {
            Text = body ?? ""
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            host,
            port,
            SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            username,
            password);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);

        return true;
    }
}
