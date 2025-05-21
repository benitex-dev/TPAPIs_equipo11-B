using System.Web;
using System.Web.Mvc;

namespace TPAPIs_equipo11_B
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
