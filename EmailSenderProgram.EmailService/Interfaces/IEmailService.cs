using EmailSenderProgram.BussinessObject;
using EmailSenderProgram.DataAccess;

namespace EmailSenderProgram.EmailService.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendMail(Customer customer, Email emailTemplateDetails, string toEmail = "");
    }
}
