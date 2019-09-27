using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Accio.BLL.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Accio.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(EmailSettings settings)
        {
            _settings = settings;

        }

        public async Task SendEmailAsync(string toAddress, EmailBody body = null, string ccAddress = null)
        {
            if (string.IsNullOrEmpty(toAddress))
            {
                throw new ArgumentNullException("No recipient address have been configured.");
            }

            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                Subject = body.Subject
            };

            emailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));

            if (body.IsHtml)
            {
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = body.Text };
            }
            else
            {
                emailMessage.Body = new TextPart(TextFormat.Plain) { Text = body.Text };
            }

            var toAddressList = new[] { toAddress };

            toAddressList.Select(to =>
            {
                emailMessage.To.Add(new MailboxAddress(to));
                return to;
            });

            if (!string.IsNullOrEmpty(ccAddress))
            {
                var ccAddressList = new[] { ccAddress };
                ccAddressList.Select(cc =>
                {
                    emailMessage.Cc.Add(new MailboxAddress(cc));
                    return cc;
                });
            }

            try {
                using(var smtp = new MailKit.Net.Smtp.SmtpClient()){
                    var socketOption =  _settings.EnableSsl? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOption);
                    
                    if(!string.IsNullOrEmpty(_settings.SmtpUserName)) {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    await smtp.SendAsync(emailMessage);
                    await smtp.DisconnectAsync(true);
                }

            } catch (SmtpException ex) {
                throw ex;
            }
        }
    }
}