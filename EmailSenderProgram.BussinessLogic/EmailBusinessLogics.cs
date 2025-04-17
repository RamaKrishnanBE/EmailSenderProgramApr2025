using EmailSenderProgram.BussinessLogic.Common;
using EmailSenderProgram.BussinessObject;
using EmailSenderProgram.EmailService.Interfaces;
using EmailSenderProgram.EmailService.Services;
using EmailSenderProgram.Utility.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EmailSenderProgram.BussinessLogic
{
    public class EmailBusinessLogics
    {
        private readonly IConfiguration _config;
        private readonly CustomersBusinessLogics _customersBusinessLogics;
        private readonly EmailTemplateBusinessLogics _emailTemplateBusinessLogics;
        public EmailBusinessLogics(IConfiguration config)
        {
            this._config = config;
            this._customersBusinessLogics = new CustomersBusinessLogics();
            this._emailTemplateBusinessLogics = new EmailTemplateBusinessLogics(_config);
        }
        public async Task<bool> SendAllEmails(string[] emailTypes)
        {
            try
            {
                bool isEmailSuccess = false;
                Email emailTemplateDetails;
                var toEmail = _config.GetSection(GlobalConstant.CONST_CONFIG_EMAIL_TO).Value ?? string.Empty;
                foreach (var emailType in emailTypes)
                {

                    emailTemplateDetails = _emailTemplateBusinessLogics.GetEmailTemplateDetails(emailType);
                    switch (emailType)
                    {
                        case GlobalConstant.CONST_MAIL_TYPE_WELCOME_MAIL:
                            isEmailSuccess = await SendWelcomeMail(_customersBusinessLogics, new WelcomeEmailService(_config), emailTemplateDetails, toEmail);
                            break;
                        case GlobalConstant.CONST_MAIL_TYPE_COMEBACK_MAIL:
#if DEBUG
                            //Debug mode, always send Comeback mail

                            isEmailSuccess = await SendComebackMail(_customersBusinessLogics, new ComebackEmailService(_config), emailTemplateDetails, toEmail);

#else                       //Every Sunday run Comeback mail
                            if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
                            {
                                isEmailSuccess = await SendComebackMail(_customersBusinessLogics, new ComebackEmailService(_config), emailTemplateDetails, toEmail);

                            }
#endif
                            break;
                        default:
                            //Email unknown
                            break;
                    }
                }
                return isEmailSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task<bool> SendWelcomeMail(CustomersBusinessLogics customersBusinessLogics, IEmailService emailService, Email emailTemplateDetails, string toEmail)
        {
            try
            {
                bool isEmailSuccess = false;

                var customers = customersBusinessLogics.GetCustomersForWelcomeMail();
                if (customers != null && customers.Any() && emailService != null && emailTemplateDetails != null)
                {
                    foreach (var customer in customers)
                    {
                        if (customer != null && EmailValidator.isValidEmail(customer.Email))
                        {
                            Log.Information($"Sending welcome mail for the customer {customer.Email}");

                            isEmailSuccess = await emailService.SendMail(customer, emailTemplateDetails, toEmail);
                            if (isEmailSuccess)
                            {
                                Log.Information($"Welcome mail sent successfully for {customer.Email}");
                            }
                            else
                            {
                                Log.Error($"Unable to send the welcome mail for {customer.Email}");
                            }
                        }
                        else
                        {
                            if (customer != null)
                            {
                                //Invalid Email Address
                                Log.Error($"Invalid email address provided {customer?.Email}");
                            }
                        }
                    }
                }

                return (isEmailSuccess);
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured while sending the welcome email. Error Message:  {ex.Message}");
                return false;
            }
        }
        private async Task<bool> SendComebackMail(CustomersBusinessLogics customersBusinessLogics, IEmailService emailService, Email emailTemplateDetails, string toEmail)
        {
            try
            {
                bool isEmailSuccess = false;
                var customers = customersBusinessLogics.GetCustomersForComebackMail();
                if (customers != null && customers.Any() && emailService != null && emailTemplateDetails != null)
                {
                    foreach (var customer in customers)
                    {
                        if (customer != null && EmailValidator.isValidEmail(customer.Email))
                        {
                            Log.Information($"Sending comeback mail for the customer {customer.Email}");
                            isEmailSuccess = await emailService.SendMail(customer, emailTemplateDetails, toEmail);
                            if (isEmailSuccess)
                            {
                                Log.Information($"Comeback mail sent successfully for {customer.Email}");
                            }
                            else
                            {
                                Log.Information($"Unable to send the comeback mail for {customer.Email}");
                            }
                        }
                        else
                        {
                            if (customer != null)
                            {
                                //Invalid Email Address
                                Log.Error($"Invalid email address provided {customer?.Email}");
                            }
                        }
                    }
                }

                return (isEmailSuccess);
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured while sending the comeback email. Error Message:  {ex.Message}");
                return false;
            }
        }
    }
}
