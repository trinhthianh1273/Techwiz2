﻿using MimeKit;
using System.Net.Mail;

namespace SoccerManager.UtinityService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailModel email)
        {
            var emailMessage = new MimeMessage();
            var from = _config["EmailSettings:From"];
            emailMessage.From.Add(new MailboxAddress("Angular Auth", from));
            emailMessage.To.Add(new MailboxAddress(email.To, email.To));
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(email.Content)
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    //client.Connect(_config["EmailSettings:SmtpServer"], 465, true);
                    //client.Authenticate(_config["EmailSettings:From"], _config["EmailSettings:Password"]);
                    //client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    //client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
