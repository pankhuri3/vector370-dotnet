using System.Web.Mvc;

namespace CallbackReceiver
{
    /// <summary />
    public class FilterConfig
    {
        /// <summary />
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
