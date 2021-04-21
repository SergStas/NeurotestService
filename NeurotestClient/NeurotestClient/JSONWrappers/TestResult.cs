using System.Collections.Generic;

namespace NeurotestServer.JSONWrappers
{
    /*
     * Wrapper class to transmitt subject ID and answers via one POST request
     */
    public class TestResult
    {
        public string SubjectID { get; set; }
        public IEnumerable<JSONWrappers.Answer> Answers { get; set; }
    }
}
