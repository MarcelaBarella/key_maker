using Kramer.Domain;
using Kramer.Models;
using System.Collections.Generic;
using System.Data.Entity;

using System.Collections.Generic;


namespace Kramer.Repository
{
    public class SaleTypeRepository : ISaleTypeRepository
    {
        private ApplicationDbContext db;

        public SaleTypeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleType> All()
        {
            return db.SaleType();
        }

        public SaleType GetById(int id)
        {
            return db.SaleType.Find(id);
        }

        public void Add(SaleType saleType)
        {
            db.SaleType.Add(saleType);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.SaleType.Remove(GetById(id));
            db.SaveChanges();
        }

        public void Update(SaleType saleType)
        {
            db.SaleType.Attach(saleType);
            db.Entry(saleType).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}