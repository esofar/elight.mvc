using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elight.Web.Filters;
using Elight.Web.Controllers;
using Elight.Entity;
using Elight.Infrastructure;
using Elight.IService;

namespace Elight.Web.Areas.System.Controllers
{
    [LoginChecked]
    public class RoleAuthorizeController : BaseController
    {
        private readonly IRoleAuthorizeService _roleAuthorizeService;
        private readonly IPermissionService _permissionService;

        public RoleAuthorizeController(IRoleAuthorizeService roleAuthorizeService, IPermissionService permissionService)
        {
            this._roleAuthorizeService = roleAuthorizeService;
            this._permissionService = permissionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string roleId)
        {
            var listPerIds = _roleAuthorizeService.GetList(roleId).Select(c => c.ModuleId).ToList();
            var listAllPers = _permissionService.GetList();
            List<ZTreeNode> result = new List<ZTreeNode>();
            foreach (var item in listAllPers)
            {
                ZTreeNode model = new ZTreeNode();
                model.@checked = listPerIds.Contains(item.Id) ? model.@checked = true : model.@checked = false;
                model.id = item.Id;
                model.pId = item.ParentId;
                model.name = item.Name;
                model.open = true;
                result.Add(model);
            }
            return Content(result.ToJson());
        }

        [HttpPost]
        public ActionResult Form(string roleId, string perIds)
        {
            _roleAuthorizeService.Authorize(roleId, perIds.ToStrArray());
            return Success("授权成功");
        }

    }
}
