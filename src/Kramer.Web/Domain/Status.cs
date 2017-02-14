using System.Collections.Generic;

namespace Kramer.Domain
{
    public class Status
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual List<UserRequest> UserRequests { get; set; }
    }
}