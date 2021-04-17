using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class TestDataBaseController : Controller
    {
        [HttpGet]
        public IEnumerable<string> GetSubjectsIDs()
        {
            return TestDataBase.GetSubjectsIDs();
        }
        [HttpPost]
        public IActionResult PostSubject(SubjectInfo info)
        {
            if (ModelState.IsValid)
            {
                TestDataBase.CreateNewSubject(info);
                return Ok(info);
            }

            return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult PostResult(ulong subjectID, IEnumerable<Answer> answers)
        {
            if (ModelState.IsValid)
            {
                TestResult result = new TestResult(subjectID, answers.ToList());
                TestDataBase.SubmitTestResult(result);

                return Ok(subjectID);
            }

            return BadRequest(ModelState);
        }
    }
}
