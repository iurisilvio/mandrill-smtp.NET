using System.Dynamic;
using System.Net.Mail;
using mandrill.smtp;
using System.Configuration;

namespace mandrill.console
{
    class Program
    {
        private static string _config(string key)
        {
            return ConfigurationSettings.AppSettings[key];
        }

        static void Main(string[] args)
        {
            string SMTPUsername = _config("MandrillUser");
            string APIKey = _config("MandrillKey");

            var client = new MandrillSmtpClient(SMTPUsername, APIKey);

            MandrillMailMessage message = new MandrillMailMessage();
            message.From = new MailAddress(_config("From"));
            var to = _config("To");
            message.To.Add(to);

            message.MandrillHeader.Template = _config("MandrillTemplate");
            message.MandrillHeader.PreserveRecipients = false;

            // set global merge vars
            dynamic e1 = new ExpandoObject();
            e1.var1 = "test1";
            e1.var2 = "test2";
            message.MandrillHeader.MergeVars.Add(e1);

            // set merge vars to one recipient
            dynamic e2 = new ExpandoObject();
            e2.var2 = "override test2";
            e2._rcpt = to.Split(',')[0];
            message.MandrillHeader.MergeVars.Add(e2);

            message.Subject = "Test message";

            client.Send(message);
        }
    }
}
