using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NeurotestServer.Tests
{
    [TestClass()]
    public class SubjectInfoTests
    {
        [TestCategory("FromJSONTest")]
        [TestMethod()]
        public void FromJSONFirstTest()
        {
            DateTime birthDate = DateTime.Now;
            JSONWrappers.SubjectInfo jsonInfo = new JSONWrappers.SubjectInfo
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                Sex = "Male",
                BirthDate = birthDate.ToString("dd.MM.yyyy"),
                Address = "123, hghghg",
                Job = "Programmer",
                Diseases = "D1, D2",
                Phone = "88888888888"
            };
            SubjectInfo result = SubjectInfo.FromJSON(jsonInfo);

            Assert.AreEqual(result.FirstName, jsonInfo.FirstName);
            Assert.AreEqual(result.LastName, jsonInfo.LastName);
            Assert.AreEqual(result.Patronymic, jsonInfo.Patronymic);
            Assert.AreEqual(result.Sex, Sex.Male);
            Assert.AreEqual(result.Address, jsonInfo.Address);
            Assert.AreEqual(result.Job, jsonInfo.Job);
            Assert.AreEqual(result.Diseases, jsonInfo.Diseases);
            Assert.AreEqual(result.Phone, jsonInfo.Phone);
        }
        [TestCategory("FromJSONTest")]
        [TestMethod()]
        public void FromJSONSecondTest()
        {
            DateTime birthDate = DateTime.Now;
            JSONWrappers.SubjectInfo jsonInfo = new JSONWrappers.SubjectInfo
            {
                FirstName = "Мария",
                LastName = "Петрова",
                Patronymic = "",
                Sex = "Female",
                BirthDate = birthDate.ToString("dd.MM.yyyy"),
                Address = "123, hghghg",
                Job = "Programmer",
                Diseases = "D1, D2",
                Phone = "11111111111"
            };
            SubjectInfo result = SubjectInfo.FromJSON(jsonInfo);

            Assert.AreEqual(result.FirstName, jsonInfo.FirstName);
            Assert.AreEqual(result.LastName, jsonInfo.LastName);
            Assert.AreEqual(result.Patronymic, jsonInfo.Patronymic);
            Assert.AreEqual(result.Sex, Sex.Female);
        }
        [TestMethod()]
        public void ToJSONTest()
        {
            DateTime birthDate = DateTime.Now;
            JSONWrappers.SubjectInfo jsonInfo = new JSONWrappers.SubjectInfo
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                Sex = "Male",
                BirthDate = birthDate.ToString("dd.MM.yyyy"),
                Address = "123, hghghg",
                Job = "Programmer",
                Diseases = "D1, D2",
                Phone = "88888888888"
            };
            SubjectInfo info = SubjectInfo.FromJSON(jsonInfo);
            JSONWrappers.SubjectInfo result = info.ToJSON();

            Assert.AreEqual(result.FirstName, jsonInfo.FirstName);
            Assert.AreEqual(result.LastName, jsonInfo.LastName);
            Assert.AreEqual(result.Patronymic, jsonInfo.Patronymic);
            Assert.AreEqual(result.Sex, jsonInfo.Sex);
            Assert.AreEqual(result.Address, jsonInfo.Address);
            Assert.AreEqual(result.Job, jsonInfo.Job);
            Assert.AreEqual(result.Diseases, jsonInfo.Diseases);
            Assert.AreEqual(result.Phone, jsonInfo.Phone);
        }
        [TestCategory("FromCSVTest")]
        [TestMethod()]
        public void FromCSVFirstTest()
        {
            DateTime birthDate = DateTime.Now;
            SubjectInfo result = SubjectInfo.FromCSV($"0;Иванов Иван Иванович;Male;{birthDate.ToString("dd.MM.yyyy")};123, hghghg;Programmer;D1, D2;99999999999");

            Assert.AreEqual(result.FirstName, "Иван");
            Assert.AreEqual(result.LastName, "Иванов");
            Assert.AreEqual(result.Patronymic, "Иванович");
            Assert.AreEqual(result.Sex, Sex.Male);
            Assert.AreEqual(result.Address, "123, hghghg");
            Assert.AreEqual(result.Job, "Programmer");
            Assert.AreEqual(result.Diseases, "D1, D2");
            Assert.AreEqual(result.Phone, "99999999999");
        }
        [TestCategory("FromCSVTest")]
        [TestMethod()]
        public void FromCSVSecondTest()
        {
            DateTime birthDate = DateTime.Now;
            SubjectInfo result = SubjectInfo.FromCSV($"1;Петрова Мария;Female;{birthDate.ToString("dd.MM.yyyy")};123, hghghg;Programmer;D1, D2;99999999999");

            Assert.AreEqual(result.FirstName, "Мария");
            Assert.AreEqual(result.LastName, "Петрова");
            Assert.AreEqual(result.Patronymic, "");
            Assert.AreEqual(result.Sex, Sex.Female);
        }
        [TestCategory("FromCSVTest")]
        [TestMethod()]
        public void FromCSVThiirdTest()
        {
            DateTime birthDate = DateTime.Now;
            SubjectInfo result = SubjectInfo.FromCSV($"1;Петрова Мария;Wrong;{birthDate.ToString("dd.MM.yyyy")};123, hghghg;Programmer;D1, D2;99999999999");

            Assert.AreEqual(result.Sex, Sex.Undefined);
        }
    }
}
