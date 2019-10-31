
/********************************************************************************
**文 件 名:IOT_ProductLineBLL
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-10-12 10:15:30
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
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Plugins.IOT.Domain.IRepository.IOT;
using JFine.Plugins.IOT.Domain.Repository.IOT;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;

namespace JFine.Plugins.IOT.Busines.IOT
{	
	/// <summary>
	/// IOT_ProductLineBLL
	/// </summary>	
	public class IOT_ProductLineBLL
	{
	    private IIOT_ProductLineRepository service=new IOT_ProductLineRepository();

		/// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey ="IOT_ProductLineCache" ;

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetPageListBySql(Pagination pagination, string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
			 List<DbParameter> parameter =  new List<DbParameter>();
            //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                sqlWhere.Append(" AND (Code like @keyword or Name like @keyword)");
				parameter.Add(DbParameters.CreateDbParameter("@keyword","%"+ keyword +"%",DbType.AnsiString));
            }
            
            return service.GetPageListBySql(pagination, sqlWhere.ToString(),parameter);
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<IOT_ProductLineEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Name"].IsEmpty())
            {
                string name = queryParam["Name"].ToString();
                expression = expression.And(t => t.Id.Contains(name));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<IOT_ProductLineEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Id.Contains(keyord));
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOT_ProductLineEntity GetForm(string keyValue)
        {
            return service.GetForm(keyValue);
        }
        /// <summary>
        /// 获取关联用户产线
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataTable GetRelvantProductLine(string keyValue)
        {
            return service.GetRelvantProductLine(keyValue);
        }
        #endregion

        #region 数据处理



        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, IOT_ProductLineEntity iOT_ProductLineEntity)
        {
            try
            {
                service.SaveForm(keyValue, iOT_ProductLineEntity);
                CacheFactory.Cache().WriteCache(cacheKey,iOT_ProductLineEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存关联用户产线
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="productLineListJson"></param>
        public void SaveRelvantProductLine(string keyValue, string productLineListJson)
        {
            try
            {
                service.SaveRelvantProductLine(keyValue, productLineListJson);
                //CacheFactory.Cache().WriteCache(cacheKey, iOT_ProductLineEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteForm(string keyValue)
        {
            try
            {
                service.DeleteForm(keyValue);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
