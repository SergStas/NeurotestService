using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class TestSubjectTests
    {
        [TestMethod()]
        public void ToCSVStringTest()
        {
            DateTime birthDate = DateTime.Now;
            string csvString = $"0;Иванов Иван Иванович;Male;{birthDate.ToString("dd.MM.yyyy")};123, hghghg;Programmer;D1, D2;99999999999";
            
            SubjectInfo info = SubjectInfo.FromCSV(csvString);
            TestSubject subject = new TestSubject(0, info);
            string result = subject.ToCSVString();

            Assert.AreEqual(result, csvString + "\n");
        }
    }
}
