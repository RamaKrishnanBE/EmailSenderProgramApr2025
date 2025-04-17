using EmailSenderProgram.BussinessLogic.Common;
using Microsoft.Extensions.Configuration;

namespace EmailSenderProgram.BussinessLogic
{
    public class EmailTypesBusinessLogics
    {
        private readonly IConfiguration _config;
        public EmailTypesBusinessLogics(IConfiguration config)
        {

            this._config = config;
        }

        public string[]? GetAvailableEmailTypes()
        {
            string emailTypes = _config.GetSection(GlobalConstant.CONST_CONFIG_EMAIL_TYPES).Value ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(emailTypes))
            {
                return emailTypes.Split(',');
            }
            else
            {
                return null;
            }

        }
    }
}
