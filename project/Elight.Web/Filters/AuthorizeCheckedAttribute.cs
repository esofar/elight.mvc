using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Elight.Infrastructure;
using Elight.IService;

namespace Elight.Web.Filters
{
    /// <summary>
    /// 表示一个特性，该特性用于标识用户是否有访问权限。
    /// </summary>
    public class AuthorizeCheckedAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 是否忽略权限检查。
        /// </summary>
        public bool Ignore { get; set; }

        public AuthorizeCheckedAttribute(bool ignore = false)
        {
            this.Ignore = ignore;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Ignore)
            {
                return;
            };
            var userId = OperatorProvider.Instance.Current.UserId;
            var action = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            bool hasPermission = AutoFacConfig.Resolve<IPermissionService>().ActionValidate(userId, action);
            if (!hasPermission)
            {
                StringBuilder script = new StringBuilder();
                script.Append("<script>alert('对不起，您没有权限访问当前页面。');</script>");
                filterContext.Result = new ContentResult() { Content = script.ToString() };
            }
        }


    }
}