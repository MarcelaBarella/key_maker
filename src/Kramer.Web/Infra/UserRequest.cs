﻿using Kramer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Infra
{
    public class UserRequest
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Role { get; set; }
        public virtual string Username { get; set; }
        public virtual bool Pending { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }
    }
}