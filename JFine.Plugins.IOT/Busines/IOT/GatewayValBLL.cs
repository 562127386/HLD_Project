﻿
/********************************************************************************
**文 件 名:ModbusGatewayValBLL
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:58:35
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
using JFine.Domain.Models.SystemManage;
using JFine.Busines.SystemManage;

namespace JFine.Plugins.IOT.Busines.IOT
{
    /// <summary>
    /// GatewayValBLL
    /// </summary>	
    public class GatewayValBLL
    {
        private IGatewayValRepository service = new GatewayValRepository();

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ModbusGatewayValCache";

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetList()
        {
            return service.GetList();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetListBySql(string sqlWhere)
        {
            return service.GetListBySql(sqlWhere);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetPageListBySql(Pagination pagination, string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                sqlWhere.Append(" AND (Code like @keyword or Name like @keyword)");
                parameter.Add(DbParameters.CreateDbParameter("@keyword", "%" + keyword + "%", DbType.AnsiString));
            }

            return service.GetPageListBySql(pagination, sqlWhere.ToString(), parameter);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<GatewayValEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["ParameterCode"].IsEmpty())
            {
                string ParameterCode = queryParam["ParameterCode"].ToString();
                expression = expression.And(t => t.ParameterCode.Contains(ParameterCode));
            }
            return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<GatewayValEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Brand"].IsEmpty())
            {
                string Brand = queryParam["Brand"].ToString();
                expression = expression.And(t => t.ParameterCode.Contains(Brand));
            }
            return service.GetPageList(pagination, expression);
        }
        public DataTable GetNewestData(Pagination pagination, string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            //查询条件
            List<DbParameter> parameter = new List<DbParameter>();
            //查询条件
            if (!queryParam["deviceId"].IsEmpty())
            {
                string deviceId = queryParam["deviceId"].ToString();
                sqlWhere.Append(" AND (DeviceId='" + deviceId + "')");

            }
            return service.GetNewestData(pagination, sqlWhere.ToString(), null);
        }

        public DataTable GetHistoryData(string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            //查询条件
            List<DbParameter> parameter = new List<DbParameter>();
            //查询条件
            if (!queryParam["deviceId"].IsEmpty())
            {
                string deviceId = queryParam["deviceId"].ToString();
                sqlWhere.Append(" AND (DeviceId='" + deviceId + "')");

            }
            if (!queryParam["ParameterCode"].IsEmpty())
            {
                string ParameterCode = queryParam["ParameterCode"].ToString();
                sqlWhere.Append(" AND (ParameterCode='" + ParameterCode + "')");

            }
            return service.GetHistoryData(sqlWhere.ToString());
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public GatewayValEntity GetForm(string keyValue)
        {
            return service.GetForm(keyValue);
        }
        /// <summary>
        /// 获取每个设备当天开机时间。
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity>GetDeviceStartTime(string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            //查询条件
            List<DbParameter> parameter = new List<DbParameter>();
            //查询条件
            if (!queryParam["deviceId"].IsEmpty())
            {
                string deviceId = queryParam["deviceId"].ToString();
                sqlWhere.Append(" AND (DeviceId='" + deviceId + "')");

            }
            return service.GetDeviceStartTime(sqlWhere);
        }
        #endregion

        #region 数据处理



        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, GatewayValEntity gatewayValEntity)
        {
            try
            {
                service.SaveForm(keyValue, gatewayValEntity);
                CacheFactory.Cache().WriteCache(cacheKey, gatewayValEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveList(List<GatewayValEntity> valList)
        {
            try
            {
                service.SaveList(valList);
            }
            catch (Exception e)
            {
                //记录日志
                LogEntity logEntity = new LogEntity();
                logEntity.Category = "Modbus线程";
                logEntity.OperateType = "写数据";
                logEntity.CreateUserId = "";
                logEntity.CreateUserName = "Modbus";
                logEntity.Module = "";
                logEntity.ModuleId = "";
                logEntity.ExecuteResult = -1;
                logEntity.Description = "写数据异常";
                logEntity.CreateDate = DateTime.Now;
                logEntity.IPAddress = "";
                logEntity.Host = "";
                logEntity.Browser = "";
                logEntity.SourceContentJson = e.Message;
                logEntity.Mark = "";
                logEntity.AddLog();
            }
        }

        public void SaveList(StringBuilder sql)
        {
            try
            {
                service.SaveList(sql);
            }
            catch (Exception e)
            {
                //记录日志
                LogEntity logEntity = new LogEntity();
                logEntity.Category = "采集线程";
                logEntity.OperateType = "写数据";
                logEntity.CreateUserId = "";
                logEntity.CreateUserName = "采集器";
                logEntity.Module = "";
                logEntity.ModuleId = "";
                logEntity.ExecuteResult = 1;
                logEntity.Description = "写数据异常";
                logEntity.CreateDate = DateTime.Now;
                logEntity.IPAddress = "";
                logEntity.Host = "";
                logEntity.Browser = "";
                logEntity.SourceContentJson = e.Message;
                logEntity.Mark = "";
                logEntity.AddLog();
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
