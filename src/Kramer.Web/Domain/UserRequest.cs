using Kramer.Models;
using System;

namespace Kramer.Domain
{
    public class UserRequest
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Name { get; set; }
        public virtual string Username { get; set; }
        public virtual Status Status { get; set; }
        public virtual int StatusId { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }
        public virtual SaleType SaleType { get; set; }
        public virtual int? SaleTypeId { get; set; }
        public virtual DateTime RequestDate { get; set; }
    }
}