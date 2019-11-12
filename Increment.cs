using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_System
{
    public static class Increment
    {
        public static string IncrementId(string text)
        {
            var textArr = text.ToCharArray();
            var characters = new List<char>();

            for(char c = '0'; c <= '9'; c++)
            {
                characters.Add(c);
            }

            for(int i = text.Length - 1; i >= 0; i--)
            {
                if(textArr[i] == characters.Last())
                {
                    textArr[i] = characters.First();
                }
                else
                {
                    textArr[i] = characters[characters.IndexOf(textArr[i]) + 1];
                    break;
                }
            }

            return new string(textArr);
        }
    }
}
