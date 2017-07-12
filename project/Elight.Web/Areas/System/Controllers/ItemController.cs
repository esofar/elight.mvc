using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elight.Web.Controllers;
using Elight.Entity;
using Elight.Infrastructure;
using Elight.Web.Filters;
using Elight.IService;

namespace Elight.Web.Areas.System.Controllers
{
    [LoginChecked]
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;
        private readonly IItemsDetailService _itemsDetailService;

        public ItemController(IItemService itemService, IItemsDetailService itemsDetailService)
        {
            this._itemService = itemService;
            this._itemsDetailService = itemsDetailService;
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Index(long pageIndex, long pageSize, string keyWord)
        {
            var pageData = _itemService.GetList(pageIndex, pageSize, keyWord);
            var result = new LayPadding<Sys_Item>()
            {
                result = true,
                msg = "success",
                list = pageData.Items,
                count = pageData.TotalItems
            };
            return Content(result.ToJson());
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(Sys_Item model)
        {
            if (model.Id.IsNullOrEmpty())
            {
                var primaryKey = _itemService.Insert(model);
                return primaryKey != null ? Success() : Error();
            }
            else
            {
                int row = _itemService.Update(model);
                return row > 0 ? Success() : Error();
            }
        }

        [HttpPost]
        public ActionResult GetForm(string primaryKey)
        {
            var entity = _itemService.Get(primaryKey);
            return Content(entity.ToJson());
        }

        [HttpPost]
        public ActionResult Delete(string primaryKey)
        {
            long count = _itemService.GetChildCount(primaryKey);
            if (count == 0)
            {
                //删除字典。
                int row = _itemService.Delete(primaryKey);
                //删除字典选项。
                _itemsDetailService.Delete(primaryKey);
                return row > 0 ? Success() : Error();
            }
            return Warning(string.Format("操作失败，请先删除该项的{0}个子级字典。", count));
        }

        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListTree()
        {
            var listAllItems = _itemService.GetList();
            List<ZTreeNode> result = new List<ZTreeNode>();
            foreach (var item in listAllItems)
            {
                ZTreeNode model = new ZTreeNode();
                model.id = item.Id;
                model.pId = item.ParentId;
                model.name = item.Name;
                model.open = true;
                result.Add(model);
            }
            return Content(result.ToJson());
        }

        [HttpPost]
        public ActionResult GetListSelectTree()
        {
            var data = _itemService.GetList();
            var treeList = new List<TreeSelect>();
            foreach (var item in data)
            {
                TreeSelect model = new TreeSelect();
                model.id = item.Id;
                model.text = item.Name;
                model.parentId = item.ParentId;
                treeList.Add(model);
            }
            return Content(treeList.ToTreeSelectJson());
        }
    }
}
