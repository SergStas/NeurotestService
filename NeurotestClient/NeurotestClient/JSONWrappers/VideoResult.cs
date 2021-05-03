using System.Collections.Generic;

namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt video session result via POST request
     */
    public class VideoResult
    {
        public string SubjectID { get; set; }
        public IEnumerable<JSONWrappers.WatchInfo> WatchSession { get; set; }
    }
}
