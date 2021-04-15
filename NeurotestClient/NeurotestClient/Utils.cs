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
    }
}
