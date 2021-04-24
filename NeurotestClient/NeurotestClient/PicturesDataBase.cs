using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeurotestServer
{
    /*
     * Wrapper class for the file system.
     * Organizes access to the pictures.
     */
    public static class PicturesDataBase
    {
        /*
         * Generates a list of paths to the pictures for a given configuration of test
         */
        public static List<string> SamplePictures(TestConfig config)
        {
            List<string> angerSample = SampleEmotionType(config.AngerCount, EmotionType.Anger);
            List<string> astonishmentSample = SampleEmotionType(config.AstonishmentCount, EmotionType.Astonishment);
            List<string> disgustSample = SampleEmotionType(config.DisgustCount, EmotionType.Disgust);
            List<string> fearSample = SampleEmotionType(config.FearCount, EmotionType.Fear);
            List<string> happinessSample = SampleEmotionType(config.HappinessCount, EmotionType.Happiness);
            List<string> sadnessSample = SampleEmotionType(config.SadnessCount, EmotionType.Sadness);

            List<string> sample = new List<string>(config.TotalCount);
            sample.AddRange(angerSample);
            sample.AddRange(astonishmentSample);
            sample.AddRange(disgustSample);
            sample.AddRange(fearSample);
            sample.AddRange(happinessSample);
            sample.AddRange(sadnessSample);
            sample = Utils.PermuteList(sample);

            string assertionMessage = "Wrong sampling of {0}. Expected {1} pictures, got {2}.";
            Debug.Assert(angerSample.Count() == TestConfig.c_SeveritiesCount * config.AngerCount,
                string.Format(assertionMessage, "anger", config.AngerCount, angerSample.Count()));
            Debug.Assert(astonishmentSample.Count() == TestConfig.c_SeveritiesCount * config.AstonishmentCount,
                string.Format(assertionMessage, "astonishment", config.AstonishmentCount, astonishmentSample.Count()));
            Debug.Assert(disgustSample.Count() == TestConfig.c_SeveritiesCount * config.DisgustCount,
                string.Format(assertionMessage, "disgust", config.DisgustCount, disgustSample.Count()));
            Debug.Assert(fearSample.Count() == TestConfig.c_SeveritiesCount * config.FearCount,
                string.Format(assertionMessage, "fear", config.FearCount, fearSample.Count()));
            Debug.Assert(happinessSample.Count() == TestConfig.c_SeveritiesCount * config.HappinessCount,
                string.Format(assertionMessage, "happiness", config.HappinessCount, happinessSample.Count()));
            Debug.Assert(sadnessSample.Count() == TestConfig.c_SeveritiesCount * config.SadnessCount,
                string.Format(assertionMessage, "sadness", config.SadnessCount, sadnessSample.Count()));
            Debug.Assert(sample.Distinct().Count() == sample.Count(),
                $"Expected all elements to be unique. Got {sample.Count() - sample.Distinct().Count()} duplicates.");

            return sample;
        }
        public static string ConvertPathToUrl(string path)
        {
            Debug.Assert(File.Exists(path), $"Attempt to convert invalid file path: {path}.");
            return path.Replace(m_PicturesDir, "Pictures").Replace(Path.DirectorySeparatorChar, '/');
        }
        public static string ConvertUrlToPath(string url)
        {
            string path = url.Replace("Pictures", m_PicturesDir).Replace('/', Path.DirectorySeparatorChar);
            Debug.Assert(File.Exists(path), $"Got wrong URL to path convertion: {path}.");
            return path;
        }
        private static List<string> SampleEmotionType(short quantity, EmotionType type)
        {
            var filtered = m_PicturePaths.Where(path => Path.GetDirectoryName(path).Contains(type.ToString("G")));
            var weak = filtered.Where(path => Utils.GetFirstDigit(Path.GetFileName(path)) == EmotionSeverity.Weak.ToString("D")[0]);
            var average = filtered.Where(path => Utils.GetFirstDigit(Path.GetFileName(path)) == EmotionSeverity.Average.ToString("D")[0]);
            var strong = filtered.Where(path => Utils.GetFirstDigit(Path.GetFileName(path)) == EmotionSeverity.Strong.ToString("D")[0]);

            /* Here we use different method of randomization to increase randomness */
            List<string> weakSample = weak.OrderBy(x => Guid.NewGuid()).Take(quantity).ToList();
            List<string> averageSample = average.OrderBy(x => Guid.NewGuid()).Take(quantity).ToList();
            List<string> strongSample = strong.OrderBy(x => Guid.NewGuid()).Take(quantity).ToList();

            List<string> sample = new List<string>();
            sample.AddRange(weakSample);
            sample.AddRange(averageSample);
            sample.AddRange(strongSample);
            sample = Utils.PermuteList(sample);

            return sample;
        }
        /*
         * This method gets paths to all pictures in the database
         */
        private static List<string> ReadDataBase()
        {
            List<string> picturePaths = new List<string>(c_MaxPicturesCount);

            foreach (string emotionDir in Directory.GetDirectories(m_PicturesDir))
            {
                foreach (string picturePath in Directory.GetFiles(emotionDir))
                {
                    string absolutePicturePath = Path.Combine(m_PicturesDir, emotionDir, picturePath);
                    picturePaths.Add(absolutePicturePath);
                }
            }

            var wrongPictures = picturePaths.Where(path =>
                (Utils.GetFirstDigit(Path.GetFileName(path)) != '1') &&
                (Utils.GetFirstDigit(Path.GetFileName(path)) != '2') &&
                (Utils.GetFirstDigit(Path.GetFileName(path)) != '3')
            );
            Debug.Assert(wrongPictures.Count() == 0, $"Database contains wrong pictures: {string.Join(", ", wrongPictures.ToArray())}.");
            Debug.Assert(picturePaths.Count() == c_MaxPicturesCount,
                $"Changes in database detected. Expected {c_MaxPicturesCount} pictures, got {picturePaths.Count()}.");

            return picturePaths;
        }

        private const string c_DataBaseDir = @"DataBase\Pictures";  // Path to the pictures directory relative to the main server directory
        private const ushort c_MaxPicturesCount = 211;  // Maximum total number of pictures
        private static readonly string m_PicturesDir = Path.Combine(Directory.GetCurrentDirectory(), c_DataBaseDir);  // Absolute path to the directory with pictures
        private static readonly List<string> m_PicturePaths = ReadDataBase();  // Absolute paths to all pictures
    }
}
