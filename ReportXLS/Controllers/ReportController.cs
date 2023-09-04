using Microsoft.AspNetCore.Mvc;

namespace ReportXLS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Report");
        }
    }
}
