using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class ContactInfoController : AdminBaseController
    {
        // GET: ContactInfo
        public ActionResult Index()
        {
            var list = ContactInfoModel.GetList();
            return View(list);
        }

        // GET: ContactInfo/Details/5
        public ActionResult Details(int id)
        {
            var model = ContactInfoModel.GetList().Where(p => p.ContactInfoId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // GET: ContactInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactInfo/Create
        [HttpPost]
        public ActionResult Create(ContactInfoModel model)
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

        // GET: ContactInfo/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ContactInfoModel.GetList().Where(p => p.ContactInfoId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: ContactInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(ContactInfoModel model)
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

        // GET: ContactInfo/Delete/5
        public ActionResult Delete(int id)
        {
            var model = ContactInfoModel.GetList().Where(p => p.ContactInfoId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: ContactInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection fc)
        {
            try
            {
                // TODO: Add delete logic here
                ContactInfoModel.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                var model = ContactInfoModel.GetList().Where(p => p.ContactInfoId.Equals(id)).FirstOrDefault();
                return View(model);
            }
        }

        public ActionResult MoveUp(int id)
        {
            var model = ContactInfoModel.GetList().Where(p => p.ContactInfoId.Equals(id)).FirstOrDefault();
            model.Order = model.Order + 1;
            model.Update();
            return RedirectToAction("Index");
        }
        public ActionResult MoveDown(int id)
        {
            var model = ContactInfoModel.GetList().Where(p => p.ContactInfoId.Equals(id)).FirstOrDefault();
            model.Order = model.Order - 1;
            model.Update();
            return RedirectToAction("Index");
        }
    }
}
