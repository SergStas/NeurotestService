using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class VideoInfoTests
    {
        [TestMethod()]
        public void FromJSONTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            JSONWrappers.VideoInfo jsonInfo = new JSONWrappers.VideoInfo
            {
                Url = "Videos/Astonishment/Astonishment1.mp4",
                Type = "Astonishment"
            };
            VideoInfo result = VideoInfo.FromJSON(jsonInfo);

            Assert.AreEqual(result.Path, @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient\DataBase\Videos\Astonishment\Astonishment1.mp4");
            Assert.AreEqual(result.Type, EmotionType.Astonishment);
        }
        [TestMethod()]
        public void ToJSONTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);

            VideoInfo info = new VideoInfo(@"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient\DataBase\Videos\Fear\Fear3.mp4");
            JSONWrappers.VideoInfo result = info.ToJSON();

            Assert.AreEqual(result.Url, @"Videos/Fear/Fear3.mp4");
            Assert.AreEqual(result.Type, "Fear");
        }

        private const string c_WorkingDir = @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient";
    }
}
