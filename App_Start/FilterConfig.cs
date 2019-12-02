using System.Web;
using System.Web.Mvc;

namespace MVC5_EF6_SP_3_tier_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
