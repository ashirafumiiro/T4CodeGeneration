using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRepository<T, TDTO>
    {
        T Add(T entity);
        T Update(TDTO entity);
        T Delete(T entity);
        T GetOne(int? id);
        List<T> GetAll();
    }
}
