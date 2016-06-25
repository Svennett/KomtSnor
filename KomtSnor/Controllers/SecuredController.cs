using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using KomtSnor.Models;
using KomtSnor.Models.CustomAnnotations;

namespace KomtSnor.Controllers
{
    public class SecuredController : Controller
    {

        public ActionResult SecuredHome()
        {
            IEnumerable<MethodInfo> allActionResultMethods_WithNoParameters = getAllActionResultMethods_WithNoParameters();

            SecuredViewModel securedViewModel = new SecuredViewModel();
            securedViewModel.MethodsInfos = allActionResultMethods_WithNoParameters;

            return View(securedViewModel);
        }

        private IEnumerable<MethodInfo> getAllActionResultMethods_WithNoParameters()
        {
            //get all Methods that open a securedView

            MethodInfo[] allMethods = this.GetType().GetMethods();
            IEnumerable<MethodInfo> allActionResultMethods = allMethods.Where(method => method.ReturnParameter?.ParameterType == typeof(ActionResult));
            IEnumerable<MethodInfo> allActionResultMethods_WithNoParameters = allActionResultMethods.Where(method => method.GetParameters().Length == 0);
            return allActionResultMethods_WithNoParameters;
        }





        [FireBaseAuthentication]
        [GoogleAuthentication]
        [SqlServerAuthentication]
        public ActionResult SecuredPage1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SecuredPage1(FormCollection collection)
        {
            return View();
        }
    }
}