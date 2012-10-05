using System.Net;
using System.Net.Mail;

namespace mandrill.smtp
{
    public class MandrillSmtpClient : SmtpClient
    {
        public MandrillSmtpClient(string SMTPUsername, string APIKey, string host = "smtp.mandrillapp.com", int port = 587) : base(host, port)
        {
            this.Credentials = new NetworkCredential(SMTPUsername, APIKey);
            this.EnableSsl = true;
        }
    }
}
