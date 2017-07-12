using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Elight.Web.Filters;
using Elight.Web.Controllers;
using Elight.Entity;
using Elight.Infrastructure;
using Elight.IService;

namespace Elight.Web.Areas.System.Controllers
{
    public class UserLogOnController : BaseController
    {
        private readonly IUserLogOnService _userLogOnService;

        public UserLogOnController(IUserLogOnService userLogOnService)
        {
            this._userLogOnService = userLogOnService;
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Form(Sys_UserLogOn model)
        {
            if (model.Id.IsNullOrEmpty())
            {
                var primaryKey = _userLogOnService.Insert(model);
                return primaryKey != null ? Success() : Error();
            }
            else
            {
                var row = _userLogOnService.UpdateInfo(model);
                return row > 0 ? Success() : Error();
            }
        }

    }
}
