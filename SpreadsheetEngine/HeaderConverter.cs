using System;
using System.CodeDom;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

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
            
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] < 'A' && s[i] > 'Z')
                {
                    throw new ArgumentException(String.Format("Error with index {0}", i));
                }
                
                result += (s[i] - 'A' + 1) * (int) Math.Pow(26, s.Length - i - 1);
            }

            char last = s[s.Length - 1];

            if (last < 'A' && last > 'Z')
            {
                //TODO throw an error here
                throw new System.ArgumentException(String.Format("Error with index {0}", s.Length - 1));
                return Int32.MinValue;
            }

            result += (last - 'A');

            return result;
        }

        //s should be a proper reference to a cell such as A5 or BCD567
        public static Tuple<int, int> getCellLocation(string s)
        {
            var match = Regex.Match(s, @"[A-Za-z]+\d+");

            if (match.Success)
            {
                int column = Convert(Regex.Match(s, @"[A-Za-z]+").Value);
                int row = Int32.Parse(Regex.Match(s, @"\d+").Value) - 1;

                return Tuple.Create(column, row);
            }
            else
            {
                throw new ArgumentException("The supplied string did not match a valid cell form");
            }
        }

        public static string getCellName(Tuple<int, int> coordinates)
        {
            return $"{Convert(coordinates.Item1)}{coordinates.Item2 + 1}";
        }
    }    
}