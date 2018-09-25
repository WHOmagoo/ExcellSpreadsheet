using System;

namespace BSTtree
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter an arbitrary amount of integers separated by spaces.");
            
            String response = Console.ReadLine();

            int[] nums = parseInput(response);
            
            BST<int> root = new BST<int>(nums[0]);
            
            for (int i = 1; i < nums.Length; i++)
            {
                root.Insert(nums[i]);
            }

            Console.WriteLine("The numbers are: ");
            
            foreach (int num in nums)
            {
                Console.Write(num + ", ");
            }

            Console.Write("\n\nHere are the numbers in sorted order: ");
            
            root.InOrder();

            Console.WriteLine();
            
            Console.Write("\n\nHere are the numbers in preOrder: ");
            
            root.PreOrder();
            Console.WriteLine();
            
            Console.Write("\n\nHere are the numbers in postOrder: ");
            root.PostOrder();
            Console.WriteLine();
            
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