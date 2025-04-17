namespace EmailSenderProgram.BussinessObject
{
    public class Email
    {
        public string Name { get; set; } = string.Empty;
        public string EmailType { get; set; } = string.Empty;
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailBodyTemplate { get; set; } = string.Empty;
        public string EmailFrom { get; set; } = string.Empty;
        public string EmailFromPassword { get; set; } = string.Empty;
    }
}
