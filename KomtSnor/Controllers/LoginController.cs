using KomtSnor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomtSnor.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Lgoin
        public ActionResult FireBaseLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FireBaseLogin(FormCollection collection)
        {
            string email = collection["email"];
            string uid = collection["uid"];

            FireBaseUser = new FireBaseUser(email, uid);
            ActionResult previousPageActionResult = CheckSessionForCurrentActionDescriptor();
            return previousPageActionResult;
        }


        private ActionResult CheckSessionForCurrentActionDescriptor()
        {
            ActionDescriptor actionDescriptor = null;
            string previousController;
            string previousPage;
            try
            {
                actionDescriptor = (ActionDescriptor)Session[Constants.Authentication.CurrentActionDiscription];
            }
            catch
            {
                // ignored
            }
            if (actionDescriptor != null)
            {
                previousController = actionDescriptor.ControllerDescriptor.ControllerName;
                previousPage = actionDescriptor.ActionName;
            }
            else
            {
                previousController = "Home";
                previousPage = "Index";
            }
            return RedirectToAction(previousPage, previousController);
        }



        public ActionResult GoogleLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GoogleLogin(FormCollection collection)
        {
            var id_token = collection["id_token"];
            var name = collection["name"];
            var email = collection["email"];
            GoogleUser = new GoogleUser(email, id_token, name);
            ActionResult previousPageActionResult = CheckSessionForCurrentActionDescriptor();
            return Json(new {result = "succes", actionResult = previousPageActionResult });
        }



        public ActionResult FacebookLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FacebookLogin(FormCollection collection)
        {
            return View();
        }
    }
}