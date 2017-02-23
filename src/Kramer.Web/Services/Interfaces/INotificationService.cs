using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kramer.Services.Interfaces
{
    public interface INotificationService
    {
        void SendConfirmationToRequester(string requesterEmail, string requesterName, string userEmail);
        void SendCredentialsToUser(string userEmail, string userName, string login, string password);
        void SendCancellationToRequester(string requesterEmail, string requesterName, string userEmail);
        void SendEditedToUser(string requesterEmail, string currentUserEmail);
    }
}
