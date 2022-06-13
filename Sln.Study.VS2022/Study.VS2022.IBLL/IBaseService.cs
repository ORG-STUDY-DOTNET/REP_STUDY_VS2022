using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.IBLL
{
    public interface IBaseService<T>
        where T : class, new()
    {
        T Add(T entity);
        int SaveChanges();
    }
}
