using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace NeurotestServer
{
    /*
     * Wrapper class for the file system.
     * Organizes access to the test subjects and results.
     * Also provides API to serialize and store new data.
     */
    public static class TestDataBase
    {
        /* Saves test result in the CSV format */
        public static void SubmitTestResult(TestResult result)
        {
            /* Writing all answers to CSV file */
            string filename = Convert.ToString(result.SubjectID) + ".csv";
            string path = Path.Combine(m_ResultsDir, filename);
            StringBuilder builder = new StringBuilder(c_AnswerCSVHeader);

            foreach (Answer answer in result.Answers)
            {
                builder.Append(answer.ToCSVString());
            }
            File.WriteAllText(path, builder.ToString());

            /* Writing statistics to CSV file */
            string statisticsFilePath = Path.Combine(m_ResultsDir, c_StatisticsFile);
            File.AppendAllText(statisticsFilePath, result.ToCSVString());
        }
        /* 
         * Creates TestSubject instance and stors it in the csv format.
         * Returns an ID of created subject.
         * */
        public static ulong CreateNewSubject(SubjectInfo info)
        {
            TestSubject subject = new TestSubject(m_NextSubjectID, info);
            UpdateNextSubjectID();

            string filename = Convert.ToString(subject.ID) + ".csv";
            string path = Path.Combine(m_TestSubjectsDir, filename);
            File.WriteAllText(path, subject.ToCSVString());

            return subject.ID;
        }
        /* Returns a list of all subject files in the database */
        public static List<string> GetSubjectsFiles()
        {
            List<string> files = new List<string>(Convert.ToInt32(m_NextSubjectID - 1));

            foreach (string subjectFilename in Directory.GetFiles(m_TestSubjectsDir))
            {
                if (Path.GetExtension(subjectFilename) == ".id") continue;
                files.Add(subjectFilename);
            }

            return files;
        }
        /* Read subjects data from given files */
        public static List<SubjectInfo> GetSubjectsInfosFromFiles(List<string> filenames)
        {
            List<SubjectInfo> subjects = new List<SubjectInfo>(filenames.Count);

            foreach (string filename in filenames)
            {
                string subjectPath = Path.Combine(c_TestsSubjectsDataBase, filename);
                subjects.Add(SubjectInfo.FromCSV(subjectPath));
            }

            return subjects;
        }
        private static void UpdateNextSubjectID()
        {
            File.WriteAllText(m_IDFilePath, Convert.ToString(m_NextSubjectID));
            m_NextSubjectID++;
        }
        private static ulong RestorNextSubjectID()
        {
            ulong ID;

            using (TextReader reader = File.OpenText(m_IDFilePath))
            {
                ID = ulong.Parse(reader.ReadLine());
            }

            return ID;
        }

        private static ulong m_NextSubjectID = RestorNextSubjectID();  // Value of ID for the next test subject
        private const string c_TestsSubjectsDataBase = @"DataBase\Subjects";  // Path to the test subjects directory relative to the main server directory
        private const string c_ResultsDataBase = @"DataBase\Results";  // Path to the test results directory relative to the main server directory
        private const string c_StatisticsFile = "TestResults.csv";  // File with statistics for each subject
        private const string c_AnswerCSVHeader = "QuestionPicture;RightAnswer;UserAnswer;ElapsedTime\n";
        private static readonly string m_TestSubjectsDir = Path.Combine(Directory.GetCurrentDirectory(), c_TestsSubjectsDataBase);  // Absolute path to the directory with test subjects
        private static readonly string m_ResultsDir = Path.Combine(Directory.GetCurrentDirectory(), c_ResultsDataBase);  // Absolute path to the directory with test results
        private static readonly string m_IDFilePath = Path.Combine(m_TestSubjectsDir, "NextSubjectID.id");  // Absolute path to the file with current state of the ID value
    }
}
