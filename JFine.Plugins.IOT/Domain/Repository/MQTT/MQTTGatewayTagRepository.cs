﻿/********************************************************************************
**文 件 名:MQTTGatewayTagRepository
**命名空间:JFine.Plugins.IOT.Busines.MQTT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-29 10:49:38
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFine.Data;
using JFine.Plugins.IOT.Domain.Models.MQTT;
using JFine.Data.Repository;
using JFine.Plugins.IOT.Domain.IRepository.MQTT;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Data.Common;
using System.Linq.Expressions;

namespace JFine.Plugins.IOT.Domain.Repository.MQTT
{	
	/// <summary>
	/// MQTTGatewayTagRepository
	/// </summary>	
	public class MQTTGatewayTagRepository:RepositoryFactory<MQTTGatewayTagEntity>, IMQTTGatewayTagRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MQTTGatewayTagEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MQTTGatewayTagEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   MQTT_Tags
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);
            return this.BaseRepository().FindList(strSql.ToString());
        
		}

		/// <summary>
        /// 列表-分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <returns></returns>
        public IEnumerable<MQTTGatewayTagEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   MQTT_Tags
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MQTTGatewayTagEntity> GetList(Expression<Func<MQTTGatewayTagEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<MQTTGatewayTagEntity> GetPageList(Pagination pagination, Expression<Func<MQTTGatewayTagEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MQTTGatewayTagEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="mQTTGatewayTagEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MQTTGatewayTagEntity mQTTGatewayTagEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                mQTTGatewayTagEntity.Modify(keyValue);
                this.BaseRepository().Update(mQTTGatewayTagEntity);
            }
            else
            {
                mQTTGatewayTagEntity.Create();
                this.BaseRepository().Insert(mQTTGatewayTagEntity);
            }
        }

		/// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }

        #endregion
    }
}

