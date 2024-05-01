using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;

namespace Infrastructure
{
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
                senderAddress: "DoNotReply@polslsocial.pl",
                recipientAddress: email,
                subject: subject,
                htmlContent: $"<html><h1>{htmlMessage}</h1l></html>",
                plainTextContent: htmlMessage);
        }
    }
}
