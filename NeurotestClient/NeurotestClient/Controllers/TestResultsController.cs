using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/result")]
    public class TestResultsController : Controller
    {
        public TestResultsController(ILogger<TestResultsController> logger)
        {
            m_Logger = logger;
        }
        [HttpPost]
        public IActionResult PostResult(JSONWrappers.TestResult jsonResult)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /* Fetching parameters from wrapper class */
                    ulong subjectID = Convert.ToUInt64(jsonResult.SubjectID);
                    IEnumerable<JSONWrappers.Answer> jsonAnswers = jsonResult.Answers;

                    List<Answer> answers = new List<Answer>(jsonAnswers.Count());
                    foreach (JSONWrappers.Answer jsonAnswer in jsonAnswers)
                    {
                        answers.Add(Answer.FromJSON(jsonAnswer));
                    }

                    TestResult result = new TestResult(subjectID, answers);
                    TestDataBase.SubmitTestResult(result);

                    m_Logger.LogInformation($"Submitted test result for subject with ID {subjectID}");

                    /* Sanding calculated statistics in CSV string format as a response */
                    return Ok(result.ToCSVString());
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
