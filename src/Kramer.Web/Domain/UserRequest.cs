﻿using Kramer.Models;

namespace Kramer.Domain
{
    public class UserRequest
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Username { get; set; }
        public virtual bool Pending { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }
        public virtual SaleType SaleType { get; set; }
        public virtual int SaleTypeId { get; set; }
    }
}