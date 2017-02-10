using Kramer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Repository
{
    public class ISaleTypeRepository
    {
        IEnumerable<SaleType> All();
        SaleType GetById(int id);
        void Add(SaleType saleType);
        void Delete(int id);
        void Update(SaleType saleType);
    }
}