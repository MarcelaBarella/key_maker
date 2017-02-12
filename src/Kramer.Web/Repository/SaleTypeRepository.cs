using Kramer.Domain;
using Kramer.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace Kramer.Repository
{
    public class SaleTypeRepository : RepositoryBase<SaleType>, ISaleTypeRepository
    {
        public SaleTypeRepository(ApplicationDbContext db) : base(db) {}
    }
}