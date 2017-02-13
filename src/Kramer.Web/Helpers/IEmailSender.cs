using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kramer.Helpers
{
    public interface IEmailSender
    {
        string To { get; set; }
        string From { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        void Send();
    }
}
