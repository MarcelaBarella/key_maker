using System.Collections.Generic;
using Kramer.Models;

namespace Kramer.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetById(string userId);
        bool IsAdmin(string userId);
        List<ApplicationUser> GetAdmins();
    }
}
