using System;
using System.CodeDom;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SpreadsheetEngine
{
    public class HeaderConverter
    {
        //Using Zero based index
        //Anything less than 0 will return empty
        //Anything larger than 2^14 - 1 will return empty
        public static string Convert(int index)
        {
            char[] result;
            int placesCount;

            if (index < 0 || index >= Math.Pow(2, 14))
            {
                return String.Empty;
            }


            if (index == 0)
            {
                return "A";
            }


            if (index < 26)
            {
                placesCount = 1;
            } else if (index < 702)
            {
                placesCount = 2;
            }
            else
            {
                placesCount = 3;
            }
            
            result = new Char[placesCount];
            
            //Console.WriteLine("Array size for index {0} is {1}", index, result.Length);
            
            for (int i = 0; i < placesCount; i++)
            {
                char cur = (char) ('A' + (index + 26 - 1) % 26);
                int curIndex = placesCount - i - 1;

                //Console.WriteLine("Inserting {0} into index {1}", cur, curIndex);

                result[curIndex] = cur;

                index /= 26;
                //Console.WriteLine("New index is {0}", index);
            }

            
            result[placesCount - 1] = (char) ('A' + (result[placesCount - 1] - 'A' + 1) % 26);
           
            
            return new string(result);            
        }

        public static int Convert(string s)
        {

            int result = 0;
            
            s.ToUpper();
            
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] < 'A' && s[i] > 'Z')
                {
                    //TODO throw an error here
                    return Int32.MinValue;
                }

                result += (s[i] - 'A') * (int) Math.Pow(26, i);
            }

            return result;
        }
    }    
}