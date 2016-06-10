using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KomtSnor.Domain;

namespace KomtSnor.Models.CustomAnnotations
{
    public class GoogleAuthentication : AuthorizeAttribute
    {
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = httpContext.Session;
            try
            {
                GoogleUser googleUser = (GoogleUser)session[Constants.Authentication.Google];
                if (googleUser != null)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            session[Constants.Authentication.CurrentActionDiscription] = filterContext.ActionDescriptor;

            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Login",
                                action = "GoogleLogin"
                            })
                        );
        }


    }
}