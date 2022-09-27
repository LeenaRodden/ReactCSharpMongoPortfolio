using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : AdminBaseController
    {

        public string Version()
        {
            return "0.31";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "React Portfolio";

            return View();
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Index", "ContactInfo");
        }

    }
}