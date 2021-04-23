namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt single question via POST request
     */
    public class Question
    {
        public string Url { get; set; }  // Url to the picture
        public string Type { get; set; }
        public string Severity { get; set; }
    }
}
