using ChatbotAdmin.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ChatbotAdmin.Services
{
    public class EmailSenderService
    {
        private readonly ApplicationConfig _appConfig;

        public EmailSenderService(IOptions<ApplicationConfig> option)
        {
            _appConfig = option.Value;
        }

        public bool Send(string to, string subject, string content)
        {
            try
            { 
                var smtpClient = new SmtpClient
                {
                    Host = _appConfig.SmtpHost,
                    Port = _appConfig.SmtpPort,
                    EnableSsl = _appConfig.SmtpStartEnable,
                    Credentials = new NetworkCredential(_appConfig.SmtpUsername, _appConfig.SmtpPassword)
                };

                var mailMessage = new MailMessage(_appConfig.SmtpUsername, to);
                mailMessage.Subject = subject;
                mailMessage.Body = content;
                mailMessage.IsBodyHtml = true;
                

                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
