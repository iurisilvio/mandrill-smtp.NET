using System.Net.Mail;
using mandrill.smtp.helpers;

namespace mandrill.smtp
{
    public class MandrillMailMessage : MailMessage
    {
        private MandrillHeader _mandrillHeader;
        public MandrillHeader MandrillHeader
        {
            get
            {
                return _mandrillHeader ?? (_mandrillHeader = new MandrillHeader(Headers));
            }
        }

        public MandrillMailMessage()
            : base()
        {

        }

        public MandrillMailMessage(string from, string to)
            : base(from, to)
        {

        }

        public MandrillMailMessage(string from, string to, string subject, string body)
            : base(from, to, subject, body)
        {

        }

        public MandrillMailMessage(MailAddress from, MailAddress to)
            : base(from, to)
        {

        }
    }
}
