using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Repository
{
    public class SaleTypeRepository : IUserRequestRepository, IDisposable
    {
        private ApplicationDbContext context;

        public SaleTypeRepository(ApplicationDbContext)
        {
            this.context = context;
        }

        public IEnumerable<UserRequest> All()
        {
            return context.SaleTypes.ToList();
        }

        public UserRequest GetById(int id)
        {
            return context.SaleTypes.Find(id);
        }

        public void Add(SaleType saleType)
        {
            context.SaleTypes.Add(saleType);
        }

        public void Delete(int id)
        {
            SaleType saleType = context.SaleTypes.Find(id);
            context.SaleTypes.Remove(student);
        }

        public void Update(SaleTypes saleType)
        {
            context.Entry(saleType).State = EntityState.Modified;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}