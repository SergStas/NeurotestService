namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt watch info
     */
    public class WatchInfo
    {
        public JSONWrappers.VideoInfo Video { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
