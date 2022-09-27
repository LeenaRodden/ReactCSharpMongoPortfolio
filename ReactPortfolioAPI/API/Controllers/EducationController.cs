using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class EducationController : AdminBaseController
    {
        // GET: Education
        public ActionResult Index()
        {
            var list = EducationModel.GetList();
            return View(list);
        }

        // GET: Education/Details/5
        public ActionResult Details(int id)
        {
            var model = EducationModel.GetList().Where(p => p.EducationId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // GET: Education/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Education/Create
        [HttpPost]
        public ActionResult Create(EducationModel model)
        {
            try
            {
                model.Insert();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.errors = ex.Message;
                return View(model);
            }
        }

        // GET: Education/Edit/5
        public ActionResult Edit(int id)
        {
            var model = EducationModel.GetList().Where(p => p.EducationId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: Education/Edit/5
        [HttpPost]
        public ActionResult Edit(EducationModel model)
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

        // GET: Education/Delete/5
        public ActionResult Delete(int id)
        {
            var model = EducationModel.GetList().Where(p => p.EducationId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: Education/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection fc)
        {
            try
            {
                // TODO: Add delete logic here
                EducationModel.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                var model = EducationModel.GetList().Where(p => p.EducationId.Equals(id)).FirstOrDefault();
                return View(model);
            }
        }

        public ActionResult MoveUp(int id)
        {
            var model = EducationModel.GetList().Where(p => p.EducationId.Equals(id)).FirstOrDefault();
            model.Order = model.Order + 1;
            model.Update();
            return RedirectToAction("Index");
        }
        public ActionResult MoveDown(int id)
        {
            var model = EducationModel.GetList().Where(p => p.EducationId.Equals(id)).FirstOrDefault();
            model.Order = model.Order - 1;
            model.Update();
            return RedirectToAction("Index");
        }
    }
}
