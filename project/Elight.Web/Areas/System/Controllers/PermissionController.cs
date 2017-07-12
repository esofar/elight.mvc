using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Elight.Web.Filters;
using Elight.Web.Controllers;
using Elight.Entity;
using Elight.Infrastructure;
using Elight.IService;

namespace Elight.Web.Areas.System.Controllers
{
    [LoginChecked]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            this._permissionService = permissionService;
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Index(int pageIndex, int pageSize, string keyWord)
        {
            var pageData = _permissionService.GetList(pageIndex, pageSize, keyWord);
            var result = new LayPadding<Sys_Permission>()
            {
                result = true,
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
        public ActionResult Form(Sys_Permission model)
        {
            if (model.Id.IsNullOrEmpty())
            {
                var primaryKey = _permissionService.Insert(model);
                return primaryKey != null ? Success() : Error();
            }
            else
            {
                int row = _permissionService.Update(model);
                return row > 0 ? Success() : Error();
            }
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Delete(string primaryKey)
        {
            long count = _permissionService.GetChildCount(primaryKey);
            if (count == 0)
            {
                int row = _permissionService.Delete(primaryKey.ToStrArray());
                return row > 0 ? Success() : Error();
            }
            return Error(string.Format("操作失败，请先删除该项的{0}个子级权限。", count));
        }

        [HttpPost]
        public ActionResult GetForm(string primaryKey)
        {
            var entity = _permissionService.Get(primaryKey);
            return Content(entity.ToJson());
        }

        [HttpPost]
        public ActionResult GetParent()
        {
            var data = _permissionService.GetList();
            var treeList = new List<TreeSelect>();
            foreach (Sys_Permission item in data)
            {
                TreeSelect model = new TreeSelect();
                model.id = item.Id;
                model.text = item.Name;
                model.parentId = item.ParentId;
                treeList.Add(model);
            }
            return Content(treeList.ToTreeSelectJson());
        }

        [HttpGet]
        public ActionResult Icon()
        {
            return View();
        }

    }

}
