using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Study.VS2022.Model;
using Study.VS2022.IDAL;
// 第7步:添加 Dals.tt,添加时,要用文本文件为基础

//
using Study.VS2022.Model.AutoModels;

namespace Study.VS2022.DAL
{
	public partial class TOrderDal : BaseDal<TOrder>, ITOrderDal
	{
	}

	public partial class TUserDal : BaseDal<TUser>, ITUserDal
	{
	}
}