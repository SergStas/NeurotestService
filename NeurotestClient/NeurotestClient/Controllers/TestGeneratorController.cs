using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestGeneratorController : Controller
    {
        [HttpGet]
        public IEnumerable<JSONWrappers.Question> GetTest(JSONWrappers.TestConfig jsonConfig)
        {
            TestConfig config = TestConfig.FromJSON(jsonConfig);
            return TestGenerator.CreateTest(config);
        }
    }
}
