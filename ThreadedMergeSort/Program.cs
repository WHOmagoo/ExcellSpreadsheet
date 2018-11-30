using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ThreadedMergeSort
{
    internal class Program
    {
        private static Random _random = new Random();

        public static void Main(string[] args)
        {
            
            
            int testsCount = 2048;
            int maxSize = 1 << 12;
            
            int[] list = new int[maxSize];

            Console.WriteLine("Processor Count = {0}", Environment.ProcessorCount);
            
            for (int size = 8; size <= maxSize; size <<= 1)
            {
                Console.WriteLine("Size {0} statistics", size);

                for (int threads = 1; threads <= Environment.ProcessorCount << 2; threads <<= 1)
                {
                    TimeSpan totalRunTime = TimeSpan.Zero;
                    TimeSpan totalRunTimeThreadedMerge = TimeSpan.Zero;
                    for (int numberOfTests = 0; numberOfTests < testsCount; numberOfTests++)
                    {
                        randomizeItems(list, size);
                        totalRunTime += timeFunciton(() => threadedMergeSortMaxThreads(list, 0, size, threads));
                    }
                    
                    Console.WriteLine("{0} simultaneous threads. Average time was {1:n0} ticks", threads, totalRunTime.Ticks / testsCount);
                }

                Console.WriteLine();
            }

        }

        public static TimeSpan timeFunciton(Action action)
        {
            Stopwatch timer = Stopwatch.StartNew();

            action();

            timer.Stop();
            
            return timer.Elapsed;
        }

        public static void randomizeItems(int[] list, int size)
        {
            
            while(0 < size)
            {
                size--;
                list[size] = _random.Next(Int32.MaxValue);
            }
        }

        public static void printList(int[] list)
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

        public static void basicMergeSort(int[] list, int startingIndex, int size)
        {
            if (size == 1)
            {
                return;
            }
            
            basicMergeSort(list, startingIndex, size / 2);
            basicMergeSort(list, startingIndex + size / 2, size % 2 == 0? size / 2 : size / 2 + 1);

//            int leftStart = startingIndex;
//            int rightStart = startingIndex + (size % 2 == 0 ? size / 2 : size / 2 + 1);

            mergeTwoLists(list, startingIndex, startingIndex + (size % 2 == 0 ? size / 2 : size / 2 + 1));


        }
        
        public static void threadedMergeSortMaxThreads(int[] list, int startingIndex, int size, int threads)
        {   
            if (size == 1)
            {
                return;
            }

            if (threads < 2)
            {
                basicMergeSort(list, startingIndex, size / 2);
                basicMergeSort(list, startingIndex + size / 2, size % 2 == 0? size / 2 : size / 2 + 1);   
            }
            else
            {
                
                Parallel.Invoke(
                    () => threadedMergeSortMaxThreads(list, startingIndex, size / 2, threads >> 1),
                    () => threadedMergeSortMaxThreads(list, startingIndex + size / 2,
                        size % 2 == 0 ? size / 2 : size / 2 + 1, threads >> 1));
            }

            mergeTwoLists(list, startingIndex, startingIndex + (size % 2 == 0 ? size / 2 : size / 2 + 1));
            
        }
        
        public static void threadedMergeSortUnlimitedThreads(int[] list, int startingIndex, int size)
        {
//            ParameterizedThreadStart leftP = new ParameterizedThreadStart(threadedMergeSort));
            if (size == 1)
            {
                return;
            }
                
            Parallel.Invoke(
                () => threadedMergeSortUnlimitedThreads(list, startingIndex, size / 2),
                () => threadedMergeSortUnlimitedThreads(list, startingIndex + size / 2,
                    size % 2 == 0 ? size / 2 : size / 2 + 1));

            mergeTwoLists(list, startingIndex, startingIndex + (size % 2 == 0 ? size / 2 : size / 2 + 1));
            
        }

        //Assumes that leftStart is lower than rightStart and that the lists are in adjacent in memory
        public static void mergeTwoLists(int[] list, int leftStart, int rightStart)
        {
            for (int i = rightStart - leftStart; i > 0; i--)
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

        public static bool isSorted(int[] list, int size)
        {

            int prev = list[0];
            for (int i = 0; i < size; i++)
            {
                if (prev > list[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}