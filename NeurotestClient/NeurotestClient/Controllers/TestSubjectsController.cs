using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/subject")]
    public class TestSubjectsController : Controller
    {
        [HttpGet]
        public IEnumerable<JSONWrappers.SubjectInfo> GetSubjectsInfos()
        {
            List<string> subjectsFiles = TestDataBase.GetSubjectsFiles();
            List<JSONWrappers.SubjectInfo> subjects = TestDataBase.GetSubjectsInfosFromFiles(subjectsFiles);
            return subjects;
        }
        [HttpPost]
        public IActionResult PostSubject(JSONWrappers.SubjectInfo jsonInfo)
        {
            if (ModelState.IsValid)
            {
                SubjectInfo info = SubjectInfo.FromJSON(jsonInfo);
                ulong subjectID = TestDataBase.CreateNewSubject(info);
                return Ok(subjectID);
            }

            return BadRequest(ModelState);
        }
    }
}
