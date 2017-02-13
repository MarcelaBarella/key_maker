using System.Net;
using System.Net.Mail;

namespace Kramer.Helpers
{
    public class EmailSender : IEmailSender
    {
        private SmtpClient smtp;

        public EmailSender(string host, int port, string username, string password)
        {
            smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = port;
            smtp.Credentials = new NetworkCredential(username, password);
        }

        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void Send()
        {
            // o envio do e-mail será feito aqui utilizando o SMTP
        }
    }
}