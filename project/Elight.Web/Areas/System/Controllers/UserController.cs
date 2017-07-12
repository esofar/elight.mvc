using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserLogOnService _userLogOnService;
        private readonly IUserRoleRelationService _userRoleRelationService;

        public UserController(IUserService userService, IUserRoleRelationService userRoleRelationService, IUserLogOnService userLogOnService)
        {
            this._userService = userService;
            this._userRoleRelationService = userRoleRelationService;
            this._userLogOnService = userLogOnService;
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Index(int pageIndex, int pageSize, string keyWord)
        {
            var pageData = _userService.GetList(pageIndex, pageSize, keyWord);
            var result = new LayPadding<Sys_User>()
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
        public ActionResult Form(Sys_User model, string password, string roleIds)
        {
            if (model.Id.IsNullOrEmpty())
            {
                //新增用户基本信息。
                var userId = _userService.Insert(model).ToString();
                //新增用户角色信息。
                _userRoleRelationService.SetRole(userId, roleIds.ToStrArray());
                //新增用户登陆信息。
                Sys_UserLogOn userLogOnEntity = new Sys_UserLogOn()
                  {
                      UserId = userId,
                      Password = password
                  };
                var userLoginId = _userLogOnService.Insert(userLogOnEntity);
                return userId != null && userLoginId != null ? Success() : Error();
            }
            else
            {
                //更新用户基本信息。
                int row = _userService.Update(model);
                //更新用户角色信息。
                _userRoleRelationService.SetRole(model.Id, roleIds.ToStrArray());
                return row > 0 ? Success() : Error();
            }
        }

        [HttpGet, AuthorizeChecked]
        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetForm(string primaryKey)
        {
            var entity = _userService.Get(primaryKey);

            entity.RoleId = _userRoleRelationService.GetList(entity.Id).Select(c => c.RoleId).ToList();

            return Content(entity.ToJson());
        }

        [HttpPost, AuthorizeChecked]
        public ActionResult Delete(string userIds)
        {
            //多用户删除。
            int row = _userService.Delete(userIds.ToStrArray());
            _userRoleRelationService.Delete(userIds.ToStrArray());
            _userLogOnService.Delete(userIds.ToStrArray());
            return row > 0 ? Success() : Error();
        }

        [HttpPost]
        public ActionResult CheckAccount(string userName)
        {
            var userEntity = _userService.GetByUserName(userName);
            if (userEntity != null)
            {
                return Error("已存在当前用户名，请重新输入");
            }
            return Success("恭喜您，该用户名可以注册");
        }


    }
}
