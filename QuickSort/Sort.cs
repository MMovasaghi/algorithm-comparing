using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    #region Parallel Sort

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

        static void Swap<T>(T[] array, int i, int j) where T : IComparable<T>
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        static int Partition<T>(T[] array, int from, int to, int pivot) where T : IComparable<T>
        {
            var arrayPivot = array[pivot];
            Swap(array, pivot, to - 1);
            var newPivot = from;
            for (int i = from; i < to - 1; i++)
            {
                if (array[i].CompareTo(arrayPivot) != -1)
                {
                    Swap(array, newPivot, i);
                    newPivot++;
                }
            }
            Swap(array, newPivot, to - 1);
            return newPivot;
        }

        public static void SequentialQuickSort<T>(T[] array) where T : IComparable<T>
        {
            SequentialQuickSort(array, 0, array.Length);
        }

        static void SequentialQuickSort<T>(T[] array, int from, int to) where T : IComparable<T>
        {
            if (to - from <= Threshold)
            {
                InsertionSort<T>(array, from, to);
            }
            else
            {
                int pivot = from + (to - from) / 2;
                pivot = Partition<T>(array, from, to, pivot);
                SequentialQuickSort(array, from, pivot);
                SequentialQuickSort(array, pivot + 1, to);
            }
        }

        public static void ParallelQuickSort<T>(T[] array) where T : IComparable<T>
        {
            ParallelQuickSort(array, 0, array.Length, (int)Math.Log(Environment.ProcessorCount, 2) + 4);
        }

        static void ParallelQuickSort<T>(T[] array, int from, int to, int depthRemaining) where T : IComparable<T>
        {
            if (to - from <= Threshold)
            {
                InsertionSort<T>(array, from, to);
            }
            else
            {
                int pivot = from + (to - from) / 2;
                pivot = Partition<T>(array, from, to, pivot);
                if (depthRemaining > 0)
                {
                    ParallelQuickSort(array, from, pivot, depthRemaining);
                    Parallel.Invoke(() => ParallelQuickSort(array, pivot + 1, to, depthRemaining - 1));
                }
                else
                {
                    ParallelQuickSort(array, from, pivot, 0);
                    ParallelQuickSort(array, pivot + 1, to, 0);
                }
            }
        }
    }
    #endregion
}
