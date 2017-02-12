using Kramer.Models;
using Kramer.Repository.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Repository
{
    public class RoleRepository : RepositoryBase<IdentityRole>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext db) : base(db) {}

        public IdentityRole GetById(string id)
        {
            return db.Roles.Find(id);
        }
    }
}