using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NeurotestServer.Controllers
{
    [ApiController]
    [Route("api/video")]
    public class VideoResultsController : Controller
    {
        public VideoResultsController(ILogger<VideoResultsController> logger)
        {
            m_Logger = logger;
        }
        [HttpGet]
        public IEnumerable<JSONWrappers.VideoInfo> GetVideos()
        {
            try
            {
                m_Logger.LogDebug("Request for videos");
                return VideosDataBase.GetAllVideos();
            }
            catch (Exception e)
            {
                m_Logger.LogError(e.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public IEnumerable<JSONWrappers.VideoInfo> GetVideoSampleForSubject(ulong id)
        {
            try
            {
                m_Logger.LogInformation($"Request for video sample by subject with ID {id}");

                (EmotionType best, EmotionType worst) = TestDataBase.GetBestAndWorstRecognizedEmotionTypesForSubject(id);
                if ((best == EmotionType.Undefined) || (worst == EmotionType.Undefined))
                {
                    /* There is no subject with this ID */
                    m_Logger.LogDebug($"Attempt to access non-existent subject with ID {id}");
                    return new List<JSONWrappers.VideoInfo>();
                }

                return VideosDataBase.GetVideoSample(best, worst);
            }
            catch (Exception e)
            {
                m_Logger.LogError(e.Message);
                throw;
            }
        }
        [HttpPost]
        public IActionResult SubmitVideoSessionResult(JSONWrappers.VideoResult jsonResult)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /* Fetching parameters from wrapper class */
                    ulong subjectID = Convert.ToUInt64(jsonResult.SubjectID);
                    IEnumerable<JSONWrappers.WatchInfo> jsonWatchSession = jsonResult.WatchSession;

                    List<WatchInfo> watchSession = new List<WatchInfo>(jsonWatchSession.Count());
                    foreach (JSONWrappers.WatchInfo info in jsonWatchSession)
                    {
                        watchSession.Add(WatchInfo.FromJSON(info));
                    }

                    VideosDataBase.SubmitVideoSessionResult(subjectID, watchSession);
                    m_Logger.LogInformation($"Submitted watch session result for subject with ID {subjectID}");
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
