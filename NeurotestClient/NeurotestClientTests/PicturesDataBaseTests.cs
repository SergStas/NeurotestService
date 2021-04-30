using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class PicturesDataBaseTests
    {
        [TestMethod()]
        public void ConvertPathToUrlTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);
            string path = Path.Combine(c_WorkingDir, @"DataBase\Pictures\Happiness\Happiness10.jpg");
            string result = PicturesDataBase.ConvertPathToUrl(path);

            Assert.AreEqual(result, "Pictures/Happiness/Happiness10.jpg");
        }
        [TestMethod()]
        public void ConvertUrlToPathTest()
        {
            Directory.SetCurrentDirectory(c_WorkingDir);
            string expected = Path.Combine(c_WorkingDir, @"DataBase\Pictures\Sadness\Sadness15.jpg");
            string url = "Pictures/Sadness/Sadness15.jpg";
            string result = PicturesDataBase.ConvertUrlToPath(url);

            Assert.AreEqual(result, expected);
        }

        private const string c_WorkingDir = @"C:\Users\boris\Desktop\Workspace\Neurotest\NeurotestService\NeurotestClient\NeurotestClient";
    }
}
