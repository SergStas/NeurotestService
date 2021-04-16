using System;
using System.IO;
using System.Diagnostics;

namespace NeurotestServer
{
    public struct Question
    {
        /*
         * Constructor takes in an absolute path to the picture.
         * It calculates the value of Type and Severity.
         */
        public Question(string path)
        {
            string dir = System.IO.Path.GetDirectoryName(path);
            Debug.Assert(Directory.Exists(dir), "Invalid path to the picture.");

            Path = path;
            Type = CulcEmotionType(path);
            Severity = CulcEmotionSeverity(path);
        }
        public Answer Ask()
        {
            throw new NotImplementedException();
        }
        public readonly string Path { get; }  // Absolute path to the picture
        public readonly EmotionType Type { get; }  // Type of the emotion in this picture
        public readonly EmotionSeverity Severity { get; }  // The severity of the emotion in this picture
        private static EmotionType CulcEmotionType(string path)
        {
            string filename = GetFileName(path);

            if (filename.StartsWith("Anger"))
                return EmotionType.Anger;
            else if (filename.StartsWith("Astonishment"))
                return EmotionType.Astonishment;
            else if (filename.StartsWith("Disgust"))
                return EmotionType.Disgust;
            else if (filename.StartsWith("Fear"))
                return EmotionType.Fear;
            else if (filename.StartsWith("Happiness"))
                return EmotionType.Happiness;
            else if (filename.StartsWith("Sadness"))
                return EmotionType.Sadness;

            return EmotionType.Undefined;
        }
        private static EmotionSeverity CulcEmotionSeverity(string path)
        {
            string filename = GetFileName(path);
            char severityID = Utils.GetFirstDigit(filename);

            return severityID switch
            {
                '1' => EmotionSeverity.Weak,
                '2' => EmotionSeverity.Average,
                '3' => EmotionSeverity.Strong,
                _ => EmotionSeverity.Undefined
            };
        }
        private static string GetFileName(string path) => System.IO.Path.GetFileName(path);
    }
}
