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
        public static List<Question> CreateTest(TestConfig config)
        {
            List<Question> questions = new List<Question>(config.TotalCount);

            foreach (string path in PicturesDataBase.SamplePictures(config))
            {
                Debug.Assert(File.Exists(path), $"Got invalid picture path: {path}.");
                questions.Add(new Question(path));
            }

            return questions;
        }
    }
}
