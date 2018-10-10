using System;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace Spreadsheet
{
    public class HeaderConverter
    {
        //Not Zero based index
        public static string Convert(int index)
        {
            char[] result = new Char[index / 26 + 1];
            while (index > 0)
            {
                index--;
                char cur = (char) ('A' + index % 26);

                result[index / 26] = cur;

                index /= 26;
            }

            String returnResult = "";
            
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != '\0')
                {
                    returnResult += result[i];
                }
                else
                {
                    break;
                }
            }
            
            return returnResult;            
        }

        public static void TestConvert()
        {
            
            String result = Convert(1);
            
            if (!result.Equals("A"))
            {
                Console.WriteLine("1 != A | " + result);
            }

            result = Convert(26);

            if (!result.Equals("Z"))
            {
                Console.WriteLine("26 != Z |" + result);
            }
            
            result = Convert(8);
            
            if (!result.Equals("H"))
            {
                Console.WriteLine("8 != H | " + result);
            }

            result = Convert(35);

            if (!result.Equals("AI"))
            {
                Console.WriteLine("35 != AI " + result);
            }
        }

    }    
}