using System;
using Kramer.Domain;
using Kramer.Models;

namespace Kramer.Repository
{
    public class UserRequestRepository : RepositoryBase<UserRequest>, IUserRequestRepository
    {
        public UserRequestRepository(ApplicationDbContext db) : base(db) {}

        public void ChangeStatus(int userId, int statusId)
        {
            var userRequest = GetById(userId);
            userRequest.StatusId = statusId;
            Update(userRequest);
        }

        public override void Add(UserRequest entity)
        {
            entity.RequestDate = DateTime.Now;
            base.Add(entity);
        }
    }
}