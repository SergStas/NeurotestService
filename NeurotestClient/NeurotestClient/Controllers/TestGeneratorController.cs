using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestGeneratorController : Controller
    {
        public TestGeneratorController(ILogger<TestGeneratorController> logger)
        {
            m_Logger = logger;
        }
        [HttpPost]
        public IActionResult RequestTest(JSONWrappers.TestConfig jsonConfig)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TestConfig config = TestConfig.FromJSON(jsonConfig);
                    List<JSONWrappers.Question> questions = TestGenerator.CreateTest(config);

                    m_Logger.LogDebug("Created new test");
                    return Ok(questions);
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                m_Logger.LogError(e.Message);
                throw;
            }
        }

        private readonly ILogger m_Logger;
    }
}
