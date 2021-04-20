using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/subject")]
    public class TestSubjectsController : Controller
    {
        [HttpGet]
        public IEnumerable<SubjectInfo> GetSubjectsInfos()
        {
            List<string> subjectsFiles = TestDataBase.GetSubjectsFiles();
            List<SubjectInfo> subjects = TestDataBase.GetSubjectsInfosFromFiles(subjectsFiles);
            return subjects;
        }
        [HttpPost]
        public IActionResult PostSubject(SubjectInfo info)
        {
            if (ModelState.IsValid)
            {
                ulong subjectID = TestDataBase.CreateNewSubject(info);
                return Ok(subjectID);
            }

            return BadRequest(ModelState);
        }
    }
}
