using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class AnswerTests
    {
        [TestMethod()]
        public void FromJSONTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Happiness/Happiness10.jpg",
                Type = EmotionType.Happiness.ToString("G"),
                Severity = EmotionSeverity.Weak.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Happiness.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer result = Answer.FromJSON(jsonAnswer);

            /* Don't testing Question convertion, because it belongs to another test */
            Assert.AreEqual(result.UserInput, EmotionType.Happiness);
            Assert.AreEqual(result.ElapsedTime, 5.5f);
        }
        [TestMethod()]
        public void ToCSVStringTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Happiness/Happiness10.jpg",
                Type = EmotionType.Happiness.ToString("G"),
                Severity = EmotionSeverity.Weak.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Happiness.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);
            string result = answer.ToCSVString();
            string expected = @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient\DataBase\Pictures\Happiness\Happiness10.jpg;" +
                "Happiness;Happiness;5,50\n";

            Assert.AreEqual(result, expected);
        }
        [TestMethod()]
        public void GetRightTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Happiness/Happiness30.jpg",
                Type = EmotionType.Happiness.ToString("G"),
                Severity = EmotionSeverity.Strong.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Astonishment.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.AreEqual(answer.GetRight(), EmotionType.Happiness);
        }
        [TestMethod()]
        public void GetSeverityTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Happiness/Happiness10.jpg",
                Type = EmotionType.Happiness.ToString("G"),
                Severity = EmotionSeverity.Weak.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Astonishment.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.AreEqual(answer.GetSeverity(), EmotionSeverity.Weak);
        }
        [TestCategory("IsRightTest")]
        [TestMethod()]
        public void IsRightFirstTestTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Happiness/Happiness20.jpg",
                Type = EmotionType.Happiness.ToString("G"),
                Severity = EmotionSeverity.Average.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Astonishment.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.IsFalse(answer.IsRight());
        }
        [TestCategory("IsRightTest")]
        [TestMethod()]
        public void IsRightSecondTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Fear/Fear20.jpg",
                Type = EmotionType.Fear.ToString("G"),
                Severity = EmotionSeverity.Average.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Fear.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.IsTrue(answer.IsRight());
        }
        [TestCategory("IsSimilarTest")]
        [TestMethod()]
        public void IsSimilarFirstTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Disgust/Disgust20.jpg",
                Type = EmotionType.Disgust.ToString("G"),
                Severity = EmotionSeverity.Average.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Anger.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.IsTrue(answer.IsSimilar());
        }
        [TestCategory("IsSimilarTest")]
        [TestMethod()]
        public void IsSimilarSecondTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Sadness/Sadness20.jpg",
                Type = EmotionType.Sadness.ToString("G"),
                Severity = EmotionSeverity.Average.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Fear.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.IsTrue(answer.IsSimilar());
        }
        [TestCategory("IsSimilarTest")]
        [TestMethod()]
        public void IsSimilarThirdTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Happiness/Happiness20.jpg",
                Type = EmotionType.Happiness.ToString("G"),
                Severity = EmotionSeverity.Average.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Astonishment.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.IsTrue(answer.IsSimilar());
        }
        [TestCategory("IsSimilarTest")]
        [TestMethod()]
        public void IsSimilarFourthTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Disgust/Disgust20.jpg",
                Type = EmotionType.Disgust.ToString("G"),
                Severity = EmotionSeverity.Average.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Disgust.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.IsFalse(answer.IsSimilar());
        }
        [TestCategory("IsSimilarTest")]
        [TestMethod()]
        public void IsSimilarFifthTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.Question jsonQuestion = new JSONWrappers.Question
            {
                Url = "Pictures/Astonishment/Astonishment20.jpg",
                Type = EmotionType.Astonishment.ToString("G"),
                Severity = EmotionSeverity.Average.ToString("G")
            };
            JSONWrappers.Answer jsonAnswer = new JSONWrappers.Answer
            {
                Question = jsonQuestion,
                UserInput = EmotionType.Sadness.ToString("G"),
                ElapsedTime = "5.5"
            };
            Answer answer = Answer.FromJSON(jsonAnswer);

            Assert.IsFalse(answer.IsSimilar());
        }

        private const string c_WorkingDir = @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient";
    }
}
