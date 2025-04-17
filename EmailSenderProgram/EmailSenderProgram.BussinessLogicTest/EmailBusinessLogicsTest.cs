using EmailSenderProgram.BussinessLogic;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EmailSenderProgram.BussinessLogicTest
{

    [TestFixture]
    public class EmailBusinessLogicsTest
    {
        private EmailBusinessLogics? _emailBusinessLogics;
        private Mock<IConfiguration>? _mockConfig;

        [SetUp]
        public void SetUp()
        {
            _mockConfig = new Mock<IConfiguration>();
            _emailBusinessLogics = new EmailBusinessLogics(_mockConfig.Object);
        }

        [Test]
        public async void EmailSend_Correct_Single_Mail_Type_ReturnTrue()
        {
            string[] emailTypes = new string[] { "WelcomeMail" };
            var result = await _emailBusinessLogics?.SendAllEmails(emailTypes);

            Assert.That(result, Is.True, "1 should not be prime");
        }
        [Test]
        public async void EmailSend_Correct_Multiple_Mail_Type_ReturnTrue()
        {
            string[] emailTypes = new string[] { "WelcomeMail", "ComebackMail" };
            var result = await _emailBusinessLogics?.SendAllEmails(emailTypes);

            Assert.That(result, Is.True, "1 should not be prime");
        }

        [Test]
        public async void EmailSend_Incorrect_Mail_Type_ReturnFalse()
        {
            string[] emailTypes = new string[] { "abcemail" };
            var result = await _emailBusinessLogics?.SendAllEmails(emailTypes);

            Assert.That(result, Is.False, "1 should not be prime");
        }
    }
}
