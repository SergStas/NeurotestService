using System;
using System.Text;

namespace NeurotestServer
{
    public struct WatchInfo
    {
        public static WatchInfo FromJSON(JSONWrappers.WatchInfo jsonInfo)
        {
            VideoInfo video = VideoInfo.FromJSON(jsonInfo.Video);
            DateTime startTime = Convert.ToDateTime(jsonInfo.StartTime);
            DateTime endTime = Convert.ToDateTime(jsonInfo.EndTime);

            return new WatchInfo(video, startTime, endTime);
        }
        public string ToCSVString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Video.Path);
            builder.Append(";");
            builder.Append(Video.Type.ToString("G"));
            builder.Append(";");
            builder.Append(StartTime);
            builder.Append(";");
            builder.Append(EndTime);
            builder.Append("\n");

            return builder.ToString();
        }
        public readonly VideoInfo Video { get; }
        public readonly DateTime StartTime { get; }  // Time of the beginning of the watch
        public readonly DateTime EndTime { get; }  // Time of the end of the watch
        private WatchInfo(VideoInfo video, DateTime startTime, DateTime endTime)
        {
            Video = video;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
