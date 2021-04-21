using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeurotestServer
{
    /*
     * Test constructor which will generate test sequence.
     */
    public static class TestGenerator
    {
        public static List<JSONWrappers.Question> CreateTest(TestConfig config)
        {
            List<JSONWrappers.Question> questions = new List<JSONWrappers.Question>(config.TotalCount);

            foreach (string path in PicturesDataBase.SamplePictures(config))
            {
                Debug.Assert(File.Exists(path), $"Got invalid picture path: {path}.");
                Question question = new Question(path);
                questions.Add(question.ToJSON());
            }

            return questions;
        }
    }
}
