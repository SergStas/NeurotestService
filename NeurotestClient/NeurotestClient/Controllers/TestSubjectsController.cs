using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/subject")]
    public class TestSubjectsController : Controller
    {
        public TestSubjectsController(ILogger<TestSubjectsController> logger)
        {
            m_Logger = logger;
        }
        [HttpGet]
        public IEnumerable<JSONWrappers.SubjectInfo> GetSubjectsInfos()
        {
            try
            {
                m_Logger.LogDebug("Request for subjects infos");
                return TestDataBase.GetSubjectsInfos();
            }
            catch (Exception e)
            {
                m_Logger.LogError(e.Message);
                throw;
            }
        }
        [HttpPost]
        public IActionResult PostSubject(JSONWrappers.SubjectInfo jsonInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SubjectInfo info = SubjectInfo.FromJSON(jsonInfo);
                    ulong subjectID = TestDataBase.CreateNewSubject(info);

                    m_Logger.LogInformation($"New subject created with ID {subjectID}");
                    return Ok(subjectID);
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
