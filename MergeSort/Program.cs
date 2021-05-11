using System;
using System.Diagnostics;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ARRAY_LENGTH = 999999;

            int[] my_array = new int[ARRAY_LENGTH];
            int[] my_array_copy = new int[ARRAY_LENGTH];

            Random random = new Random();
            int tmp = 0;
            for (int i = 0; i < ARRAY_LENGTH; i++)
            {
                tmp = random.Next(1, 10000);
                my_array[i] = tmp;
                my_array_copy[i] = tmp;
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Parallel Merge Sort
            Sort.ParallelMergeSort<int>(my_array);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}", ts.Milliseconds / 10);
            Console.WriteLine("RunTime Of Parallel Quick Sort (ms): " + elapsedTime);

            stopWatch.Start();

            //Sequential Merge Sort
            Sort.SequentialMergeSort<int>(my_array_copy);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            elapsedTime = elapsedTime = String.Format("{0:00}", ts.Milliseconds / 10);
            Console.WriteLine("RunTime Of Sequential Quick Sort (ms): " + elapsedTime);

        }
    }
}