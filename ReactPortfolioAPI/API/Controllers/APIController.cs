using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
   
    public class APIController : Controller
    {
        // GET: API - Home page
        public ActionResult Index()
        {
            return RedirectToAction("Technology");
        }

        public ActionResult Portfolio()
        {
            return RedirectToAction("Website");
        }

        [Helpers.AllowCrossSite]
        public JsonResult Technology()
        {
            var model = TechnologyModel.GetList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Helpers.AllowCrossSite]
        public JsonResult Website()
        {
            var model = WebsiteModel.GetList();
            var result = Json(model, JsonRequestBehavior.AllowGet);
            return result;
        }

        [Helpers.AllowCrossSite]
        public JsonResult Education()
        {
            var model = EducationModel.GetList();
            return Json(model,JsonRequestBehavior.AllowGet);    
        }

        [Helpers.AllowCrossSite]
        public JsonResult Experience()
        {
            var model = ExperienceModel.GetList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Helpers.AllowCrossSite]
        public JsonResult ContactInfo()
        {
            var model = ContactInfoModel.GetList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Helpers.AllowCrossSite]
        public JsonResult Settings()
        {
            var model = SettingsModel.GetSettings();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}