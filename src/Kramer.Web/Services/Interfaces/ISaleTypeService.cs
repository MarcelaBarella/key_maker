using Kramer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kramer.Services
{
    public interface ISaleTypeService
    {
        List<SaleType> GetAvailableSaleTypes(string userId);
    }
}
