using System.Collections.Generic;

namespace NeurotestServer
{
    /*
     * Wrapper struct to transmitt subject ID and answers via one POST request
     */
    public struct ResultInfo
    {
        public ResultInfo(ulong subjectID, IEnumerable<Answer> answers)
        {
            SubjectID = subjectID;
            Answers = answers;
        }
        public readonly ulong SubjectID { get; }
        public readonly IEnumerable<Answer> Answers { get; }
    }
}
