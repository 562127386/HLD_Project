﻿/********************************************************************************
**文 件 名:VMWMapLogMap
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:18:02
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/

using JFine.Plugins.VMW.Domain.Models.VMW;
using JFine.Data.Common;
using System.Data.Entity.ModelConfiguration;
namespace JFine.Plugins.VMW.Mapping.VMW
{	
	/// <summary>
	/// VMWMapLogMap
	/// </summary>	
	public class VMWMapLogMap:JFEntityTypeConfiguration<VMWMapLogEntity>
	{
	   public VMWMapLogMap()
	   {
	      this.ToTable("VMW_Map_Log");
		  this.HasKey(t=>t.Id);
	   }
    }
}

