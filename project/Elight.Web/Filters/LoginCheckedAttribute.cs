using Elight.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elight.Web.Filters
{
    /// <summary>
    /// 表示一个特性，该特性用于标识用户是否需要登陆。
    /// </summary>
    public class LoginCheckedAttribute : ActionFilterAttribute
    {

        public bool Ignore { get; set; }
        public LoginCheckedAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Ignore)
            {
                return;
            }
            if (OperatorProvider.Instance.Current == null)
            {
                filterContext.HttpContext.Response.Write("<script>top.location.href = '/Account/Login'</script>");
            }
        }
    }
}