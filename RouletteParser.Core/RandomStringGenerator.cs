using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteParser.Core
{
    public static class RandomStringGenerator
    {
        private const string AllowedChars = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
        public static string Get(int stringLength)
        {
            Random random = new Random();
            char[] value = new char[stringLength];
            for (int i = 0; i < value.Length; i++)
                value[i] = AllowedChars[random.Next(AllowedChars.Length)];
            return new string(value);
        }
    }
}
