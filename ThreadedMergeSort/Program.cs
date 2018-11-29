using System;
using System.Collections.Generic;
using System.Deployment.Internal;

namespace ThreadedMergeSort
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            for (int azz = 0; azz < 2048; azz++)
            {
                for (int size = 2; size <= 2048; size <<= 1)
                {
                    List<int> toSort = generateRandomizedList(size);

//            List<int> toSort = new List<int>(vals);

//            toSort.Add(2);
//            toSort.Add(1);
//            toSort.Add(3);
//            toSort.Add(5);
//            toSort.Add(8);
//            toSort.Add(4);
//            toSort.Add(6);
//            toSort.Add(7);

//            printList(toSort);

//                Console.WriteLine("Sorted");
                    basicMergeSort(toSort, 0, toSort.Count);

//            printList(toSort);
                    if (!isSorted(toSort))
                    {
                        Console.WriteLine("Size {0:D4} is not sorted", size);
                        printList(toSort);

                    }
                }
            }

        }

        public static List<int> generateRandomizedList(int size)
        {
            List<int> result = new List<int>(size);

            Random random = new Random();
            
            
            for (; size > 0; size--)
            {
                result.Add(random.Next(Int32.MaxValue));
            }

            return result;
        }

        public static void printList(List<int> list)
        {
            int i = 0;
            foreach (var item in list)
            {
                Console.Write("{0:D2}, ", item);
                if (i % 8 == 7)
                {
                    Console.WriteLine();
                }

                i++;
            }   
        }

        public static void basicMergeSort(List<int> list, int startingIndex, int size)
        {
            if (size == 1)
            {
                return;
            }
            
            basicMergeSort(list, startingIndex, size / 2);
            basicMergeSort(list, startingIndex + size / 2, size % 2 == 0? size / 2 : size / 2 + 1);

            int leftStart = startingIndex;
            int rightStart = startingIndex + (size % 2 == 0 ? size / 2 : size / 2 + 1);
            
            for (int i = 0; i < size && leftStart - startingIndex  < size / 2 && rightStart - startingIndex < size; i++)
            {
                if (list[leftStart] > list[rightStart])
                {
                    int tmp = list[leftStart];
                    list[leftStart] = list[rightStart];
                    list[rightStart] = tmp;
                    rightStart++;
                }

                leftStart++;
            }
        }

        public static bool isSorted(List<int> list)
        {

            int prev = list[0];
            foreach (var item in list)
            {
                if (prev > item)
                {
                    return false;
                }
            }

            return true;
        }
    }
}