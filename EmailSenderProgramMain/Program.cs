
using EmailSenderProgram.BussinessLogic;
using EmailSenderProgramMain.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;

IConfiguration config = CommonHelpers.GetAppConfig(args);
Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

Log.Information("Starting Email Sender Program");

EmailTypesBusinessLogics emailTypesBusinessLogics = new EmailTypesBusinessLogics(config);
var availableEmailTypes = emailTypesBusinessLogics.GetAvailableEmailTypes();


if (availableEmailTypes != null && availableEmailTypes.Any())
{
    Log.Information($"Available email types: {availableEmailTypes.Length}");

    EmailBusinessLogics emailBusinessLogics = new EmailBusinessLogics(config);
    bool success = await emailBusinessLogics.SendAllEmails(availableEmailTypes);
    //Check if the sending went OK
    if (success == true)
    {
        Log.Information("All mails are sent, I hope...");
    }
    else
    {
        Log.Error("Oops, something went wrong when sending mail (I think...)");
    }
    Console.ReadKey();

}
