using System.Net.Mail;

namespace Kramer.Helpers
{
    public class EmailSender : IEmailSender
    {
        private SmtpClient smtp;

        /*As informações que você precisar para configurar  o SMTP, você recebe
         * via construtor */
        public EmailSender(/*string host, int port, ...*/)
        {
            smtp = new SmtpClient();
            smtp.Host = "";
            smtp.Port = 0;
            // e as outras configuracoes do smtp vão aqui
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