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

        /// <summary>
		/// 推荐在 DAL 层使用，在 BLL 层中所有被操作的内容都是已加载到内存的。
		/// </summary>
		/// <typeparam name="TOtherTable">表对应的实体类型</typeparam>
		/// <returns>返回用于在某次会话中，针对某张表的 Set </returns>
		protected DbSet<TOtherTable> GetSet<TOtherTable>()
            where TOtherTable : class, new()
        {
            return EFDbContextFactory.GetCurrentDbContext().Set<TOtherTable>();
        }
    }
}
