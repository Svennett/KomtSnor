using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;

namespace KomtSnor.Controllers
{
    public class WebApiController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Json(new { result = "hallo", actionResult = "Goedendag" });
        }
    }
}
