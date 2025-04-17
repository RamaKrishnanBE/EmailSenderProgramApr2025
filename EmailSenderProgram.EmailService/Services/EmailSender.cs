using EmailSenderProgram.EmailService.Common;
using EmailSenderProgram.EmailService.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Net;
using System.Net.Mail;

namespace EmailSenderProgram.EmailService.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly string _emailHost;
        private readonly int _emailPort;

        public EmailSender(IConfiguration config)
        {
            this._config = config;
            _emailHost = _config.GetSection(GlobalConstant.CONST_CONFIG_EMAIL_HOST).Value ?? string.Empty;
            _emailPort = Convert.ToInt32(_config.GetSection(GlobalConstant.CONST_CONFIG_EMAIL_PORT).Value ?? string.Empty);

        }

        public async Task<bool> SendEmail(string fromEmail, string fromPassword, string toEmail, string subject, string mailBody)
        {
            try
            {

                var fromAddress = new MailAddress(fromEmail, string.Empty);
                var toAddress = new MailAddress(toEmail, string.Empty);

                var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = mailBody,
                    IsBodyHtml = true
                };


                var smtp = new SmtpClient
                {
                    Host = _emailHost,
                    Port = _emailPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = mailMessage)
                {
                    await smtp.SendMailAsync(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured while sending the email using the Host {_emailHost} and port {_emailPort}. Error Message:  {ex.Message}");
                return false;
            }
        }
    }
}
