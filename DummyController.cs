using Microsoft.AspNetCore.Mvc;

namespace SmCty.Framework.Common.APIGateway.Web
{
    [ApiController]
    [Route("api/[controller]")]
    public class DummyController : Controller
    {

        [HttpGet]
        public string DummyMethod()
        {
            return "Gateway is open";
        }
    }
}