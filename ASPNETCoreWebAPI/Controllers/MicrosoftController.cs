
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //With the [EnableCors] attribute.
    //[EnableCors(PolicyName = "AllowOnlyMicrosoft")]
    [Authorize(AuthenticationSchemes = "LoginForMicrosoftUsers", Roles = "Superadmin,Admin")]
    public class MicrosoftController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("THis is microsoft");
        }
    }
}
