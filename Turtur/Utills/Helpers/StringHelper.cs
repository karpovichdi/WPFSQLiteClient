using System;

namespace Turtur.Utills.Helpers
{
    public static class StringHelper
    {
        public static int GetIntFromString(string text) 
        {
            string b = string.Empty;
            int val = -1;

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                {
                    b += text[i];
                }
                else 
                {
                    return -1;
                }
            }

            if (b.Length > 0)
                val = int.Parse(b);

            return val;
        }
    }
}
