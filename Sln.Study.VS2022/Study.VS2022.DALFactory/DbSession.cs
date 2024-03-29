﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Study.VS2022.Model;
using Study.VS2022.IDAL;
using Study.VS2022.DAL;
using Microsoft.EntityFrameworkCore;

// 看情况添加
using Study.VS2022.Model.AutoModels;
// 第7步:添加 Dals.tt,添加时,要用文本文件为基础

namespace Study.VS2022.DALFactory
{
	public partial class DbSession : IDbSession
	{
		private ITOrderDal _TOrderDal;
		public ITOrderDal TOrderDal
		{
			get 
			{
				if (_TOrderDal == null)
				{
					_TOrderDal = new TOrderDal();
				}
				return _TOrderDal;
			}
		}

		private ITUserDal _TUserDal;
		public ITUserDal TUserDal
		{
			get 
			{
				if (_TUserDal == null)
				{
					_TUserDal = new TUserDal();
				}
				return _TUserDal;
			}
		}
		public int SaveChanges()
		{
			DbContext ctx = EFDbContextFactory.GetCurrentDbContext();
			return ctx.SaveChanges();
		}
	}
}