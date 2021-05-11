using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    public static class Sort
    {
        public static int Threshold = 150;
        public static void InsertionSort<T>(T[] array, int from, int to) where T : IComparable<T>
        {
            for (int i = from + 1; i < to; i++)
            {
                var a = array[i];
                int j = i - 1;

                while (j >= from && array[j].CompareTo(a) == -1)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = a;
            }
        }
        static void Merge<T>(T[] arr, int l, int m, int r) where T : IComparable<T>
        {
            int n1 = m - l + 1;
            int n2 = r - m;

            T[] L = new T[n1];
            T[] R = new T[n2];
            int i, j;

            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            // Merge the temp arrays

            // Initial indexes of first
            // and second subarrays
            i = 0;
            j = 0;

            // Initial index of merged
            // subarry array
            int k = l;
            while (i < n1 && j < n2)
            {
                
                if (L[i].CompareTo(R[j]) == -1)
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        public static void SequentialMergeSort<T>(T[] array) where T : IComparable<T>
        {
            SequentialMergeSort(array, 0, array.Length - 1);
        }
        // Main function that
        // sorts arr[l..r] using
        // merge()
        static void SequentialMergeSort<T>(T[] arr, int from, int to) where T : IComparable<T>
        {
            if (from < to)
            {
                if (to - from <= Threshold)
                {
                    InsertionSort<T>(arr, from, to);
                }
                else
                {
                    // Find the middle
                    // point
                    int m = from + (to - from) / 2;

                    // Sort first and
                    // second halves
                    Sort.SequentialMergeSort<T>(arr, from, m);
                    Sort.SequentialMergeSort<T>(arr, m + 1, to);

                    // Merge the sorted halves
                    Sort.Merge<T>(arr, from, m, to);
                }
                
            }
        }

        public static void ParallelMergeSort<T>(T[] array) where T : IComparable<T>
        {
            ParallelMergeSort(array, 0, array.Length - 1, (int)Math.Log(Environment.ProcessorCount, 2) + 4);
        }

        static void ParallelMergeSort<T>(T[] array, int from, int to, int depthRemaining) where T : IComparable<T>
        {
            if (from < to)
            {
                if (to - from <= Threshold)
                {
                    InsertionSort<T>(array, from, to);
                }
                else
                {
                    // Find the middle
                    // point
                    int m = from + (to - from) / 2;

                    // Sort first and
                    // second halves
                    if (depthRemaining > 0)
                    {
                        Parallel.Invoke(
                            () => ParallelMergeSort(array, from, m, depthRemaining - 1),
                            () => ParallelMergeSort(array, m + 1, to, depthRemaining - 1));
                    }
                    else
                    {
                        ParallelMergeSort(array, from, m, 0);
                        ParallelMergeSort(array, m + 1, to, 0);
                    }
                    // Merge the sorted halves
                    Sort.Merge<T>(array, from, m, to);
                }
                
            }
        }
    }
}
