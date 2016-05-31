using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomtSnor.Models.CustomAnnotations;

namespace KomtSnor.Controllers
{
    public class SecuredController : Controller
    {
        // GET: Secured
        [FireBaseAuthentication]
        public ActionResult SecuredPage1()
        {
            return View();
        }
    }
}