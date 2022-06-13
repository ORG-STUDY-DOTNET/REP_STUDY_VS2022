using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Study.VS2022.Model;
using Study.VS2022.Model.MSSQLServer;
using Study.VS2022.Model.Oracle;

// 看情况添加
using Study.VS2022.Model.AutoModels;
// 第7步:添加 Dals.tt,添加时,要用文本文件为基础

namespace Study.VS2022.IDAL
{
	public partial interface ITOrderDal : IBaseDal<TOrder>
	{
	}

	public partial interface ITUserDal : IBaseDal<TUser>
	{
	}
}