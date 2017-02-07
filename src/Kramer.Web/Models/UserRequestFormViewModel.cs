using Kramer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Models
{
    public class UserRequestFormViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
    }
}