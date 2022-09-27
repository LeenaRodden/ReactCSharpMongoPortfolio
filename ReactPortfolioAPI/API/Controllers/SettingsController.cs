using API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace API.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            var model = SettingsModel.GetSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SettingsModel model)
        {
            try
            {
                var idStr = Request["_id"];
                model._id = new MongoDB.Bson.ObjectId(idStr);
                model.Update();

                ViewBag.errors = "Saved successfully";
            }
            catch(Exception ex)
            {
                ViewBag.errors = ex.Message;    
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ExportData()
        {
            ViewBag.errors = "";
            string exportFolder = ConfigurationManager.AppSettings.Get("ExportFolder");
            ViewBag.exportFolder = exportFolder;
            return View();
        }

        [HttpPost]
        public ActionResult ExportData(FormCollection fc)
        {
            ViewBag.errors = "";
            try
            {
                bool education = false, experience = false, technology = false, website = false, contactInfo = false, settings = false;
                string exportFolder;
                exportFolder = fc.Get("exportFolder");
                if (fc.Get("education") != null)
                {
                    education = true;
                }
                if (fc.Get("experience") != null)
                {
                    experience = true;
                }
                if (fc.Get("technology") != null)
                {
                    technology = true;
                }
                if (fc.Get("website") != null)
                {
                    website = true;
                }
                if (fc.Get("contactInfo") != null)
                {
                    contactInfo = true;
                }
                if (fc.Get("settings") != null)
                {
                    settings = true;
                }
                if (education)
                {
                    var model = EducationModel.GetList();
                    saveData("education.json", model, exportFolder);
                }
                if (experience)
                {
                    var model = ExperienceModel.GetList();
                    saveData("experience.json", model, exportFolder);
                }
                if (technology)
                {
                    var model = TechnologyModel.GetList();
                    saveData("technology.json",model, exportFolder);
                }
                if(website)
                {
                    var model = WebsiteModel.GetList();
                    saveData("website.json", model, exportFolder);
                }
                if (contactInfo)
                {
                    var model = ContactInfoModel.GetList();
                    saveData("contactinfo.json", model, exportFolder);
                }
                if (settings)
                {
                    var model = SettingsModel.GetSettings();
                    saveData("settings.json", model, exportFolder);
                }



            }
            catch(Exception ex)
            {
                ViewBag.errors = ex.Message; 
            }

            if(ViewBag.errors == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private void saveData(string fileName, object model, string exportFolder)
        {
            //var json = Json(model);
            //string s = json.Data.ToString();
            //throw new NotImplementedException();
            string fullPath = exportFolder + fileName;
            string json = new JavaScriptSerializer().Serialize(model);
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.Write(json);
            }

        }
    }
}
