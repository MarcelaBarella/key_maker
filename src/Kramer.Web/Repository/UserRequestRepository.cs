using System.Collections.Generic;
using System.Data.Entity;
using Kramer.Domain;
using Kramer.Models;

namespace Kramer.Repository
{
    public class UserRequestRepository : IUserRequestRepository
    {
        private ApplicationDbContext db;

        //vamos receber o db via construtor. Quem vai ser responsável por
        //adicionar esse db aqui vai ser o contâiner de injeção de dependência,
        //o Ninject. Como já configuramos a injeção do ApplicationDbContext lá nas
        //configurações do Ninject, ele fará isso sozinho.
        public UserRequestRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserRequest> All()
        {
            return db.UserRequest;
        }

        public UserRequest GetById(int id)
        {
            return db.UserRequest.Find(id);
        }

        public void Add(UserRequest userRequest)
        {
            db.UserRequest.Add(userRequest);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.UserRequest.Remove(GetById(id));
            db.SaveChanges();
        }

        public void Update(UserRequest userRequest)
        {
            db.UserRequest.Attach(userRequest);
            db.Entry(userRequest).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ChangeStatus(int id)
        {
            var userRequest = GetById(id);
            userRequest.Pending = !userRequest.Pending;
            Update(userRequest);
        }
    }
}