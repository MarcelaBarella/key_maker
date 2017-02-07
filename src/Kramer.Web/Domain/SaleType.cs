using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Domain
{
    public class SaleType
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual List<UserRequest> UserRequests { get; set; }
    }
}