/********************************************************************************
**文 件 名:IOT_Order_SecRepository
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-09-30 14:27:44
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
using JFine.Plugins.IOT.IOTHubs;
using JFine.Code.Online;

namespace JFine.Plugins.IOT.Domain.Repository.IOT
{
    /// <summary>
    /// IOT_Order_SecRepository
    /// </summary>	
    public class IOT_Order_SecRepository : RepositoryFactory<IOT_Order_SecEntity>, IIOT_Order_SecRepository
    {

        private static OrderHub orderHub = new OrderHub();
        #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   IOT_Order_Sec
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
        public IEnumerable<IOT_Order_SecEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   IOT_Order_Sec
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(), parameter, pagination);

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetList(Expression<Func<IOT_Order_SecEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetPageList(Pagination pagination, Expression<Func<IOT_Order_SecEntity, bool>> condition)
        {
            //登陆人过滤
            var userModel = OnlineUser.Operator.GetCurrent();
            if (!userModel.IsSystem)
            {
                var line_list = new RepositoryFactory().BaseRepository().FindList<IOT_ProductLine_MapEntity>(u => u.UserId == userModel.UserId);
                string param = string.Empty;
                foreach (var item in line_list)
                {
                    param += item.LineCode + "|";
                }
                condition = condition.And(t => param.Contains(t.ProductLine));
            }
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOT_Order_SecEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="iOT_Order_SecEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, IOT_Order_SecEntity iOT_Order_SecEntity)
        {
            iOT_Order_SecEntity.Status = "0";
            iOT_Order_SecEntity.StartStatus = "0";
            iOT_Order_SecEntity.ProceedStatus = string.IsNullOrEmpty(keyValue) ? "0" : iOT_Order_SecEntity.ProceedStatus;
            iOT_Order_SecEntity.QualifyQuantity = iOT_Order_SecEntity.QualifyQuantity == null ? 0 : iOT_Order_SecEntity.QualifyQuantity;
            iOT_Order_SecEntity.UnqualifyQuantity = iOT_Order_SecEntity.UnqualifyQuantity == null ? 0 : iOT_Order_SecEntity.UnqualifyQuantity;
            if (!string.IsNullOrEmpty(keyValue))
            {
                iOT_Order_SecEntity.Modify(keyValue);
                this.BaseRepository().Update(iOT_Order_SecEntity);
            }
            else
            {
                iOT_Order_SecEntity.Create();
                this.BaseRepository().Insert(iOT_Order_SecEntity);
            }
            //推送信息刷新订单看板
            orderHub.UpdateOrderBoard(keyValue);
        }
        /// <summary>
        /// 更新合格数量
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="iOT_Order_SecEntity"></param>
        public void SaveChangeQForm(string keyValue, IOT_Order_SecEntity iOT_Order_SecEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {

                this.BaseRepository().ExecuteBySql(@" update IOT_Order_Sec set  QualifyQuantity='" + iOT_Order_SecEntity.QualifyQuantity + "',UnqualifyQuantity='" + iOT_Order_SecEntity.UnqualifyQuantity + "' where Id='" + keyValue + "'");
                //推送信息刷新订单看板
                orderHub.UpdateOrderBoard(keyValue);
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
        /// 启动订单
        /// </summary>
        /// <param name="keyValue"></param>
        public void OrderStart(string keyValue)
        {
            StringBuilder sqldata = new StringBuilder(@" update IOT_Order_Sec set StartStatus='1' where Id='" + keyValue + "'");
            this.BaseRepository().ExecuteBySql(sqldata.ToString());
        }
        /// <summary>
        /// 更换进行中订单
        /// </summary>
        /// <param name="keyValue"></param>
        public void ChangeProceedOrder(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            //更新当前产线其余订单状态，生产中改为暂停，其余为未开始。
            string productLine = db.FindEntity(u => u.Id == keyValue).ProductLine;
            db.ExecuteBySql(@" update dbo.IOT_Order_Sec set ProceedStatus=case when ProceedStatus='1' then '2' else '0' end where ProductLine='" + productLine + "'");
            db.ExecuteBySql(@" update IOT_Order_Sec set ProceedStatus='1' where Id='" + keyValue + "'");
            try
            {
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
            finally
            {
                //推送信息刷新订单看板
                orderHub.UpdateOrderBoard(keyValue);
            }
        }

        /// <summary>
        /// 手动完工订单
        /// </summary>
        /// <param name="keyValue"></param>
        public void FinishProceedOrder(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            StringBuilder sqldata = new StringBuilder();
            //更新订单状态为已完工
            //校验是否为当日订单，若非，只完工选择订单，若为当日，判断是否有进行中订单，若无，自动切换下一订单为生产中。
            var orderEntity = db.FindEntity(u => u.Id == keyValue);
            db.ExecuteBySql(@" update dbo.IOT_Order_Sec set ProceedStatus=3 where Id='" + keyValue + "'");
            //当日订单
            if (orderEntity.OrderDate.Equals(DateTime.Parse(DateTime.Now.ToShortDateString())))
            {
                //判断当日是否有进行中订单
                bool isProceed = db.FindEntity(u => u.ProceedStatus == "1" && u.ProductLine == orderEntity.ProductLine && u.Id != keyValue && u.OrderDate == orderEntity.OrderDate).IsEmpty();
                if (!isProceed)
                {
                    //查询当日订单开始时间大于当前订单的订单，更改为生产中
                    var order_NextEntity = db.FindEntity(u => u.ProductLine == orderEntity.ProductLine && u.OrderDate == orderEntity.OrderDate && u.OrderBegin_Time > orderEntity.OrderBegin_Time);
                    db.ExecuteBySql(@" update dbo.IOT_Order_Sec set ProceedStatus=1 where Id='" + order_NextEntity.Id + "'");
                }
            }
            try
            {
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
            finally
            {
                //推送信息刷新订单看板
                orderHub.UpdateOrderBoard(keyValue);
            }
        }
        /// <summary>
        /// 实时更新订单合格不合格数量
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="quantityType"></param>
        public void UpdateOrderQuantity(string keyValue, string quantityType,string lineCode)
        {
            //获取正在进行中的订单Id，当日订单
            var dateNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            var Id = this.BaseRepository().FindEntity(u => u.ProceedStatus == "1" && u.OrderDate == dateNow&&u.ProductLine==lineCode).Id;
            if (quantityType == "qualify" || quantityType == "1")
            {
                this.BaseRepository().ExecuteBySql(" update IOT_Order_Sec set QualifyQuantity=isnull(QualifyQuantity,0)+1 where Id='" + Id + "'");
            }
            else if (quantityType == "unQualify" || quantityType == "0")
            {
                this.BaseRepository().ExecuteBySql(" update IOT_Order_Sec set UnqualifyQuantity=isnull(UnqualifyQuantity,0)+1 where Id='" + Id + "'");
            }
            //推送信息刷新订单看板
            //orderHub.UpdateOrderBoard(keyValue);

        }

        //获取正在进行中订单
        public IOT_Order_SecEntity GetProceedOrder()
        {
            //获取正在进行中的订单Id，当日订单
            var dateNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            return this.BaseRepository().FindEntity(u => u.ProceedStatus == "1" && u.OrderDate == dateNow);
        }

      

        #endregion
    }
}


