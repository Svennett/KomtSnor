using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KomtSnor.Domain;
using KomtSnor.Domain.Users;

namespace KomtSnor.Models.CustomAnnotations
{
    public class SqlServer_EntityAuthentication : AuthorizeAttribute
    {
        // Custom property
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = httpContext.Session;
            var response = httpContext.Response;
            try
            {
                SQLServer_EntityUser sqlServerUser = (SQLServer_EntityUser)session[Constants.Authentication.SqlServer_EntityAuthentication];
                if (sqlServerUser != null)
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
            string requestController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string requestPage = filterContext.ActionDescriptor.ActionName;

            var session = filterContext.HttpContext.Session;
            session[Constants.Authentication.CurrentActionDiscription] = filterContext.ActionDescriptor;

            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Login",
                                action = "SQLServer_EntityLogin"
                            })
                        );
        }
    }
}