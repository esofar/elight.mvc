using Elight.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace Elight.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorCheckedAttribute());  
        }
    }
}