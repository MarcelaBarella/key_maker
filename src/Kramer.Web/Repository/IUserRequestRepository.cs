using System.Collections.Generic;
using Kramer.Domain;

namespace Kramer.Repository
{
    public interface IUserRequestRepository
    {
        IEnumerable<UserRequest> All();
        UserRequest GetById(int id);
        void Add(UserRequest userRequest);
        void Delete(int id);
        void Update(UserRequest userRequest);
        void ChangeStatus(int id);
    }
}
