using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace TicketBom.Infra.Email
{
   
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Smtp:Host"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["Smtp:Host"], Convert.ToInt32(_configuration["Smtp:Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["Smtp:User"], _configuration["Smtp:Pass"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }

    public interface IEmailService
    {
        void Send(string to, string subject, string html);
    }

}
