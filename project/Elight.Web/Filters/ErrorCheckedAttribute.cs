using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elight.Infrastructure;
using System.Text;

namespace Elight.Web.Filters
{
    /// <summary>
    /// 表示一个特性，该特性用于捕获程序运行异常。
    /// </summary>
    public class ErrorCheckedAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.ExceptionHandled = true;
            StringBuilder script = new StringBuilder();

            if (OperatorProvider.Instance.Current == null)
            {
                script.Append("<script>top.alert('登陆超时，请重新认证。'); top.window.location.href='/Account/Login'</script>");
                filterContext.Result = new ContentResult() { Content = script.ToString() };
            }
            else
            {
                Operator onlineUser = OperatorProvider.Instance.Current;
                LogHelper.Write(Level.Error, "程序抛异常", filterContext.Exception.StackTrace, onlineUser.Account, onlineUser.RealName);
                script.Append("<script>top.window.alert('系统出现异常，请联系开发人员确认。');</script>");
                filterContext.Result = new ContentResult() { Content = script.ToString() };
            }
        }
    }
}