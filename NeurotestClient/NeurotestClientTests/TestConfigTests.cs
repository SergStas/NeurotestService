using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class TestConfigTests
    {
        [TestMethod()]
        public void FromJSONTest()
        {
            JSONWrappers.TestConfig jsonConfig = new JSONWrappers.TestConfig
            {
                QuestionDuration = "3",
                AngerCount = "5",
                AstonishmentCount = "5",
                DisgustCount = "5",
                FearCount = "5",
                HappinessCount = "5",
                SadnessCount = "5"
            };
            TestConfig result = TestConfig.FromJSON(jsonConfig);

            Assert.AreEqual(result.QuestionDuration, 3);
            Assert.AreEqual(result.AngerCount, 5);
            Assert.AreEqual(result.AstonishmentCount, 5);
            Assert.AreEqual(result.DisgustCount, 5);
            Assert.AreEqual(result.FearCount, 5);
            Assert.AreEqual(result.HappinessCount, 5);
            Assert.AreEqual(result.SadnessCount, 5);
            Assert.AreEqual(result.TotalCount, 90);
        }
    }
}
