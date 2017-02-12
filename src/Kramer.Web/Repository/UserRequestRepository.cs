using System.Collections.Generic;
using System.Data.Entity;
using Kramer.Domain;
using Kramer.Models;

namespace Kramer.Repository
{
    public class UserRequestRepository : RepositoryBase<UserRequest>, IUserRequestRepository
    {
        public UserRequestRepository(ApplicationDbContext db) : base(db) {}

        public void ChangeStatus(int id)
        {
            var userRequest = GetById(id);
            userRequest.Pending = !userRequest.Pending;
            Update(userRequest);
        }
    }
}