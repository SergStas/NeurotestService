using System;

namespace NeurotestServer
{
    public class TestSubject
    {
        public TestSubject(ulong id, SubjectInfo info)
        {
            ID = id;
            Info = info;
        }
        public void ToCSV()
        {
            throw new NotImplementedException();
        }
        public ulong ID { get; }
        public SubjectInfo Info { get; }
    }
}
