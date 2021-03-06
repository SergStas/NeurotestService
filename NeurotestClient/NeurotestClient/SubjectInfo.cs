using System;

namespace NeurotestServer
{
    public class SubjectInfo
    {
        public static SubjectInfo FromJSON(JSONWrappers.SubjectInfo jsonInfo)
        {
            string firstName = jsonInfo.FirstName;
            string lastName = jsonInfo.LastName;
            string patronymic = jsonInfo.Patronymic;
            string name = string.Join(" ", new string[]{ lastName, firstName, patronymic});

            string address = jsonInfo.Address;
            string job = jsonInfo.Job;
            string diseases = jsonInfo.Diseases;
            string phone = jsonInfo.Phone;

            Sex sex = Utils.SexStringToEnum(jsonInfo.Sex);
            DateTime birthDate = Convert.ToDateTime(jsonInfo.BirthDate);

            return new SubjectInfo(name, sex, birthDate, address, job, diseases, phone);
        }
        public JSONWrappers.SubjectInfo ToJSON()
        {
            JSONWrappers.SubjectInfo jsonInfo = new JSONWrappers.SubjectInfo
            {
                FirstName = FirstName,
                LastName = LastName,
                Patronymic = Patronymic,
                Address = Address,
                Job = Job,
                Diseases = Diseases,
                Phone = Phone,
                Sex = Sex.ToString("G"),
                BirthDate = BirthDate.ToString("dd.MM.yyyy")
            };

            return jsonInfo;
        }
        public static SubjectInfo FromCSV(string csvString)
        {   
            /* 
             * Getting data from csv string.
             * First element in the row is the ID, so we need to skip it.
             */
            string[] subjectFiledsStrings = csvString.Split(';');

            string name = subjectFiledsStrings[1];
            Sex sex = Utils.SexStringToEnum(subjectFiledsStrings[2]);
            DateTime birthDate = Convert.ToDateTime(subjectFiledsStrings[3]);
            string address = subjectFiledsStrings[4];
            string job = subjectFiledsStrings[5];
            string diseases = subjectFiledsStrings[6];
            string phone = subjectFiledsStrings[7];

            return new SubjectInfo(name, sex, birthDate, address, job, diseases, phone);
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Patronymic { get; }
        public Sex Sex { get; }
        public DateTime BirthDate { get; }
        public string Address { get; }
        public string Job { get; }
        public string Diseases { get; }
        public string Phone { get; }  // The phone number of the test subject in the format ***********
        protected SubjectInfo(string name, Sex sex, DateTime birthDate, string address, string job,
            string diseases, string phone)
        {
            (string lastName, string firstName, string patronymic) = SplitName(name);

            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Sex = sex;
            BirthDate = birthDate;
            Address = address;
            Job = job;
            Diseases = diseases;
            Phone = phone;
        }
        /*
         * This method splits whole name into last, first name and patronymic
         */
        private static (string, string, string) SplitName(string name)
        {
            string[] splitted = name.Split(' ');
            if (splitted.Length == 2)
                return (splitted[0], splitted[1], "");

            return (splitted[0], splitted[1], splitted[2]);
        }
    }
}
