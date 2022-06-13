using Microsoft.EntityFrameworkCore;
using Study.VS2022.Common;
using Study.VS2022.Model.AutoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.DAL
{
    public class EFDbContextFactory
    {
        // 注意在 Nuget 中统一所依赖库的版本
        // ------------------------------------
        public static DbContext GetCurrentDbContext()
        {
            DbContext db = (DbContext)DotNetCoreCallContext.GetData("DbContext");
            if (db == null)
            {
                // 需要引用不同的 Model 项目
                // -------------------------
                db = new TestContext();
                //db = new StudyVS2022Context();
                //db = new CaseContext();

                DotNetCoreCallContext.SetData("DbContext", db);
            }
            return db;
        }
    }
}
