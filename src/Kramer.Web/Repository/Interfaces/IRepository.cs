using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kramer.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> All(); 
        T GetById(int id); 
        void Add(T entity); 
        void Delete(int id); 
        void Update(T entity);
    }
}
