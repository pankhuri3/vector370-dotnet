using System.Web.Http;
using System.Web.Mvc;

namespace CallbackReceiver.Areas.HelpPage
{
    /// <summary />
    public class HelpPageAreaRegistration : AreaRegistration
    {
        /// <summary />
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        /// <summary />
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}