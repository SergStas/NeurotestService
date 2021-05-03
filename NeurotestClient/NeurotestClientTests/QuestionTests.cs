using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class QuestionTests
    {
        [TestMethod()]
        public void FromJSONTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Astonishment/Astonishment10.jpg",
                Type = EmotionType.Astonishment.ToString("G"),
                Severity = EmotionSeverity.Weak.ToString("G")
            };
            Question result = Question.FromJSON(jsonQuestion);

            Assert.AreEqual(result.Path, @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient\DataBase\Pictures\Astonishment\Astonishment10.jpg");
            Assert.AreEqual(result.Type, EmotionType.Astonishment);
            Assert.AreEqual(result.Severity, EmotionSeverity.Weak);
        }
        [TestMethod()]
        public void ToJSONTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            Question question = new Question(@"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient\DataBase\Pictures\Fear\Fear30.jpg");
            JSONWrappers.Question result = question.ToJSON();

            Assert.AreEqual(result.Url, "Pictures/Fear/Fear30.jpg");
            Assert.AreEqual(result.Type, "Fear");
            Assert.AreEqual(result.Severity, "Strong");
        }

        private const string c_WorkingDir = @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient";
    }
}
