using System.IO;
using System.Diagnostics;

namespace NeurotestServer
{
    public struct VideoInfo
    {
        /*
         * Constructor takes in an absolute path to the video.
         * It calculates type of the emotion.
         */
        public VideoInfo(string path)
        {
            Debug.Assert(File.Exists(path), "Got invalid path to the video.");

            Path = path;
            Type = CulcEmotionType(path);
        }
        public static VideoInfo FromJSON(JSONWrappers.VideoInfo jsonInfo)
        {
            return new VideoInfo(VideosDataBase.ConvertUrlToPath(jsonInfo.Url));
        }
        public JSONWrappers.VideoInfo ToJSON()
        {
            JSONWrappers.VideoInfo jsonInfo = new JSONWrappers.VideoInfo
            {
                Url = VideosDataBase.ConvertPathToUrl(Path),
                Type = Type.ToString("G")
            };

            return jsonInfo;
        }
        public readonly string Path { get; }  // Absolute path to the video
        public readonly EmotionType Type { get; }  // Type of the emotion in this video
        private static EmotionType CulcEmotionType(string path)
        {
            string filename = System.IO.Path.GetFileName(path);

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
    }
}
