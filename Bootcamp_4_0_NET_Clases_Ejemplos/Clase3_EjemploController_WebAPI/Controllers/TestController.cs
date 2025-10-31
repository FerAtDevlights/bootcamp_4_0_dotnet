using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clase3_EjemploController_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Test-1")]
        public string TestOne()
        {
            return "Test completed";
        }

        [HttpGet("Test-2")]
        public int TestTwo()
        {
            return 5;
        }

        [HttpGet("Test-3")]
        public async Task<IActionResult> TestThree()
        {
            return Ok("Test completed");
        }
    }
}
