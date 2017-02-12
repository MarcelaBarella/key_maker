using Kramer.Domain;
using Kramer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kramer.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetById(string userId);
        bool IsAdmin(string userId);
    }
}
