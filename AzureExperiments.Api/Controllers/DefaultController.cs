using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureExperiments.Controllers
{
    [RoutePrefix("api/default")]
    public class DefaultController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("You have reached the default controller successfully");
        }
    }
}
