/********************************************************************************
**文 件 名:IOT_ProductLineRepository
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-10-12 10:15:35
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
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Data.Repository;
using JFine.Plugins.IOT.Domain.IRepository.IOT;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Data.Common;
using System.Linq.Expressions;
using System.Data;
using Newtonsoft.Json;

namespace JFine.Plugins.IOT.Domain.Repository.IOT
{
    /// <summary>
    /// IOT_ProductLineRepository
    /// </summary>	
    public class IOT_ProductLineRepository : RepositoryFactory<IOT_ProductLineEntity>, IIOT_ProductLineRepository
    {
        #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   IOT_ProductLine
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
        public IEnumerable<IOT_ProductLineEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   IOT_ProductLine
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(), parameter, pagination);

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetList(Expression<Func<IOT_ProductLineEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOT_ProductLineEntity> GetPageList(Pagination pagination, Expression<Func<IOT_ProductLineEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOT_ProductLineEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取关联用户产线
        /// </summary>
        /// <param name="keyValue">用户Id</param>
        /// <returns></returns>
        public DataTable GetRelvantProductLine(string keyValue)
        {
            StringBuilder sqldata = new StringBuilder();
            
            if (!string.IsNullOrEmpty(keyValue))
            {
                sqldata.Append(@" select (select UserId from dbo.IOT_ProductLine_Map t2 where t1.LineCode=t2.LineCode and t2.UserId='"+keyValue+"')as UserId, * from dbo.IOT_ProductLine t1 ");
                //sqldata.Append(" and t2.UserId='" + keyValue + "'");
            }
            else
            {
                sqldata.Append(@" select (select UserId from dbo.IOT_ProductLine_Map t2 where t1.LineCode=t2.LineCode)as UserId, * from dbo.IOT_ProductLine t1 ");
            }
            DataTable dt = new RepositoryFactory().BaseRepository().FindTable(sqldata.ToString());
            return dt;
        }
        #endregion

        #region 数据处理

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="iOT_ProductLineEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, IOT_ProductLineEntity iOT_ProductLineEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                iOT_ProductLineEntity.Modify(keyValue);
                this.BaseRepository().Update(iOT_ProductLineEntity);
            }
            else
            {
                iOT_ProductLineEntity.Create();
                this.BaseRepository().Insert(iOT_ProductLineEntity);
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
        /// <summary>
        /// 保存关联用户产线
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="productLineListJson"></param>
        public void SaveRelvantProductLine(string keyValue, string productLineListJson)
        {

            DataTable list_table = JsonConvert.DeserializeObject<DataTable>(productLineListJson);
            var db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<IOT_ProductLine_MapEntity>(u => u.UserId == keyValue);
                for (int i = 0; i < list_table.Rows.Count; i++)
                {
                    IOT_ProductLine_MapEntity line_MapEntity = new IOT_ProductLine_MapEntity();
                    line_MapEntity.Create();
                    line_MapEntity.UserId = keyValue;
                    line_MapEntity.LineCode = list_table.Rows[i]["LineCode"].ToString();
                    db.Insert(line_MapEntity);
                }
                db.Commit();
            }
            catch (Exception)
            {

                db.Rollback();
                throw;
            }
        }

        #endregion
    }
}


