using Kramer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Repository
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        const string ADMIN = "1";
        public UserRepository(ApplicationDbContext db) : base(db) {}

        public ApplicationUser GetById(string userId)
        {
            return db.Users.Find(userId);
        }

        public bool IsAdmin(string userId)
        {
            var user = GetById(userId);
            return user.Roles.Any(role => role.RoleId == ADMIN);
        }

        public List<ApplicationUser> GetAdmins()
        {
            return db.Users.Where(user => user.Roles.Any(role => role.RoleId == ADMIN)).ToList();
        }
    }
}