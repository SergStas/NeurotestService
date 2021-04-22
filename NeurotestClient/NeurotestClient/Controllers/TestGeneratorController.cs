using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestGeneratorController : Controller
    {
        [HttpPost]
        public IActionResult GetTest(JSONWrappers.TestConfig jsonConfig)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            TestConfig config = TestConfig.FromJSON(jsonConfig);
            return Ok(TestGenerator.CreateTest(config));
        }
    }
}
