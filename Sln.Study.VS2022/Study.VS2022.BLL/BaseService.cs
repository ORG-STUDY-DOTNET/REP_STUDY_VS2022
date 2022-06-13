using Study.VS2022.DALFactory;
using Study.VS2022.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.BLL
{
    public abstract class BaseService<T>
            where T : class, new()
    {
        protected IBaseDal<T> CurrentDal;

        protected IDbSession DbSession
        {
            get
            {
                return DbSessionFactory.GetDbSession();
            }
        }

        public BaseService()
        {
            SetCurrentDal();
        }

        public abstract void SetCurrentDal();

        public virtual T Add(T entity)
        {
            //DbSession.TOrderDal;
            return CurrentDal.Add(entity);
        }

        public virtual int SaveChanges()
        {
            return DbSession.SaveChanges();
        }
    }
}
