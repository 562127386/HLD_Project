﻿/********************************************************************************
**文 件 名:IOTDeviceController
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-07-01 14:26:04
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Code.Online;
using Newtonsoft.Json;

namespace JFine.Plugins.IOT.Areas.IOT.Controllers
{
    [HandlerPlugin("IOT")]
    /// <summary>
    /// IOTDeviceController
    /// </summary>	
    public class IOTHomeController : JFControllerBase
    {
        private IOTDeviceBLL iOTDeviceBll = new IOTDeviceBLL();
        private IOTDeviceParaBLL iOTDeviceParaBLL = new IOTDeviceParaBLL();
        private IOTOrderBLL orderBLL = new IOTOrderBLL();
        private IOT_Order_SecBLL order_SecBLL = new IOT_Order_SecBLL();
        private GatewayValBLL gatewayValBLL = new GatewayValBLL();
        private TCPGatewayValuesBLL tcp_GatewayValBLL = new TCPGatewayValuesBLL();
        private IOT_ProductLineBLL productLineBll = new IOT_ProductLineBLL();
        private IOT_SquadBLL squadBll = new IOT_SquadBLL();
        #region View
        //
        // GET: /IOT/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.IOT/Views/IOTHome/Index.cshtml");
        }

        //
        // GET: /IOT/
        public ActionResult DeviceIndex(string category)
        {
            category = string.IsNullOrEmpty(category) ? "电机" : category;
            ViewBag.DiviceList = iOTDeviceBll.GetListBySql(" and Category='" + category + "'").ToList();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceIndex.cshtml");
        }
        public ActionResult DeviceBoard(string category)
        {
            category = string.IsNullOrEmpty(category) ? "注塑机" : category;
            DataTable dt_device = iOTDeviceBll.GetDeviceOrder(" and Category='" + category + "'");
            ViewBag.DiviceList = dt_device;
            ViewBag.DeviceParamList = iOTDeviceParaBLL.GetList().ToJson();
            ViewBag.DevicePage = Math.Ceiling(Convert.ToDecimal(dt_device.Rows.Count) / 20);
            //获取开机时间
            ViewBag.DeviceStartTimeList = gatewayValBLL.GetDeviceStartTime(null).ToJson();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceBoard.cshtml");
        }
        public ActionResult DeviceBoard2(string category)
        {
            category = string.IsNullOrEmpty(category) ? "注塑机" : category;
            DataTable dt_device = iOTDeviceBll.GetDeviceOrder(" and Category='" + category + "'");
            ViewBag.DiviceList = dt_device;
            ViewBag.DeviceParamList = iOTDeviceParaBLL.GetList().ToJson();
            ViewBag.DevicePage = Math.Ceiling(Convert.ToDecimal(dt_device.Rows.Count) / 20);
            ViewBag.DeviceStartTimeList = gatewayValBLL.GetDeviceStartTime(null).ToJson();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceBoard2.cshtml");
        }
        public ActionResult DeviceBoardData(string category, string Name)
        {
            category = string.IsNullOrEmpty(category) ? "注塑机" : category;
            Name = string.IsNullOrEmpty(Name) ? "A" : Name;
            ViewBag.DiviceList = iOTDeviceBll.GetDeviceOrder(" and Category='" + category + "' and Name like '%" + Name + "%'");
            ViewBag.DeviceParamList = iOTDeviceParaBLL.GetList().ToJson();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceBoardData.cshtml");
        }
        public ActionResult DeviceBoardData2(string category, string Name)
        {
            category = string.IsNullOrEmpty(category) ? "注塑机" : category;
            Name = string.IsNullOrEmpty(Name) ? "A" : Name;
            ViewBag.DiviceList = iOTDeviceBll.GetDeviceOrder(" and Category='" + category + "' and Name like '%" + Name + "%'");
            ViewBag.OrderList = orderBLL.GetListBySql(" order by DeviceCode ").ToList();
            ViewBag.DeviceParamList = iOTDeviceParaBLL.GetList().ToJson();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceBoardData2.cshtml");
        }
        public ActionResult DeviceGraph(string category)
        {
            category = string.IsNullOrEmpty(category) ? "电机" : category;
            ViewBag.DiviceList = iOTDeviceBll.GetListBySql(" and Category='" + category + "'").ToList();

            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceGraph.cshtml");
        }
        public ActionResult Test()
        {
            ViewBag.DiviceList = iOTDeviceBll.GetListBySql("").ToList();
            return View("~/Plugins/JFine.IOT/Views/ModbusGateway/Test.cshtml");
        }
        public ActionResult DeviceBoard_M_Index()
        {
            var list = iOTDeviceBll.GetList().ToList();
            ViewBag.DeviceCount = list.Count;
            ViewBag.DeviceList = list.ToJson();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceBoard_M_Index.cshtml");
        }
        public ActionResult DeviceBoard_M_Details(string category)
        {
            category = string.IsNullOrEmpty(category) ? "注塑机" : category;
            ViewBag.DiviceList = iOTDeviceBll.GetDeviceOrder(" and Category='" + category + "'");
            ViewBag.DeviceParamList = iOTDeviceParaBLL.GetList().ToJson();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceBoard_M_Details.cshtml");
        }
        public ActionResult DeviceBoard_M_Details2(string category)
        {
            category = string.IsNullOrEmpty(category) ? "注塑机" : category;
            ViewBag.DiviceList = iOTDeviceBll.GetDeviceOrder(" and Category='" + category + "'");
            ViewBag.DeviceParamList = iOTDeviceParaBLL.GetList().ToJson();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceBoard_M_Details2.cshtml");
        }

        public ActionResult Index_Mobile()
        {
            ViewBag.DiviceList = iOTDeviceBll.GetList().ToList();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/Index_Mobile.cshtml");
        }
        public ActionResult DeviceIndex_Mobile()
        {
            ViewBag.DiviceList = iOTDeviceBll.GetList().ToList();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceIndex_Mobile.cshtml");
        }
        public ActionResult DeviceGraph_Mobile()
        {
            ViewBag.DiviceList = iOTDeviceBll.GetList().ToList();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/DeviceGraph_Mobile.cshtml");
        }
        public ActionResult Report_DeviceAvailRate()
        {
            ViewBag.DiviceList = iOTDeviceBll.GetList().ToList();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/Report_DeviceAvailRate.cshtml");
        }
        public ActionResult Report_ProAchieveRate()
        {
            ViewBag.DiviceList = iOTDeviceBll.GetList().ToList();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/Report_ProAchieveRate.cshtml");
        }
        public ActionResult OrderBoard()
        {
            //获取当前产线
            var lineCode = Request["lineCode"];
            var userCode = OnlineUser.Operator.GetCurrent().IsSystem ? "A01" : OnlineUser.Operator.GetCurrent().Account;
            lineCode = string.IsNullOrEmpty(lineCode) ? userCode : lineCode;
            //获取正在进行中的订单
            var order_SecList = order_SecBLL.GetList().Where(u => u.ProductLine == lineCode);
            var ProceedOrder = order_SecList.Where(u => u.ProceedStatus == "1").FirstOrDefault();
            //若无进行中订单，显示当日最后结束时间的订单
            ProceedOrder = ProceedOrder == null ? (order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault() == null ? new IOT_Order_SecEntity() : order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault()) : ProceedOrder;
            ProceedOrder.QualifyQuantity = ProceedOrder.QualifyQuantity == null ? 0 : ProceedOrder.QualifyQuantity;
            ProceedOrder.UnqualifyQuantity = ProceedOrder.UnqualifyQuantity == null ? 0 : ProceedOrder.UnqualifyQuantity;
            string ProceedStatus = string.Empty;
            string StatusColor = string.Empty;
            //获取同一产线的当日订单
            switch (ProceedOrder.ProceedStatus)
            {
                case "0":
                    ProceedStatus = "未开始";
                    StatusColor = "gray";
                    break;
                case "1":
                    ProceedStatus = "生产中";
                    StatusColor = "green";
                    break;
                case "2":
                    ProceedStatus = "暂停中";
                    StatusColor = "red";
                    break;
                case "3":
                    ProceedStatus = "已完成";
                    StatusColor = "blue";
                    break;
                default:
                    break;
            }
            ViewBag.ProceedOrder = ProceedOrder;
            ViewBag.ProceedStatus = ProceedStatus;
            ViewBag.StatusColor = StatusColor;
            var siblingList = order_SecList.Where(u => u.ProductLine.Equals(ProceedOrder.ProductLine) && u.OrderDate == ProceedOrder.OrderDate).OrderBy(u => u.OrderBegin_Time).ToList();
            //获取当日总产线计划数量
            var order_sum_Q = 0;
            DataTable dt_sibling = new DataTable();
            dt_sibling.Columns.Add("OrderBegin_Time");
            dt_sibling.Columns.Add("OrderEnd_Time");
            dt_sibling.Columns.Add("OrderDate");
            dt_sibling.Columns.Add("ProductLine");
            foreach (var item in siblingList)
            {
                order_sum_Q += Convert.ToInt32(item.PlanQuantity);
                DataRow row_sibling = dt_sibling.NewRow();
                row_sibling["OrderBegin_Time"] = item.OrderBegin_Time;
                row_sibling["OrderEnd_Time"] = item.OrderEnd_Time;
                row_sibling["OrderDate"] = item.OrderDate;
                row_sibling["ProductLine"] = item.ProductLine;
                dt_sibling.Rows.Add(row_sibling);
            }
            ViewBag.SiblingsJson = JsonConvert.SerializeObject(dt_sibling);
            ViewBag.ProceedSiblingsOrder = siblingList;
            ViewBag.Order_Sum_Q = order_sum_Q;
            ////获取产线名称
            //ViewBag.ProductLineName = productLineBll.GetList().Where(u => u.LineCode == ProceedOrder.ProductLine).FirstOrDefault().LineName;
            //获取当日订单网关数据
            ViewBag.CurrentGatewayValues = JsonConvert.SerializeObject(tcp_GatewayValBLL.GetCurrentHourQuantity(ProceedOrder.OrderName));
            //获取产线班组信息
            ViewBag.SquadInfo = squadBll.GetList().Where(u => u.LineCode == lineCode).FirstOrDefault();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/OrderBoard.cshtml");
        }

        public ActionResult OrderBoard_Origin()
        {
            //获取当前产线
            var lineCode = Request["lineCode"];
            var userCode = OnlineUser.Operator.GetCurrent().IsSystem ? "A01" : OnlineUser.Operator.GetCurrent().Account;
            lineCode = string.IsNullOrEmpty(lineCode) ? userCode : lineCode;
            //获取正在进行中的订单
            var order_SecList = order_SecBLL.GetList().Where(u => u.ProductLine == lineCode);
            var ProceedOrder = order_SecList.Where(u => u.ProceedStatus == "1").FirstOrDefault();
            //若无进行中订单，显示当日最后结束时间的订单
            ProceedOrder = ProceedOrder == null ? (order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault() == null ? new IOT_Order_SecEntity() : order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault()) : ProceedOrder;
            ProceedOrder.QualifyQuantity = ProceedOrder.QualifyQuantity == null ? 0 : ProceedOrder.QualifyQuantity;
            ProceedOrder.UnqualifyQuantity = ProceedOrder.UnqualifyQuantity == null ? 0 : ProceedOrder.UnqualifyQuantity;
            string ProceedStatus = string.Empty;
            string StatusColor = string.Empty;
            //获取同一产线的当日订单
            switch (ProceedOrder.ProceedStatus)
            {
                case "0":
                    ProceedStatus = "未开始";
                    StatusColor = "gray";
                    break;
                case "1":
                    ProceedStatus = "生产中";
                    StatusColor = "green";
                    break;
                case "2":
                    ProceedStatus = "暂停中";
                    StatusColor = "red";
                    break;
                case "3":
                    ProceedStatus = "已完成";
                    StatusColor = "blue";
                    break;
                default:
                    break;
            }
            ViewBag.ProceedOrder = ProceedOrder;
            ViewBag.ProceedStatus = ProceedStatus;
            ViewBag.StatusColor = StatusColor;
            var siblingList = order_SecList.Where(u => u.ProductLine.Equals(ProceedOrder.ProductLine) && u.OrderDate == ProceedOrder.OrderDate).OrderBy(u => u.OrderBegin_Time).ToList();
            //获取当日总产线计划数量
            var order_sum_Q = 0;
            DataTable dt_sibling = new DataTable();
            dt_sibling.Columns.Add("OrderBegin_Time");
            dt_sibling.Columns.Add("OrderEnd_Time");
            dt_sibling.Columns.Add("OrderDate");
            dt_sibling.Columns.Add("ProductLine");
            foreach (var item in siblingList)
            {
                order_sum_Q += Convert.ToInt32(item.PlanQuantity);
                DataRow row_sibling = dt_sibling.NewRow();
                row_sibling["OrderBegin_Time"] = item.OrderBegin_Time;
                row_sibling["OrderEnd_Time"] = item.OrderEnd_Time;
                row_sibling["OrderDate"] = item.OrderDate;
                row_sibling["ProductLine"] = item.ProductLine;
                dt_sibling.Rows.Add(row_sibling);
            }
            ViewBag.SiblingsJson = JsonConvert.SerializeObject(dt_sibling);
            ViewBag.ProceedSiblingsOrder = siblingList;
            ViewBag.Order_Sum_Q = order_sum_Q;
            ////获取产线名称
            //ViewBag.ProductLineName = productLineBll.GetList().Where(u => u.LineCode == ProceedOrder.ProductLine).FirstOrDefault().LineName;
            //获取当日订单网关数据
            ViewBag.CurrentGatewayValues = JsonConvert.SerializeObject(tcp_GatewayValBLL.GetCurrentHourQuantity(ProceedOrder.OrderName));
            //获取产线班组信息
            ViewBag.SquadInfo = squadBll.GetList().Where(u => u.LineCode == lineCode).FirstOrDefault();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/OrderBoard_Origin.cshtml");
        }

        public ActionResult OrderBoard_DarkBlue()
        {
            //获取当前产线
            var lineCode = Request["lineCode"];
            var userCode = OnlineUser.Operator.GetCurrent().IsSystem ? "A01" : OnlineUser.Operator.GetCurrent().Account;
            lineCode = string.IsNullOrEmpty(lineCode) ? userCode : lineCode;
            //获取正在进行中的订单
            var order_SecList = order_SecBLL.GetList().Where(u => u.ProductLine == lineCode);
            var ProceedOrder = order_SecList.Where(u => u.ProceedStatus == "1").FirstOrDefault();
            //若无进行中订单，显示当日最后结束时间的订单
            ProceedOrder = ProceedOrder == null ? (order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault() == null ? new IOT_Order_SecEntity() : order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault()) : ProceedOrder;
            ProceedOrder.QualifyQuantity = ProceedOrder.QualifyQuantity == null ? 0 : ProceedOrder.QualifyQuantity;
            ProceedOrder.UnqualifyQuantity = ProceedOrder.UnqualifyQuantity == null ? 0 : ProceedOrder.UnqualifyQuantity;
            string ProceedStatus = string.Empty;
            string StatusColor = string.Empty;
            //获取同一产线的当日订单
            switch (ProceedOrder.ProceedStatus)
            {
                case "0":
                    ProceedStatus = "未开始";
                    StatusColor = "gray";
                    break;
                case "1":
                    ProceedStatus = "生产中";
                    StatusColor = "green";
                    break;
                case "2":
                    ProceedStatus = "暂停中";
                    StatusColor = "red";
                    break;
                case "3":
                    ProceedStatus = "已完成";
                    StatusColor = "blue";
                    break;
                default:
                    break;
            }
            ViewBag.ProceedOrder = ProceedOrder;
            ViewBag.ProceedStatus = ProceedStatus;
            ViewBag.StatusColor = StatusColor;
            var siblingList = order_SecList.Where(u => u.ProductLine.Equals(ProceedOrder.ProductLine) && u.OrderDate == ProceedOrder.OrderDate).OrderBy(u => u.OrderBegin_Time).ToList();
            //获取当日总产线计划数量
            var order_sum_Q = 0;
            DataTable dt_sibling = new DataTable();
            dt_sibling.Columns.Add("OrderBegin_Time");
            dt_sibling.Columns.Add("OrderEnd_Time");
            dt_sibling.Columns.Add("OrderDate");
            dt_sibling.Columns.Add("ProductLine");
            foreach (var item in siblingList)
            {
                order_sum_Q += Convert.ToInt32(item.PlanQuantity);
                DataRow row_sibling = dt_sibling.NewRow();
                row_sibling["OrderBegin_Time"] = item.OrderBegin_Time;
                row_sibling["OrderEnd_Time"] = item.OrderEnd_Time;
                row_sibling["OrderDate"] = item.OrderDate;
                row_sibling["ProductLine"] = item.ProductLine;
                dt_sibling.Rows.Add(row_sibling);
            }
            ViewBag.SiblingsJson = JsonConvert.SerializeObject(dt_sibling);
            ViewBag.ProceedSiblingsOrder = siblingList;
            ViewBag.Order_Sum_Q = order_sum_Q;
            ////获取产线名称
            //ViewBag.ProductLineName = productLineBll.GetList().Where(u => u.LineCode == ProceedOrder.ProductLine).FirstOrDefault().LineName;
            //获取当日订单网关数据
            ViewBag.CurrentGatewayValues = JsonConvert.SerializeObject(tcp_GatewayValBLL.GetCurrentHourQuantity(ProceedOrder.OrderName));
            //获取产线班组信息
            ViewBag.SquadInfo = squadBll.GetList().Where(u => u.LineCode == lineCode).FirstOrDefault();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/OrderBoard_DarkBlue.cshtml");
        }

        public ActionResult OrderBoard_Black()
        {
            //获取当前产线
            var lineCode = Request["lineCode"];
            var userCode = OnlineUser.Operator.GetCurrent().IsSystem ? "A01" : OnlineUser.Operator.GetCurrent().Account;
            lineCode = string.IsNullOrEmpty(lineCode) ? userCode : lineCode;
            //获取正在进行中的订单
            var order_SecList = order_SecBLL.GetList().Where(u => u.ProductLine == lineCode);
            var ProceedOrder = order_SecList.Where(u => u.ProceedStatus == "1").FirstOrDefault();
            //若无进行中订单，显示当日最后结束时间的订单
            ProceedOrder = ProceedOrder == null ? (order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault() == null ? new IOT_Order_SecEntity() : order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault()) : ProceedOrder;
            ProceedOrder.QualifyQuantity = ProceedOrder.QualifyQuantity == null ? 0 : ProceedOrder.QualifyQuantity;
            ProceedOrder.UnqualifyQuantity = ProceedOrder.UnqualifyQuantity == null ? 0 : ProceedOrder.UnqualifyQuantity;
            string ProceedStatus = string.Empty;
            string StatusColor = string.Empty;
            //获取同一产线的当日订单
            switch (ProceedOrder.ProceedStatus)
            {
                case "0":
                    ProceedStatus = "未开始";
                    StatusColor = "gray";
                    break;
                case "1":
                    ProceedStatus = "生产中";
                    StatusColor = "green";
                    break;
                case "2":
                    ProceedStatus = "暂停中";
                    StatusColor = "red";
                    break;
                case "3":
                    ProceedStatus = "已完成";
                    StatusColor = "blue";
                    break;
                default:
                    break;
            }
            ViewBag.ProceedOrder = ProceedOrder;
            ViewBag.ProceedStatus = ProceedStatus;
            ViewBag.StatusColor = StatusColor;
            var siblingList = order_SecList.Where(u => u.ProductLine.Equals(ProceedOrder.ProductLine) && u.OrderDate == ProceedOrder.OrderDate).OrderBy(u => u.OrderBegin_Time).ToList();
            //获取当日总产线计划数量
            var order_sum_Q = 0;
            DataTable dt_sibling = new DataTable();
            dt_sibling.Columns.Add("OrderBegin_Time");
            dt_sibling.Columns.Add("OrderEnd_Time");
            dt_sibling.Columns.Add("OrderDate");
            dt_sibling.Columns.Add("ProductLine");
            foreach (var item in siblingList)
            {
                order_sum_Q += Convert.ToInt32(item.PlanQuantity);
                DataRow row_sibling = dt_sibling.NewRow();
                row_sibling["OrderBegin_Time"] = item.OrderBegin_Time;
                row_sibling["OrderEnd_Time"] = item.OrderEnd_Time;
                row_sibling["OrderDate"] = item.OrderDate;
                row_sibling["ProductLine"] = item.ProductLine;
                dt_sibling.Rows.Add(row_sibling);
            }
            ViewBag.SiblingsJson = JsonConvert.SerializeObject(dt_sibling);
            ViewBag.ProceedSiblingsOrder = siblingList;
            ViewBag.Order_Sum_Q = order_sum_Q;
            ////获取产线名称
            //ViewBag.ProductLineName = productLineBll.GetList().Where(u => u.LineCode == ProceedOrder.ProductLine).FirstOrDefault().LineName;
            //获取当日订单网关数据
            ViewBag.CurrentGatewayValues = JsonConvert.SerializeObject(tcp_GatewayValBLL.GetCurrentHourQuantity(ProceedOrder.OrderName));
            //获取产线班组信息
            ViewBag.SquadInfo = squadBll.GetList().Where(u => u.LineCode == lineCode).FirstOrDefault();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/OrderBoard_Black.cshtml");
        }

        public ActionResult OrderBoard_Gray()
        {
            //获取当前产线
            var lineCode = Request["lineCode"];
            var userCode = OnlineUser.Operator.GetCurrent().IsSystem ? "A01" : OnlineUser.Operator.GetCurrent().Account;
            lineCode = string.IsNullOrEmpty(lineCode) ? userCode : lineCode;
            //获取正在进行中的订单
            var order_SecList = order_SecBLL.GetList().Where(u => u.ProductLine == lineCode);
            var ProceedOrder = order_SecList.Where(u => u.ProceedStatus == "1").FirstOrDefault();
            //若无进行中订单，显示当日最后结束时间的订单
            ProceedOrder = ProceedOrder == null ? (order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault() == null ? new IOT_Order_SecEntity() : order_SecList.OrderByDescending(u => u.OrderEnd_Time).FirstOrDefault()) : ProceedOrder;
            ProceedOrder.QualifyQuantity = ProceedOrder.QualifyQuantity == null ? 0 : ProceedOrder.QualifyQuantity;
            ProceedOrder.UnqualifyQuantity = ProceedOrder.UnqualifyQuantity == null ? 0 : ProceedOrder.UnqualifyQuantity;
            string ProceedStatus = string.Empty;
            string StatusColor = string.Empty;
            //获取同一产线的当日订单
            switch (ProceedOrder.ProceedStatus)
            {
                case "0":
                    ProceedStatus = "未开始";
                    StatusColor = "gray";
                    break;
                case "1":
                    ProceedStatus = "生产中";
                    StatusColor = "green";
                    break;
                case "2":
                    ProceedStatus = "暂停中";
                    StatusColor = "red";
                    break;
                case "3":
                    ProceedStatus = "已完成";
                    StatusColor = "blue";
                    break;
                default:
                    break;
            }
            ViewBag.ProceedOrder = ProceedOrder;
            ViewBag.ProceedStatus = ProceedStatus;
            ViewBag.StatusColor = StatusColor;
            var siblingList = order_SecList.Where(u => u.ProductLine.Equals(ProceedOrder.ProductLine) && u.OrderDate == ProceedOrder.OrderDate).OrderBy(u => u.OrderBegin_Time).ToList();
            //获取当日总产线计划数量
            var order_sum_Q = 0;
            DataTable dt_sibling = new DataTable();
            dt_sibling.Columns.Add("OrderBegin_Time");
            dt_sibling.Columns.Add("OrderEnd_Time");
            dt_sibling.Columns.Add("OrderDate");
            dt_sibling.Columns.Add("ProductLine");
            foreach (var item in siblingList)
            {
                order_sum_Q += Convert.ToInt32(item.PlanQuantity);
                DataRow row_sibling = dt_sibling.NewRow();
                row_sibling["OrderBegin_Time"] = item.OrderBegin_Time;
                row_sibling["OrderEnd_Time"] = item.OrderEnd_Time;
                row_sibling["OrderDate"] = item.OrderDate;
                row_sibling["ProductLine"] = item.ProductLine;
                dt_sibling.Rows.Add(row_sibling);
            }
            ViewBag.SiblingsJson = JsonConvert.SerializeObject(dt_sibling);
            ViewBag.ProceedSiblingsOrder = siblingList;
            ViewBag.Order_Sum_Q = order_sum_Q;
            ////获取产线名称
            //ViewBag.ProductLineName = productLineBll.GetList().Where(u => u.LineCode == ProceedOrder.ProductLine).FirstOrDefault().LineName;
            //获取当日订单网关数据
            ViewBag.CurrentGatewayValues = JsonConvert.SerializeObject(tcp_GatewayValBLL.GetCurrentHourQuantity(ProceedOrder.OrderName));
            //获取产线班组信息
            ViewBag.SquadInfo = squadBll.GetList().Where(u => u.LineCode == lineCode).FirstOrDefault();
            return View("~/Plugins/JFine.IOT/Views/IOTHome/OrderBoard_Gray.cshtml");
        }
        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Form()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.IOT/Views/IOTHome/Form.cshtml");
        }

        #endregion

        #region 数据获取
        //public string LoadHourProductivity(string deviceCode)
        //{
        //    return iOTDeviceBll.LoadHourProductivity(deviceCode).ToJson();
        //}
        #endregion

        #region 数据处理
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult UpdateOrderQuantity(string keyValue, string quantityType)
        {
            order_SecBLL.UpdateOrderQuantity(keyValue, quantityType);
            return Success("保存成功。");
        }
        #endregion

    }
}

