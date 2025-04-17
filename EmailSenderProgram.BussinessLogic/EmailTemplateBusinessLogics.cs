using EmailSenderProgram.BussinessLogic.Common;
using EmailSenderProgram.BussinessObject;
using Microsoft.Extensions.Configuration;

namespace EmailSenderProgram.BussinessLogic
{
    public class EmailTemplateBusinessLogics
    {
        private readonly IConfiguration _config;
        public EmailTemplateBusinessLogics(IConfiguration config)
        {
            this._config = config;
        }
        public Email GetEmailTemplateDetails(string emailType)
        {
            var emailTemplateDetails = new Email();
            emailTemplateDetails.Name = _config.GetSection(string.Format(GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_PATH, emailType, GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_NAME)).Value ?? string.Empty;
            emailTemplateDetails.EmailType = _config.GetSection(string.Format(GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_PATH, emailType, GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_EMAILTYPE)).Value ?? string.Empty;
            emailTemplateDetails.EmailSubject = _config.GetSection(string.Format(GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_PATH, emailType, GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_EMAILSUBJECT)).Value ?? string.Empty;
            emailTemplateDetails.EmailBodyTemplate = _config.GetSection(string.Format(GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_PATH, emailType, GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_EMAILBODYTEMPLATE)).Value ?? string.Empty;

            emailTemplateDetails.EmailFrom = _config.GetSection(string.Format(GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_PATH, emailType, GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_EMAIL_FROM)).Value ?? string.Empty;
            emailTemplateDetails.EmailFromPassword = _config.GetSection(string.Format(GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_PATH, emailType, GlobalConstant.CONST_CONFIG_EMAIL_TEMPLATE_FIELD_EMAIL_FROM_PASSWORD)).Value ?? string.Empty;

            return emailTemplateDetails;
        }
    }
}
