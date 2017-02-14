using System;
using System.Net;
using System.Net.Mail;

namespace Kramer.Helpers
{
    public class EmailSender : IEmailSender
    {
        private SmtpClient smtp;
        private bool throwError;

        public EmailSender(string host, int port, string username, string password, bool enableSsl, bool throwError)
        {
            smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = port;
            smtp.Credentials = new NetworkCredential(username, password);
            smtp.EnableSsl = enableSsl;
            this.throwError = throwError;
        }

        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void Send()
        {
            try
            {
                MailMessage mail = new MailMessage(From, To);
                mail.Subject = Subject;
                mail.Body = Body;
                smtp.Send(mail);
            }
            catch
            {
                if (throwError)
                    throw;
            }
        }
    }
}