﻿/********************************************************************************
**文 件 名:IOTProductPlanEntity
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-08-06 14:34:16
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using JFine.Domain.Models;
namespace JFine.Plugins.IOT.Domain.Models.IOT
{	
	/// <summary>
	/// IOTProductPlanEntity
	/// </summary>	
	public class IOTProductPlanEntity:BaseEntity<IOTProductPlanEntity>, ICreate, IModify
	{

		/// <summary>
        /// 默认构造函数
        /// </summary>
        public IOTProductPlanEntity()
		{
            this.Id= System.Guid.NewGuid().ToString();

 		}

	#region 实体成员

	  
	  /// <summary>
	  /// 主键
	  /// </summary>	
	  public string Id { get; set; }

	  
	  /// <summary>
	  /// 流水编码
	  /// </summary>	
	  public string Code { get; set; }

	  
	  /// <summary>
	  /// 文件
	  /// </summary>	
	  public string FileRUL { get; set; }

	  
	  /// <summary>
	  /// 文件
	  /// </summary>	
	  public string FileName { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public int? Quantity { get; set; }

	  
	  /// <summary>
	  /// 创建日期
	  /// </summary>	
	  public DateTime? CreateDate { get; set; }

	  
	  /// <summary>
	  /// 创建用户账户
	  /// </summary>	
	  public string CreateUserId { get; set; }

	  
	  /// <summary>
	  /// 创建用户名称
	  /// </summary>	
	  public string CreateUserName { get; set; }

	  
	  /// <summary>
	  /// 最后修改时间
	  /// </summary>	
	  public DateTime? UpdateDate { get; set; }

	  
	  /// <summary>
	  /// 最后修改用户
	  /// </summary>	
	  public string UpdateUserId { get; set; }

	  
	  /// <summary>
	  /// 最后修改用户名称
	  /// </summary>	
	  public string UpdateUserName { get; set; }

 
  #endregion
    }
}
