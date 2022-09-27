using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace API.Controllers
{
    public class ExperienceController : AdminBaseController
    {
        // GET: Experience
        public ActionResult Index()
        {
            var list = ExperienceModel.GetList();
            return View(list);
        }

        // GET: Experience/Details/5
        public ActionResult Details(int id)
        {
            var model = ExperienceModel.GetList().Where(p => p.ExperienceId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // GET: Experience/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experience/Create
        [HttpPost]
        public ActionResult Create(ExperienceModel model)
        {
            try
            {
                // TODO: Add insert logic here
                model.Insert();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                return View(model);
            }
        }

        // GET: Experience/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ExperienceModel.GetList().Where(p => p.ExperienceId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: Experience/Edit/5
        [HttpPost]
        public ActionResult Edit(ExperienceModel model)
        {
            try
            {
                // TODO: Add update logic here
                model.Update();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                return View(model);
            }
        }

        // GET: Experience/Delete/5
        public ActionResult Delete(int id)
        {
            var model = ExperienceModel.GetList().Where(p => p.ExperienceId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: Experience/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ExperienceModel.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                var model = ExperienceModel.GetList().Where(p => p.ExperienceId.Equals(id)).FirstOrDefault();
                return View(model);
            }
        }

        public ActionResult MoveUp(int id)
        {
            var model = ExperienceModel.GetList().Where(p => p.ExperienceId.Equals(id)).FirstOrDefault();
            model.Order = model.Order + 1;
            model.Update();
            return RedirectToAction("Index");
        }
        public ActionResult MoveDown(int id)
        {
            var model = ExperienceModel.GetList().Where(p => p.ExperienceId.Equals(id)).FirstOrDefault();
            model.Order = model.Order - 1;
            model.Update();
            return RedirectToAction("Index");
        }
    }
}
