using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestCategory("GetFirstDigitTest")]
        [TestMethod()]
        public void GetFirstDigitFirstTest()
        {
            char result = Utils.GetFirstDigit("hfakgbfakg");
            Assert.AreEqual(result, ' ');
        }
        [TestCategory("GetFirstDigitTest")]
        [TestMethod()]
        public void GetFirstDigitSecondTest()
        {
            char result = Utils.GetFirstDigit("hf90akg7679bfakg97");
            Assert.AreEqual(result, '9');
        }
        [TestMethod()]
        public void PermuteListTest()
        {
            List<int> expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> result = Utils.PermuteList(expected);

            Assert.IsFalse(expected.SequenceEqual(result));
        }
        [TestCategory("EmotionTypeStringToEnumTest")]
        [TestMethod()]
        public void EmotionTypeStringToEnumFirstTest()
        {
            EmotionType result = Utils.EmotionTypeStringToEnum("Anger");
            Assert.AreEqual(result, EmotionType.Anger);
        }
        [TestCategory("EmotionTypeStringToEnumTest")]
        [TestMethod()]
        public void EmotionTypeStringToEnumSecondTest()
        {
            EmotionType result = Utils.EmotionTypeStringToEnum("Astonishment");
            Assert.AreEqual(result, EmotionType.Astonishment);
        }
        [TestCategory("EmotionTypeStringToEnumTest")]
        [TestMethod()]
        public void EmotionTypeStringToEnumThirdTest()
        {
            EmotionType result = Utils.EmotionTypeStringToEnum("Disgust");
            Assert.AreEqual(result, EmotionType.Disgust);
        }
        [TestCategory("EmotionTypeStringToEnumTest")]
        [TestMethod()]
        public void EmotionTypeStringToEnumFourthTest()
        {
            EmotionType result = Utils.EmotionTypeStringToEnum("Fear");
            Assert.AreEqual(result, EmotionType.Fear);
        }
        [TestCategory("EmotionTypeStringToEnumTest")]
        [TestMethod()]
        public void EmotionTypeStringToEnumFifthTest()
        {
            EmotionType result = Utils.EmotionTypeStringToEnum("Happiness");
            Assert.AreEqual(result, EmotionType.Happiness);
        }
        [TestCategory("EmotionTypeStringToEnumTest")]
        [TestMethod()]
        public void EmotionTypeStringToEnumSixthTest()
        {
            EmotionType result = Utils.EmotionTypeStringToEnum("Sadness");
            Assert.AreEqual(result, EmotionType.Sadness);
        }
        [TestCategory("EmotionTypeStringToEnumTest")]
        [TestMethod()]
        public void EmotionTypeStringToEnumSeventhTest()
        {
            EmotionType result = Utils.EmotionTypeStringToEnum("kdbhagkfba");
            Assert.AreEqual(result, EmotionType.Undefined);
        }
        [TestCategory("SexStringToEnumTest")]
        [TestMethod()]
        public void SexStringToEnumFirstTest()
        {
            Sex result = Utils.SexStringToEnum("Male");
            Assert.AreEqual(result, Sex.Male);
        }
        [TestCategory("SexStringToEnumTest")]
        [TestMethod()]
        public void SexStringToEnumSecondTest()
        {
            Sex result = Utils.SexStringToEnum("Female");
            Assert.AreEqual(result, Sex.Female);
        }
        [TestCategory("SexStringToEnumTest")]
        [TestMethod()]
        public void SexStringToEnumThirdTest()
        {
            Sex result = Utils.SexStringToEnum("ahga");
            Assert.AreEqual(result, Sex.Undefined);
        }
    }
}
