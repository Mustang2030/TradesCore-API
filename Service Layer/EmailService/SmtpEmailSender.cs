using Microsoft.Extensions.Configuration;
using Service_Layer.IEmailService;
using Data_Layer.Utilities;
using Data_Layer.Models;
using System.Net.Mail;
using System.Net;

namespace Service_Layer.EmailService
{
    public class SmtpEmailSender(IConfiguration configuration) : IEmailSender
    {
        private readonly EmailSettings _settings = new(configuration);

        public async Task<OperationResult<EmailSettings>> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(toEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.From = new MailAddress(_settings.SenderEmail, _settings.SenderName);

                using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.SenderEmail, _settings.Password),
                    EnableSsl = true
                };

                await client.SendMailAsync(message);

                return OperationResult<EmailSettings>.SuccessResult();
            }
            catch (Exception ex)
            {
                return OperationResult<EmailSettings>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }
    }
}
