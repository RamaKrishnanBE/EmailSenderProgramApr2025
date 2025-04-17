using EmailSenderProgram.BussinessObject;
using EmailSenderProgram.DataAccess;
using EmailSenderProgram.EmailService.Common;
using EmailSenderProgram.EmailService.Interfaces;
using EmailSenderProgram.Utility.NewFolder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EmailSenderProgram.EmailService.Services
{
    public class ComebackEmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        private readonly string _voucherCode;
        public ComebackEmailService(IConfiguration config)
        {
            this._config = config;
            this._emailSender = new EmailSender(config);
            this._voucherCode = _config.GetSection(GlobalConstant.CONST_CONFIG_VOUCHER_CODE).Value ?? string.Empty;
        }
        public async Task<bool> SendMail(Customer customer, Email emailTemplateDetails, string toEmail = "")
        {
            try
            {
                bool success = false;
                toEmail = !string.IsNullOrEmpty(toEmail) ? toEmail : (customer?.Email ?? string.Empty);
                Dictionary<string, string> placeholdersAndValues = new Dictionary<string, string>();
                placeholdersAndValues.Add(GlobalConstant.CONST_PLACEHOLDER_RECEIVERNAME, (customer?.Email ?? string.Empty));
                placeholdersAndValues.Add(GlobalConstant.CONST_PLACEHOLDER_VOUCHER, _voucherCode);

                var emailBody = await GetEmailBodyContent(emailTemplateDetails, placeholdersAndValues);

                //Sending email through SMTP server
                success = await _emailSender.SendEmail(emailTemplateDetails.EmailFrom, emailTemplateDetails.EmailFromPassword, toEmail, emailTemplateDetails.EmailSubject, emailBody);
                return success;
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured while sending the welcome email. Error Message:  {ex.Message}");
                return false;
            }
        }
        public async Task<string> GetEmailBodyContent(Email emailTemplateDetails, Dictionary<string, string> placeholdersAndValues)
        {
            var dir = Directory.GetCurrentDirectory();
            dir += GlobalConstant.CONST_EMAIL_TEMPLATE_PATH;
            var emailTemplatePath = Path.Combine(dir, emailTemplateDetails.EmailBodyTemplate);
            var emailBody = EmailContentReplacer.ReplacePlaceHolders(await File.ReadAllTextAsync(emailTemplatePath), placeholdersAndValues);
            return emailBody;
        }
    }
}
