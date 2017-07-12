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
    public class OrganizeController : BaseController
    {
        private readonly IOrganizeService _organizeService;

        public OrganizeController(IOrganizeService organizeService)
        {
            this._organizeService = organizeService;
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Index(int pageIndex, int pageSize, string keyWord)
        {
            var pageData = _organizeService.GetList(pageIndex, pageSize, keyWord);
            var result = new LayPadding<Sys_Organize>()
            {
                result=true,
                msg = "success",
                list = pageData.Items,
                count = pageData.TotalItems
            };
            return Content(result.ToJson());
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost, AuthorizeChecked, ValidateAntiForgeryToken]
        public ActionResult Form(Sys_Organize model)
        {
            if (model.Id.IsNullOrEmpty())
            {
                var primaryKey = _organizeService.Insert(model);
                return primaryKey != null ? Success() : Error();
            }
            else
            {
                int row = _organizeService.Update(model);
                return row > 0 ? Success() : Error();
            }
        }

        [HttpPost]
        public ActionResult GetForm(string primaryKey)
        {
            var entity = _organizeService.Get(primaryKey);
            return Content(entity.ToJson());
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Delete(string primaryKey)
        {
            long count = _organizeService.GetChildCount(primaryKey);
            if (count == 0)
            {
                int row = _organizeService.Delete(primaryKey);
                return row > 0 ? Success() : Error();
            }
            return Error(string.Format("操作失败，请先删除该项的{0}个子级机构。", count));
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListTreeSelect()
        {
            var data = _organizeService.GetList();
            var treeList = new List<TreeSelect>();
            foreach (Sys_Organize item in data)
            {
                TreeSelect model = new TreeSelect();
                model.id = item.Id;
                model.text = item.FullName;
                model.parentId = item.ParentId;
                treeList.Add(model);
            }
            return Content(treeList.ToTreeSelectJson());
        }
    }
}
