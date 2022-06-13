using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Study.VS2022.Model;
using Study.VS2022.IBLL;

// 看情况添加
using Study.VS2022.Model.AutoModels;
// 第7步:添加 Dals.tt,添加时,要用文本文件为基础

namespace Study.VS2022.BLL
{
	public partial class TOrderService : BaseService<TOrder>, ITOrderService
	{
		public override void SetCurrentDal()
		{
			CurrentDal = this.DbSession.TOrderDal;
		}
	}

	public partial class TUserService : BaseService<TUser>, ITUserService
	{
		public override void SetCurrentDal()
		{
			CurrentDal = this.DbSession.TUserDal;
		}
	}
}