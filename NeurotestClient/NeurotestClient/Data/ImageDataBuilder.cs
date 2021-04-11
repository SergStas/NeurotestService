using System;
using System.IO;
using System.Collections.Generic;

namespace NeurotestClient.Data
{
    using Url = String;
    using ImageInfoList = List<ImageInfo>;
    public static class ImageDataBuilder
    {
        public static Url[] GetUrls(ITestConfig config)
        {
            // TO DO
            return null;
        }
        private static ImageInfoList GetImagesInfo()
        {
            ImageInfoList info = new ImageInfoList();

            foreach (string dir in Directory.GetDirectories(m_ImagesDir))
            {
                foreach (string filename in Directory.GetFiles(dir))
                {
                    string abs_path = Path.Combine(m_ImagesDir, dir, filename);
                    ImageInfo imageInfo = new ImageInfo(abs_path);
                    info.Add(imageInfo);
                }
            }

            return info;
        }

        private static readonly ImageInfoList m_Info = GetImagesInfo();
        private static readonly string m_ImagesDir = Path.Combine(Directory.GetCurrentDirectory(), "Фото");
    }
    public enum EmotionType
    {
        Anger = 0,
        Disgust,
        Sadness,
        Joy,
        Fear,
        Astonishment,
        Undefined
    }
    public enum EmotionLevel
    {
        Weak = 0,
        Average,
        Strong,
        Undefined
    }
    public struct ImageInfo
    {
        public ImageInfo(string path)
        {
            m_Path = path;
            m_Type = ComputeType(path);
            m_Level = ComputeLevel(path);
        }
        public string GetPath() => m_Path;
        public EmotionType GetEmotionType() => m_Type;
        public EmotionLevel GetEmotionLevel() => m_Level;
        private static EmotionLevel ComputeLevel(string path)
        {
            string filename = Path.GetFileName(path);
            int firstDigitIndex = filename.IndexOfAny("0123456789".ToCharArray());
            char firstDigit = filename[firstDigitIndex];

            if (firstDigit == '1')
                return EmotionLevel.Weak;
            else if (firstDigit == '2')
                return EmotionLevel.Average;
            else if (firstDigit == '3')
                return EmotionLevel.Strong;
            else
                return EmotionLevel.Undefined;
        }
        private static EmotionType ComputeType(string path)
        {
            string filename = Path.GetFileName(path);

            if (filename.StartsWith("Гнев"))
                return EmotionType.Anger;
            else if (filename.StartsWith("Отвращение"))
                return EmotionType.Disgust;
            else if (filename.StartsWith("Печаль"))
                return EmotionType.Sadness;
            else if (filename.StartsWith("Радость"))
                return EmotionType.Joy;
            else if (filename.StartsWith("Страх"))
                return EmotionType.Fear;
            else if (filename.StartsWith("Удивление"))
                return EmotionType.Astonishment;
            else
                return EmotionType.Undefined;
        }

        private string m_Path;
        private EmotionType m_Type;
        private EmotionLevel m_Level;
    }
}