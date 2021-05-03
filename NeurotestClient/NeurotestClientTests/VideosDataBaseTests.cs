using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class VideosDataBaseTests
    {
        [TestMethod()]
        public void GetAllVideosTest()
        {
            List<JSONWrappers.VideoInfo> videos = VideosDataBase.GetAllVideos();
            Assert.AreEqual(videos.Count, 62);
        }
        [TestMethod()]
        public void GetVideoSampleTest()
        {
            List<JSONWrappers.VideoInfo> sample = VideosDataBase.GetVideoSample(EmotionType.Astonishment, EmotionType.Fear);

            Assert.AreEqual(sample[0].Type, "Astonishment");
            Assert.AreEqual(sample[1].Type, "Fear");
        }
        [TestMethod()]
        public void ConvertPathToUrlTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);
            string path = Path.Combine(c_WorkingDir, @"DataBase\Videos\Happiness\Happiness3.mp4");
            string result = VideosDataBase.ConvertPathToUrl(path);

            Assert.AreEqual(result, "Videos/Happiness/Happiness3.mp4");
        }
        [TestMethod()]
        public void ConvertUrlToPathTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);
            string expected = Path.Combine(c_WorkingDir, @"DataBase\Videos\Sadness\Sadness6.mp4");
            string url = "Videos/Sadness/Sadness6.mp4";
            string result = VideosDataBase.ConvertUrlToPath(url);

            Assert.AreEqual(result, expected);
        }

        private const string c_WorkingDir = @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient";
    }
}
