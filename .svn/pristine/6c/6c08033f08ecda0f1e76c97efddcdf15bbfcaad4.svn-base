/********************************************************************************
**文 件 名:IOT_ProductLineController
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-10-12 10:15:32
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

namespace JFine.Plugins.IOT.Areas.IOT.Controllers
{	
	/// <summary>
	/// IOT_ProductLineController
	/// </summary>	
	public class IOT_ProductLineController:JFControllerBase
	{
		 private IOT_ProductLineBLL iOT_ProductLineBll = new IOT_ProductLineBLL();

        #region View
        //
        // GET: /IOT/
        public  ActionResult Index2()
        {
            return View("~/Plugins/JFine.IOT/Views/IOT_ProductLine/Index.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult Form2()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.IOT/Views/IOT_ProductLine/Form.cshtml");
        }
        [HttpGet]
        [HandlerAuthorize]
        public  ActionResult DistributeProductLine()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.IOT/Views/IOT_ProductLine/DistributeProductLine.cshtml");
        }
        #endregion

        #region 数据获取
        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string keyword)
        {
            var data = iOT_ProductLineBll.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Id.Contains(keyword), "");
            }
            var treeList = new List<TreeSelectModel>();
            foreach (IOT_ProductLineEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Id;
                treeModel.parentId = item.Id;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        
		}

		/// <summary>
        /// 列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = iOT_ProductLineBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取目录级功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = iOT_ProductLineBll.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Id.Contains(keyword), "");
            }
            var treeList = new List<TreeGridModel>();
            foreach (IOT_ProductLineEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.Id == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.Id;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = iOT_ProductLineBll.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="queryJson">查询字符串</param>
        /// <returns>下拉选择Json</returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string queryJson)
        {
            var data = iOT_ProductLineBll.GetList(queryJson).ToList();
            List<object> list = new List<object>();
            foreach (IOT_ProductLineEntity item in data)
            {
                list.Add(new { id = item.LineCode, text = item.LineName });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取关联用户产线
        /// </summary>
        /// <param name="keyValue">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRelvantProductLine(string keyValue)
        {
            var data = iOT_ProductLineBll.GetRelvantProductLine(keyValue);
            return Content(data.ToJson());
        }

        #endregion

        #region 数据处理


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="IOT_ProductLineEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, IOT_ProductLineEntity iOT_ProductLineEntity)
        {
            iOT_ProductLineBll.SaveForm(keyValue, iOT_ProductLineEntity);
            return Success("保存成功。");
        }

		/// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            iOT_ProductLineBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRelvantProductLine(string keyValue,string productLineListJson)
        {
            iOT_ProductLineBll.SaveRelvantProductLine(keyValue, productLineListJson);
            return Success("操作成功。");
        }
        #endregion

    }
}

