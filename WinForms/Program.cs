using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            
            Random r = new Random();
            
            int[] nums = new int[10000];
            
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = r.Next(0,20000);
            }
            
            Dictionary<int, int> dictionary = new Dictionary<int, int>(20000);
            

            DateTime startTime = DateTime.UtcNow;
            
            int duplicates = 0;
            foreach (var i in nums)
            {
                try
                {
                    dictionary.Add(i, i);
                }
                catch (ArgumentException e)
                {
                    duplicates++;
                }
            }

            DateTime endTime = DateTime.UtcNow;

            String dictionaryResult = "There are " + duplicates + " duplicates, it took " +
                                      (endTime - startTime) + " for dictionary to finish";
            
            Console.WriteLine(dictionaryResult);

            startTime = DateTime.UtcNow;

            duplicates = 0;

            for (int i = 0; i < nums.Length;i++)
            {
                if (listContains(nums, i + 1, nums[i]))
                {
                    duplicates++;
                }
            }

            endTime = DateTime.UtcNow;

            String oNResult = "There are " + duplicates + " duplicates, it took " + (endTime - startTime) +
                              " for O(1) space complexity to finish";
            
            Console.WriteLine(oNResult);


            startTime = DateTime.UtcNow;

            duplicates = 0;

            Array.Sort(nums);

            int prevNum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (prevNum == nums[i])
                {
                    duplicates++;
                }

                prevNum = nums[i];
            }

            endTime = DateTime.UtcNow;

            String sortedResult = "There are " + duplicates + " duplicates, it took " + (endTime - startTime) +
                                  " for sorted array";
            
            Console.WriteLine(sortedResult);


            Application.EnableVisualStyles();
            Application.Run(new Form1());

        }

        public static bool listContains(int[] list, int startingIndex, int number)
        {
            for (int i = startingIndex; i < list.Length; i++)
            {
                if (list[i] == number)
                {
                    return true;
                }
            }

            return false;
        }
    }
}