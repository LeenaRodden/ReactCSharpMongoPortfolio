using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var users = UserModel.GetList();
            if(users == null || users.Count == 0)
            {
                return RedirectToAction("CreateUser");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if (!AdminBaseController.AuthenticateUser(username, password, Response))
            {
                ViewBag.errors = "Invalid username and/or password";
                return View();
            }
            else
            {                
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            List<HttpCookie> cookiesToAdd = new List<HttpCookie>();
            foreach (var cookie in Request.Cookies.Keys)
            {
                var c = new HttpCookie(cookie.ToString());
                c.Expires = DateTime.Now.AddDays(-1);
                // Response.Cookies.Add(c);
                cookiesToAdd.Add(c);
            }
            foreach (var c in cookiesToAdd)
            {
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        /// <summary>
        /// Method to create initial user with a valid password. Not used if there are already users.
        /// </summary>
        [HttpPost]
        
        public ActionResult CreateUser(UserModel model, string password)
        {
            var users = UserModel.GetList();
            if (users.Count > 0)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var newSalt = UserModel.GenerateSalt();
                var hashedPassword = UserModel.ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(newSalt));
                model.Hash = hashedPassword;
                model.Salt = newSalt;
                model.Insert();

                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                ViewBag.errors = ex.Message;
                return View(model);
            }
        }
    }
}