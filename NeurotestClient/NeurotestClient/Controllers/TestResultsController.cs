using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/result")]
    public class TestResultsController : Controller
    {
        [HttpPost]
        public IActionResult PostResult(JSONWrappers.TestResult jsonResult)
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

                return Ok(subjectID);
            }

            return BadRequest(ModelState);
        }
    }
}
