using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.VS2022.Common
{
    /// <summary>
    /// .net core 不再支持 CallContext，需要另实现
    /// </summary>
    public static class DotNetCoreCallContext
    {
        static ConcurrentDictionary<string, AsyncLocal<object>> state = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static void SetData(string name, object data)
        {
            state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;
        }

        public static object GetData(string name)
        {
            AsyncLocal<object> data;
            return state.TryGetValue(name, out data) ? data.Value : null;
        }
    }
}
