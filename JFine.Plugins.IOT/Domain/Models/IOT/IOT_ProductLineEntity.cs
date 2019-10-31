/********************************************************************************
**文 件 名:IOT_ProductLineEntity
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-10-12 10:15:34
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
	/// IOT_ProductLineEntity
	/// </summary>	
	public class IOT_ProductLineEntity:BaseEntity<IOT_ProductLineEntity>, ICreate, IModify
	{

		/// <summary>
        /// 默认构造函数
        /// </summary>
        public IOT_ProductLineEntity()
		{
            this.Id= System.Guid.NewGuid().ToString();

 		}

	#region 实体成员

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string Id { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string ParentId { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string LineName { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string LineCode { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public DateTime? CreateDate { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string CreateUserId { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string CreateUserName { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public DateTime? UpdateDate { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string UpdateUserId { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string UpdateUserName { get; set; }

 
  #endregion
    }
}

