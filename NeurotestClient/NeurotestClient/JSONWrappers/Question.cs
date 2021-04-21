namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt single question via POST request
     */
    public class Question
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public string Severity { get; set; }
    }
}
