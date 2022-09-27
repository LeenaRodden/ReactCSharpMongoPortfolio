using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : AdminBaseController
    {

        private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        public string Version()
        {
            return "0.3";
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

        [Helpers.AllowCrossSite]
        [HttpGet]
        public ActionResult WeatherForecast()
        {
            Random r = new Random();

            var arr = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = r.Next(-20, 55),
                Summary = Summaries[r.Next(Summaries.Length)]
            })
            .ToArray();

            return Json(arr, JsonRequestBehavior.AllowGet);
            //return null;
        }

    }

        public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

}