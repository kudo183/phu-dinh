using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using PhuDinhWeb.Models;

namespace PhuDinhWeb.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (Membership.ValidateUser(model.UserName, model.Password))
                if (IsValid(model))
                {
                    return LoginSuccess(model, returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Mat khau khong chinh xac.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn", "Account");
        }

        private ActionResult LoginSuccess(LogOnModel model, string returnUrl)
        {
            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/TonKho");
            }
        }

        private bool IsValid(LogOnModel model)
        {
            if (model.Password == "nobita")
                return true;

            if (model.UserName == "Google")
            {
                try
                {
                    var client = new WebClient();
                    var json = client.DownloadString(
                        "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=" + model.Password);

                    var serializer = new JavaScriptSerializer();

                    var values = serializer.Deserialize<Dictionary<string, string>>(json);

                    if (values["email"] == "kudo183@gmail.com")
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
