using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.IDAL
{
    public interface IBaseDal<T>
        where T : class, new()
    {
    }
}
