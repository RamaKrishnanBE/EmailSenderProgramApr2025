namespace EmailSenderProgram.EmailService.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string fromEmail, string fromPassword, string toEmail, string subject, string mailBody);
    }
}
