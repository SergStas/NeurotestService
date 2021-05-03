using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeurotestServer
{
    /*
     * Wrapper class for the file system.
     * Organizes access to the videos.
     */
    public static class VideosDataBase
    {
        /*
         * Samples VideoInfo wrappers from paths to all videos
         */
        public static List<JSONWrappers.VideoInfo> GetAllVideos()
        {
            Debug.Assert(m_VideosPaths.Count() > 0, "Videos paths must be initialized at this point.");
            List<JSONWrappers.VideoInfo> jsonVideos = new List<JSONWrappers.VideoInfo>(c_VideosCount);

            foreach (string videoPath in m_VideosPaths)
            {
                VideoInfo video = new VideoInfo(videoPath);
                jsonVideos.Add(video.ToJSON());
            }

            return jsonVideos;
        }
        /*
         * Samples tuple of videos based on the types of emotions the subject recognized best and worst.
         * The first element in the sample stands for best video, last - for worst one.
         */
        public static List<JSONWrappers.VideoInfo> GetVideoSample(EmotionType best, EmotionType worst)
        {
            Debug.Assert((best != EmotionType.Undefined) && (worst != EmotionType.Undefined), "Got invalid values for emotion types.");

            /* Filtering videos categories by required emotion types */
            var bestVideos = m_VideosPaths.Where(path => Path.GetDirectoryName(path).Contains(best.ToString("G")));
            var worstVideos = m_VideosPaths.Where(path => Path.GetDirectoryName(path).Contains(worst.ToString("G")));

            /* 
             * Randomly choosing best and worst video from all filtered videos.
             * Creating Random instance twice to increase randomness.
             */
            int bestVideoIndex = new Random().Next(bestVideos.Count());
            int worstVideoIndex = new Random().Next(worstVideos.Count());

            VideoInfo bestVideo = new VideoInfo(bestVideos.ElementAt(bestVideoIndex));
            VideoInfo worstVideo = new VideoInfo(worstVideos.ElementAt(worstVideoIndex));

            return new List<JSONWrappers.VideoInfo> { bestVideo.ToJSON(), worstVideo.ToJSON() };
        }
        /*
         * Saves result of the watch session in the database
         */
        public static void SubmitVideoSessionResult(ulong subjectID, List<WatchInfo> session)
        {
            /* Writing watch session to CSV file */
            string filename = $"Videos{Convert.ToString(subjectID)}.csv";
            string path = Path.Combine(m_ResultsDir, filename);
            StringBuilder builder = new StringBuilder(c_VideoCSVHeader);

            foreach (WatchInfo info in session)
            {
                builder.Append(info.ToCSVString());
            }
            File.WriteAllText(path, builder.ToString(), Encoding.UTF8);
        }
        public static string ConvertPathToUrl(string path)
        {
            Debug.Assert(File.Exists(path), $"Attempt to convert invalid file path: {path}.");
            return path.Replace(m_VideosDir, "Videos").Replace(Path.DirectorySeparatorChar, '/');
        }
        public static string ConvertUrlToPath(string url)
        {
            string path = url.Replace("Videos", m_VideosDir).Replace('/', Path.DirectorySeparatorChar);
            Debug.Assert(File.Exists(path), $"Got wrong URL to path convertion: {path}.");
            return path;
        }
        /*
         * This method gets paths to all videos in the database
         */
        private static List<string> ReadDataBase()
        {
            List<string> videoPaths = new List<string>(c_VideosCount);

            foreach (string emotionDir in Directory.GetDirectories(m_VideosDir))
            {
                foreach (string videoPath in Directory.GetFiles(emotionDir))
                {
                    string absoluteVideoPath = Path.Combine(m_VideosDir, emotionDir, videoPath);
                    videoPaths.Add(absoluteVideoPath);
                }
            }

            return videoPaths;
        }

        private const string c_DataBaseDir = @"DataBase\Videos";  // Path to the videos directory relative to the main server directory
        private const string c_ResultsDataBase = @"DataBase\Results";  // Copy of the constant from TestDataBase
        private const string c_VideoCSVHeader = "VideoPath;Type;StartTime;EndTime\n";
        private const ushort c_VideosCount = 62;  // Total number of videos
        private static readonly string m_VideosDir = Path.Combine(Directory.GetCurrentDirectory(), c_DataBaseDir);  // Absolute path to the directory with videos
        private static readonly string m_ResultsDir = Path.Combine(Directory.GetCurrentDirectory(), c_ResultsDataBase);  // Copy of the member from TestDataBase
        private static readonly List<string> m_VideosPaths = ReadDataBase();  // Absolute paths to all videos
    }
}
