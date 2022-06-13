using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.DAL
{
    public class BaseDal<T>
      where T : class, new()
    {
        private DbContext db
        {
            get
            {
                return EFDbContextFactory.GetCurrentDbContext();
            }
        }

        public virtual T Add(T entity)
        {
            db.Set<T>().Add(entity);
            return entity;
        }
    }
}
