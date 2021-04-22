using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestGeneratorController : Controller
    {
        [HttpPost]
        public IActionResult RequestTest(JSONWrappers.TestConfig jsonConfig)
        {
            if (ModelState.IsValid)
            {
                TestConfig config = TestConfig.FromJSON(jsonConfig);
                return Ok(TestGenerator.CreateTest(config));
            }

            return BadRequest(ModelState);
        }
    }
}
