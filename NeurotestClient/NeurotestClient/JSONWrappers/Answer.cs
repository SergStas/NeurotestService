namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt single answer via POST request
     */
    public class Answer
    {
        public JSONWrappers.Question Question { get; set; }
        public string UserInput { get; set; }
        public string ElapsedTime { get; set; }
    }
}
