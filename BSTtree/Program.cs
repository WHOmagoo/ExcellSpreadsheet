using System;

namespace BSTtree
{
    public class Program
    {

        private static readonly int size = 20;
        
        public static void Main(string[] args)
        {
            Random rand = new Random();
            int[] nums = new int[size];


            nums = randomizeInput(size);
            nums = parseInput("12 34 65  87 99 lasjdflk 209 493");

            Console.WriteLine("Here we go!");
            
            foreach (var i in nums)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            
            BST root = new BST(nums[0]);
            
            for (int i = 1; i < size; i++)
            {
                root.add(new BST(nums[i]));
            }

            foreach (int num in nums)
            {
                Console.Write(num + ", ");
            }

            Console.WriteLine();
            
            root.printSorted();
            root.printStats();
        }

        public static int[] parseInput(String input)
        {
            string[] stringInts = input.Split(' ');
            int[] ints = new int[stringInts.Length];
            int length = 0;

            foreach (var stringInt in stringInts)
            {
                bool result = Int32.TryParse(stringInt, out ints[length]);

                if (result)
                {
                    length++;
                }
                
            }
            
            Array.Resize(ref ints, length);
            
            return ints;
        }

        public static int[] randomizeInput(int size)
        {
            int[] nums = new int[size];
            
            Random rand = new Random();
            
            for (int i = 0; i < size; i++)
            {
                nums[i] = rand.Next(0, size * 3);
                
            }

            return nums;
        }
    }
}