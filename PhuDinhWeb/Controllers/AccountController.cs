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
                return IsValidGoogle(model);
            }

            if (model.UserName == "FaceBook")
            {
                return IsValidFaceBook(model);
            }
            return false;
        }

        private bool IsValidGoogle(LogOnModel model)
        {
            try
            {
                var client = new WebClient();
                var serializer = new JavaScriptSerializer();
                var json = "";
                var email = "";
                var appID = "";

                json = client.DownloadString(
                    "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=" + model.Password);

                var values = serializer.Deserialize<Dictionary<string, string>>(json);

                email = values["email"];
                appID = values["aud"];

                if (email == "kudo183@gmail.com" && appID == "735134733733-c5h3gefmuokq7u6hl802q2u8sv8f4r7c.apps.googleusercontent.com")
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        private bool IsValidFaceBook(LogOnModel model)
        {
            try
            {
                var client = new WebClient();
                var serializer = new JavaScriptSerializer();
                var json = "";
                var email = "";
                var appID = "";

                json = client.DownloadString(
                    string.Format("https://graph.facebook.com/v2.2/me?access_token={0}"
                    , model.Password));
                email = serializer.Deserialize<Dictionary<string, string>>(json)["email"];

                json = client.DownloadString(
                        string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token=368482453361134|rHWxSUsxAi-OF0UQPrRlDVdTl90"
                        , model.Password));
                appID = serializer.Deserialize<Dictionary<string, FBResponse>>(json)["data"].app_id;

                if (email == "quochuy100@gmail.com" && appID == "368482453361134")
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        protected class FBResponse
        {
            public string app_id { get; set; }
            public string application { get; set; }
            public string expires_at { get; set; }
            public bool is_valid { get; set; }
            public List<string> scopes { get; set; }
            public string user_id { get; set; }
        }
    }
}
