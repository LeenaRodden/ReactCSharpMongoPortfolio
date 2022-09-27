using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class TechnologyController : AdminBaseController
    {
        // GET: Technology
        public ActionResult Index()
        {
            var list = TechnologyModel.GetList();
            return View(list);
        }

        // GET: Technology/Details/5
        public ActionResult Details(int id)
        {
            var model = TechnologyModel.GetList().Where(p=>p.TechnologyId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // GET: Technology/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Technology/Create
        [HttpPost]
        public ActionResult Create(TechnologyModel model)
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

        // GET: Technology/Edit/5
        public ActionResult Edit(int id)
        {
            var model = TechnologyModel.GetList().Where(p => p.TechnologyId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: Technology/Edit/5
        [HttpPost]
        public ActionResult Edit(TechnologyModel model)
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

        // GET: Technology/Delete/5
        public ActionResult Delete(int id)
        {
            var model = TechnologyModel.GetList().Where(p => p.TechnologyId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: Technology/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                TechnologyModel.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                var model = TechnologyModel.GetList().Where(p => p.TechnologyId.Equals(id)).FirstOrDefault();
                return View(model);
            }
        }

        public ActionResult MoveUp(int id)
        {
            var model = TechnologyModel.GetList().Where(p => p.TechnologyId.Equals(id)).FirstOrDefault();
            model.Order = model.Order + 1;
            model.Update();
            return RedirectToAction("Index");
        }
        public ActionResult MoveDown(int id)
        {
            var model = TechnologyModel.GetList().Where(p => p.TechnologyId.Equals(id)).FirstOrDefault();
            model.Order = model.Order - 1;
            model.Update();
            return RedirectToAction("Index");
        }
    }
}
