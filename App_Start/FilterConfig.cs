using System.Web;
using System.Web.Mvc;

namespace ASP_NET_Gantt__Excel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
