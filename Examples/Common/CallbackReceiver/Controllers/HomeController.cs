using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CallbackReceiver.Controllers
{
    /// <summary />
    public class HomeController : Controller
    {
        /// <summary />
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
