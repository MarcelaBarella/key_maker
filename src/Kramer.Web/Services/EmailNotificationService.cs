using Kramer.Helpers;
using Kramer.Models;
using Kramer.Repository;
using Kramer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Services
{
    public class EmailNotificationService : INotificationService
    {
        private IEmailSender emailSender;
        private IUserRepository userRepository;

        public EmailNotificationService(IEmailSender emailSender, IUserRepository userRepository)
        {
            this.emailSender = emailSender;
            this.userRepository = userRepository;
        }

        public void SendConfirmationToRequester(string requesterEmail, string requesterName, string userEmail)
        {
            emailSender.To = requesterEmail;
            emailSender.Subject = "Global Payments - Credenciais de Acesso";
            emailSender.Body =
                "Olá " + requesterEmail
                + "</br ></br>"
                + "O usuário para o e-mail " + userEmail + " já foi criado e as credenciais de acesso foram enviadas para o usuário.";
            emailSender.Send();
        }

        public void SendCredentialsToUser(string userEmail, string userName, string login, string password)
        {
            emailSender.To = userEmail;
            emailSender.Subject = "Global Payments - Credenciais de Acesso";
            emailSender.Body =
                "Olá " + userName
                + "</br ></br>"
                + "Você está recebendo este email por solicitação da Global Payments. </br>"
                + "Abaixo estão suas credenciais para acessar o Portal de Serviços da Global Payments.Elas devem ser usadas exclusivamente por você, e não devem ser compartilhadas com outras pessoas.</br>"
                + "https://portaldeservicos.globalpagamentos.com.br/Pages/Login-global.aspx?x=7A41CA43-8BED-4975-9EB8-FFED74B5228F</br>"
                + "</br> Login: " + login + "</br> Senha: " + password
                + "</br> Troque sua senha ao acessar o portal.</br>"
                + "Qualquer dúvida operacional, entre em contato com a Global Payments.";
            emailSender.Send();
        }


        public void SendCancellationToRequester(string requesterEmail, string requesterName, string userEmail)
        {
            emailSender.To = requesterEmail;
            emailSender.Subject = "Global Payments - Solicitação Cancelada";
            emailSender.Body =
                "Olá " + requesterEmail
                + "</br></br>"
                + "Informamos-lhe que a sua solicitação para a criação de usuário para o e-mail " + userEmail + " foi cancelada.</br>"
                + "Para mais informações, por favor, entre em contato com o administrador do sistema.";
            emailSender.Send();
        }

        //Current User
        public void SendEditedToUser(string requesterEmail, string currentUserEmail)
        {
            emailSender.To = currentUserEmail;
            emailSender.Subject = "Global Payments - Credenciais de Acesso";
            emailSender.Body =
                "Olá "
                + "</br ></br>"
                + "A solicitação criada pelo usuário " + requesterEmail + " foi editada, favor verifica-la";
            emailSender.Send();
        }

        public void SendEditedToAdmins(string userEdited, string currentUser)
        {
            var admins = userRepository.GetAdmins().Where(admin => admin.UserName != currentUser);

            emailSender.Subject = "Global Payments - Solicitação Alterada";
            emailSender.Body = "Olá "
                               + "</br></br>"
                               + "A solicitação criada para criação do usuário " + userEdited + " foi alterada.";

            foreach (var admin in admins)
            {
                emailSender.To = admin.Email;
                emailSender.Send();
            }
        }
     
    }
}