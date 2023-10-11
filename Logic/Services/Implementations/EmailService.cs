using Logic.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Implementations
{
    public class EmailService:IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(string toEmail, string subject, string body)
        {
            
            string smtpHost = "smtp.gmail.com"; 
            int smtpPort = 587;
            string senderEmail = _configuration["email"];
            string senderPassword = _configuration["password"]; 

            MailMessage mail = new MailMessage(senderEmail, toEmail, subject, body);
            mail.IsBodyHtml = true; 
            
            
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.EnableSsl = true; 
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);


            try
            {
                client.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
