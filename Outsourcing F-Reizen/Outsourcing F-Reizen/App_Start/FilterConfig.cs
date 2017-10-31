using System.Web;
using System.Web.Mvc;

namespace Outsourcing_F_Reizen
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
