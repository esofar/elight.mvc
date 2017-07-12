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
    public class ItemsDetailController : BaseController
    {
        private readonly IItemsDetailService _itemsDetailService;

        public ItemsDetailController(IItemsDetailService itemsDetailService)
        {
            this._itemsDetailService = itemsDetailService;
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Index(int pageIndex, int pageSize, string itemId, string keyWord)
        {
            var pageData = _itemsDetailService.GetList(pageIndex, pageSize, itemId, keyWord);
            var result = new LayPadding<Sys_ItemsDetail>()
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
        public ActionResult Form(Sys_ItemsDetail model)
        {
            if (model.Id.IsNullOrEmpty())
            {
                var primaryKey = _itemsDetailService.Insert(model);
                return primaryKey != null ? Success() : Error();
            }
            else
            {
                int row = _itemsDetailService.Update(model);
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
            int row = _itemsDetailService.Delete(primaryKey);
            return row > 0 ? Success() : Error();
        }

        [HttpPost]
        public ActionResult GetForm(string primaryKey)
        {
            var entity = _itemsDetailService.Get(primaryKey);
            return Content(entity.ToJson());
        }

    }
}
