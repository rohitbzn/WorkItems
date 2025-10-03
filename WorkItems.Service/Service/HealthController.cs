using System;
using System.Web.Http;

namespace WorkItems.Service.Service
{
    [RoutePrefix("api/v1/health")]
    public class HealthController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow
            });
        }
    }
}