using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkCompressor.Utils
{
    public class ShortURLFork
    {
        private static int PREFIX = 2019;

        public static int Decode(string str)
        {
            return ShortURL.Decode(str) / PREFIX;
        }

        public static string Encode(int id)
        {
            return ShortURL.Encode(id * PREFIX);
        }
    }
}

