using System.Collections.Generic;
namespace Kramer.Models
{
    public class UserRequestChangeStatusViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public StatusViewModel Status { get; set; }
        public List<StatusViewModel> Statuses { get; set; }
    }
}