using Kramer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kramer.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext db;

        public RepositoryBase(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<T> All()
        {
            return db.Set<T>().AsEnumerable();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Set<T>().Remove(GetById(id));
            db.SaveChanges();
        }

        public void Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}

