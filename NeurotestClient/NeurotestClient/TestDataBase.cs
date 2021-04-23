﻿using System;
using System.IO;
using System.Linq;
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
            File.WriteAllText(path, builder.ToString(), Encoding.UTF8);

            /* Writing statistics to CSV file */
            string statisticsFilePath = Path.Combine(m_ResultsDir, c_StatisticsFile);
            File.AppendAllText(statisticsFilePath, result.ToCSVString(), Encoding.UTF8);
        }
        /* 
         * Creates TestSubject instance and stors it in the csv format.
         * Returns an ID of created subject.
         * */
        public static ulong CreateNewSubject(SubjectInfo info)
        {
            TestSubject subject = new TestSubject(m_NextSubjectID, info);
            UpdateNextSubjectID();

            File.AppendAllText(m_SubjectsFilePath, subject.ToCSVString(), Encoding.UTF8);

            return subject.ID;
        }
        /* Reads subjects data */
        public static List<JSONWrappers.SubjectInfo> GetSubjectsInfos()
        {
            /* Skipping header */
            IEnumerable<string> csvRows = File.ReadLines(m_SubjectsFilePath).Skip(1);

            List<JSONWrappers.SubjectInfo> subjects = new List<JSONWrappers.SubjectInfo>(csvRows.Count());
            foreach (string row in csvRows)
            {
                SubjectInfo info = SubjectInfo.FromCSV(row);
                subjects.Add(info.ToJSON());
            }

            return subjects;
        }
        private static void UpdateNextSubjectID()
        {
            File.WriteAllText(m_IDFilePath, Convert.ToString(++m_NextSubjectID));
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

        private const string c_TestsSubjectsDataBase = @"DataBase\Subjects";  // Path to the test subjects directory relative to the main server directory
        private const string c_ResultsDataBase = @"DataBase\Results";  // Path to the test results directory relative to the main server directory
        private const string c_StatisticsFile = "TestResults.csv";  // File with statistics for each subject
        private const string c_AnswerCSVHeader = "QuestionPicture;RightAnswer;UserAnswer;ElapsedTime\n";
        private static readonly string m_TestSubjectsDir = Path.Combine(Directory.GetCurrentDirectory(), c_TestsSubjectsDataBase);  // Absolute path to the directory with test subjects
        private static readonly string m_ResultsDir = Path.Combine(Directory.GetCurrentDirectory(), c_ResultsDataBase);  // Absolute path to the directory with test results
        private static readonly string m_IDFilePath = Path.Combine(m_TestSubjectsDir, "NextSubjectID.id");  // Absolute path to the file with current state of the ID value
        private static readonly string m_SubjectsFilePath = Path.Combine(m_TestSubjectsDir, "Subjects.csv"); // Absolute path to the file with subjects data
        private static ulong m_NextSubjectID = RestorNextSubjectID();  // Value of ID for the next test subject
    }
}
