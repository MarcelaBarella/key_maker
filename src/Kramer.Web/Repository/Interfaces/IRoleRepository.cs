using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kramer.Repository.Interfaces
{
    public interface IRoleRepository : IRepository<IdentityRole>
    {
        IdentityRole GetById(string id);
    }
}
