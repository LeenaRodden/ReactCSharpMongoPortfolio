using API.Models;
using Encryption;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace API.Controllers
{
    public abstract class AdminBaseController : Controller
    {

        private static string encPassword = ConfigurationManager.AppSettings.Get("AdminKey");


        private ActionExecutingContext filterContext;

        internal static bool AuthenticateUser(string username, string password, HttpResponseBase response)
        {
            var model = UserModel.GetList().Where(p => p.Username == username).First();
            if(model != null)
            {
                var hashedPassword = UserModel.ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(model.Salt));
                if(hashedPassword != null && hashedPassword == model.Hash)
                {
                    var ckeID = new HttpCookie("id");
                    ckeID.Value = AESThenHMAC.SimpleEncryptWithPassword(model.UserId.ToString(), encPassword);
                    ckeID.SameSite = SameSiteMode.None;
                    ckeID.Secure = true;
                    ckeID.Expires = DateTime.Now.AddDays(7);
                    response.SetCookie(ckeID);


                    var ckeDisplayName = new HttpCookie("username");
                    ckeDisplayName.Value = model.Username;
                    ckeDisplayName.SameSite = SameSiteMode.None;
                    ckeDisplayName.Secure = true;
                    ckeDisplayName.Expires = DateTime.Now.AddDays(7);
                    response.SetCookie(ckeDisplayName);

                    return true;
                }
            }
            return false;

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.filterContext = filterContext;
            var account = filterContext.HttpContext.Request.Cookies["id"];
            var username = filterContext.HttpContext.Request.Cookies["username"];
            if(account != null && username != null)
            {
                string accountStr = AESThenHMAC.SimpleDecryptWithPassword(account.Value, encPassword);
                int accountID = 0;
                if (!Int32.TryParse(accountStr, out accountID))
                {
                    account = null;
                }
                else
                {
                    try
                    {
                        var user = UserModel.GetList().Where(p => p.UserId == accountID && p.Username == username.Value).First();
                        if (user == null)
                        {
                            account = null;
                        }
                    }
                    catch
                    {
                        account = null;
                    }

                }
            }
            if (account == null)
            {
                //This does not always stop Route from processing
                //filterContext.HttpContext.Response.Redirect("~/Login");
                //filterContext.HttpContext.Response.End();
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
        }
    }
}