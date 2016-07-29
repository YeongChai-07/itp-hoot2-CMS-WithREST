/*using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;*/
using System.Web.Mvc;
using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;

namespace HootHoot_CMS.Controllers.View_Controller
{
    public class AccountsController : Controller
    {
        private HootHootDbContext db = new HootHootDbContext();
        private AccountsDataGateway accountsGateway = new AccountsDataGateway();

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Accounts loginAccount, string returnUrl)
        {
            bool modelState_FirstPass = ModelState.IsValid;
            Accounts userInfo = null;

            if (modelState_FirstPass)
            {
                string userName_ToCheck = loginAccount.user_name.ToLower().Replace(" ",string.Empty);
                userInfo = accountsGateway.findByUserName(userName_ToCheck);

                if (userInfo == null)
                {
                    ModelState.AddModelError(Constants.ACCOUNTS_MODEL_KEYS[0], Constants.ENTERED_USERNAME_INVALID);
                }
                else if (!userInfo.password.Equals(loginAccount.password))
                {
                    ModelState.AddModelError(Constants.ACCOUNTS_MODEL_KEYS[1], Constants.ENTERED_PASSWORD_INVALID);
                }

            }

            if(ModelState.IsValid)
            {
                //Save the information of the successful logged on user to the UesrLogonViewModel
                //and then save it as a session
                Session["logon_user"] = new UserLogonViewModel()
                {
                    full_name = userInfo.full_name,
                    user_name = userInfo.user_name,
                    user_role = userInfo.user_role
                };
                return RedirectToLocal(returnUrl);
            }

            return View(loginAccount);
            
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            UserLogonViewModel logonUser = Session["logon_user"] as UserLogonViewModel;

            //Check whether the UserLogonViewModel received from the Session is null 
            if(logonUser != null)
            {
                //Since it is not null, destroy it from the session by assigning the session with null
                Session["logon_user"] = null;
            }

            return RedirectToAction("Login", "Accounts");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Questions");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
