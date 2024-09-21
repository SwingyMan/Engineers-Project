using Azure;
using Azure.Communication.Email;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure;

public class EmailSender : IEmailSender
{
    private readonly EmailClient _emailClient;

    public EmailSender(EmailClient emailClient)
    {
        _emailClient = emailClient;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await _emailClient.SendAsync(
            WaitUntil.Completed,
            "DoNotReply@polslsocial.pl",
            email,
            subject,
            $"<html><h1>{htmlMessage}</h1l></html>",
            htmlMessage);
    }
}