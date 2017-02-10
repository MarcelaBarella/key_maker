using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Models
{
    interface ISaleTypeRepository : IDisposable
    {
        IEnumerable<SaleTypeViewModel> GetSaleType();
        SaleTypeViewModel GetSaleTypeById(int saleTypeId);
        void Save();
    }
}