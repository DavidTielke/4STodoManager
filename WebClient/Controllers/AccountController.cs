using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebClient.ViewModel;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Logon()
        {
            return View(new LogonViewModel());
        }

        [HttpPost]
        public ActionResult Logon(LogonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValid = Membership.ValidateUser(model.Username, model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    return RedirectToAction("Index", "Todo");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Logon", "Account");
        }
    }
}