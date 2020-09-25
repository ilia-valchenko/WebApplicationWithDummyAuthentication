using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationWithDummyAuthentication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}