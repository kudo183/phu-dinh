using System.Web.Helpers;
using PhuDinhOData.Filters;
using PhuDinhOData.Models;
using WebMatrix.WebData;

namespace PhuDinhOData.Controllers
{
    [System.Web.Mvc.Authorize]
    [InitializeSimpleMembership]
    public class AccountController : System.Web.Mvc.Controller
    {

        [System.Web.Mvc.AllowAnonymous]
        public string AntiForgeryToken()
        {
            return AntiForgery.GetHtml().ToString();
        }

        //
        // POST: /Account/Login

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public bool Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return true;
            }

            return false;
        }

        //
        // POST: /Account/LogOff

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public void LogOff()
        {
            WebSecurity.Logout();
        }
    }
}
