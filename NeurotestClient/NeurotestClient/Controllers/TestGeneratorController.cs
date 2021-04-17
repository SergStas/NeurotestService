using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestGeneratorController : Controller
    {
        [HttpGet]
        public IEnumerable<Question> GetTest(TestConfig config)
        {
            return TestGenerator.CreateTest(config);
        }
    }
}
