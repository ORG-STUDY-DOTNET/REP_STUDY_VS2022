using Study.VS2022.Common;
using Study.VS2022.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.DALFactory
{
    public class DbSessionFactory
    {
        public static IDbSession GetDbSession()
        {
            IDbSession dbSession = (IDbSession)DotNetCoreCallContext.GetData("DbSession");
            if (dbSession == null)
            {
                dbSession = new DbSession();
                DotNetCoreCallContext.SetData("DbSession", dbSession);
                return dbSession;
            }
            return dbSession;
        }
    }
}
