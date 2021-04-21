using System;
using System.Linq;
using System.Collections.Generic;

namespace NeurotestServer
{
    /*
     * Class that contains project level utility functions
     */
    public static class Utils
    {
        public static char GetFirstDigit(string filename)
        {
            int firstDigitIndex = filename.IndexOfAny("0123456789".ToCharArray());
            if (firstDigitIndex == -1)
                return ' ';

            return filename[firstDigitIndex];
        }
        public static List<T> PermuteList<T>(List<T> list)
        {
            Random random = new Random();
            return list.OrderBy(x => random.Next(int.MaxValue)).ToList();
        }
        public static EmotionType EmotionTypeStringToEnum(string typeString)
        {
            if (typeString == "Anger")
                return EmotionType.Anger;
            if (typeString == "Astonishment")
                return EmotionType.Astonishment;
            if (typeString == "Disgust")
                return EmotionType.Disgust;
            if (typeString == "Fear")
                return EmotionType.Fear;
            if (typeString == "Happiness")
                return EmotionType.Happiness;
            if (typeString == "Sadness")
                return EmotionType.Sadness;
            return EmotionType.Undefined;
        }
        public static Sex SexStringToEnum(string sexString)
        {
            if (sexString == "Male")
                return Sex.Male;
            if (sexString == "Female")
                return Sex.Female;
            return Sex.Undefined;
        }
    }
}
