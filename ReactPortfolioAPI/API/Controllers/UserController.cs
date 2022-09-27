using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class UserController : AdminBaseController
    {
        // GET: User
        public ActionResult Index()
        {
            var list = UserModel.GetList();
            return View(list);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var model = UserModel.GetList().Where(p => p.UserId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View("CreateUser");
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserModel model, string password)
        {
            try
            {
                var newSalt = UserModel.GenerateSalt();
                var hashedPassword = UserModel.ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(newSalt));
                model.Hash = hashedPassword;
                model.Salt = newSalt;
                model.Insert();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.errors = ex.Message;
                return View("CreateUser",model);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var model = UserModel.GetList().Where(p => p.UserId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserModel model, string password)
        {
            try
            {
                // TODO: Add update logic here
                var newSalt = UserModel.GenerateSalt();
                var hashedPassword = UserModel.ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(newSalt));
                model.Hash = hashedPassword;
                model.Salt = newSalt;
                model.Update();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                return View(model);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var model = UserModel.GetList().Where(p => p.UserId.Equals(id)).FirstOrDefault();
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection fc)
        {
            try
            {
                // TODO: Add delete logic here
                UserModel.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                var model = UserModel.GetList().Where(p => p.UserId.Equals(id)).FirstOrDefault();
                return View(model);
            }
        }


    }
}
