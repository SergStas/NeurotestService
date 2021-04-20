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
        public IActionResult PostResult(ResultInfo resultInfo)
        {
            if (ModelState.IsValid)
            {
                /* Fetching parameters from wrapper struct */
                ulong subjectID = resultInfo.SubjectID;
                IEnumerable<Answer> answers = resultInfo.Answers;

                TestResult result = new TestResult(subjectID, answers.ToList());
                TestDataBase.SubmitTestResult(result);

                return Ok(subjectID);
            }

            return BadRequest(ModelState);
        }
    }
}
