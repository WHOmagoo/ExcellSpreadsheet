using System;

namespace BSTtree
{
    public class Program
    {

        private static readonly int size = 8;
        
        public static void Main(string[] args)
        {
            Random rand = new Random();
            int[] nums = new int[size];
            for (int i = 0; i < size; i++)
            {
                nums[i] = rand.Next(0, size * 3);
                
            }

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
    }
}