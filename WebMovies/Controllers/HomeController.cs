using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMovies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Find it all about your favorite movies.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ask any question directly to me in the address below.";

            return View();
        }
    }
}