using System;

namespace SpreadsheetEngine
{
    public class HeaderConverter
    {
        //Not Zero based index
        public static string Convert(int index)
        {
            char[] result = new Char[(int) Math.Ceiling(Math.Log(index - 1, 26))];

            Console.WriteLine("Array size for index {0} is {1}", index, result.Length);
            while (index > 0)
            {
                index--;
                char cur = (char) ('A' + index % 26);

                Console.WriteLine("Inserting into index {0}", (int) Math.Floor(Math.Log(index, 26)));
                result[(int) Math.Floor(Math.Log(index, 26))] = cur;


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