using System;
using System.Collections.Generic;

namespace NeurotestServer
{
    public class SubjectInfo
    {
        public SubjectInfo(string name, Sex sex, DateTime birthDate, string address, string job,
            List<string> diseses, string phone)
        {
            (string lastName, string firstName, string patronymic) = SplitName(name);

            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Sex = sex;
            BirthDate = birthDate;
            Address = address;
            Job = job;
            Diseses = diseses;
            Phone = phone;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Patronymic { get; }
        public Sex Sex { get; }
        public DateTime BirthDate { get; }
        public string Address { get; }
        public string Job { get; }
        public List<string> Diseses { get; }
        public string Phone { get; }  // The phone number of the test subject in the format ***********
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
