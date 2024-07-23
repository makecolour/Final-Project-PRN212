using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project
{
    internal class EmailService
    {
        private IConfigurationRoot _configuration;
        private string from;
        private string password;
        public EmailService(IConfiguration configuration)
        {
            this._configuration = configuration as IConfigurationRoot; // Adjust this line if necessary
            from = _configuration["SMTP:email"];
            password = _configuration["SMTP:password"];
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
           var client = new SmtpClient("smtp-mail.outlook.com")
           {
               Port = 587,
               Credentials = new NetworkCredential(from, password),
               EnableSsl = true
           };
           return client.SendMailAsync(new MailMessage(from: from, to: email, subject, message));
        }
    }
}
