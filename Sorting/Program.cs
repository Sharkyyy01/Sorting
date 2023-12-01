// ? using System.Diagnostics.Metrics;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Sorting
{
    internal class Program
    {
        public static void Main()
        {

            int[] arr = new int[100];
            Random rnd = new Random();


            // для тестирования
            int[] arr_01 = new int[] { 1, -2, 4, 5, 6, 3, -6, -11, 78, 100, 256, 64 };


            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(1, 100);
            }

            // вывод исходного массива
            Console.WriteLine("Исходный массив");
            Console.WriteLine(string.Join(" ", arr));
            Console.WriteLine(" ");

            // вывод BubbleSort (пузырьковая сортировка)
            Console.WriteLine("Сортировка пузырьком");
            Console.WriteLine(string.Join(" ", BubbleSort(arr)));

            // вывод ExchangeSorting (сортировка обменом)
            Console.WriteLine("\nСортировка обменом");
            Console.WriteLine(string.Join(" ", ExchangeSorting(arr)));

            // вывод InsertionSort (сортировка вставками)
            Console.WriteLine("\nСортировка вставками");
            Console.WriteLine(string.Join(" ", InsertionSort(arr)));

            // вывод ShellSort (сортировка методом шелла)
            Console.WriteLine("\nСортировка методом шелла");
            Console.WriteLine(string.Join(" ", ShellSort(arr)));

            // вывод HeapSort (пирамидальная сортировка)
            HeapSort ob = new HeapSort();
            ob.heapSort(arr);

            Console.WriteLine("\nПирамидальная сортировка");
            HeapSort.printArray(arr);

            // вывод QuickSort (быстрая сортировка)
            Console.WriteLine("\nБыстрая сортировка");
            // Console.WriteLine(string.Join(" ", QuickSort(arr)));

            // вывод MergeSort (сортировка слиянием, самая важная сортировка)
            Console.WriteLine("\nСортировка слиянием");
            Console.WriteLine(string.Join(" ", MergeSort.Sort(arr)));

        }

        /*static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }*/

        public static int[] BubbleSort(int[] arr)
        {

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            return arr;
        }

        public static int[] ExchangeSorting(int[] arr)
        {
            bool flag;
            do
            {
                flag = false;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int temp = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = temp;
                        flag = true;
                    }
                }
            } while (flag);
            return arr;
        }

        public static int[] InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int k = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > k)
                {
                    arr[j + 1] = arr[j];
                    arr[j] = k;
                    j--;
                }
            }
            return arr;
        }

        public static int[] ShellSort(int[] arr)
        {
            var d = arr.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < arr.Length; i++)
                {
                    var j = i;
                    while ((j >= d) && (arr[j - d] > arr[j]))
                    {
                        int team = arr[j];
                        arr[j] = arr[j - d];
                        arr[j - d] = team;
                        j = j - d;
                    }
                }

                d = d / 2;
            }

            return arr;
        }

        public class HeapSort
        {
            public void heapSort(int[] arr)
            {
                int n = arr.Length;

                // Построение кучи (перегруппируем массив)
                for (int i = n / 2 - 1; i >= 0; i--)
                {
                    heapify(arr, n, i);
                }
                // Один за другим извлекаем элементы из кучи
                for (int i = n - 1; i >= 0; i--)
                {
                    // Перемещаем текущий корень в конец
                    int temp = arr[0];
                    arr[0] = arr[i];
                    arr[i] = temp;

                    // вызываем процедуру heapify на уменьшенной куче
                    heapify(arr, i, 0);
                }
            }

            // Процедура для преобразования в двоичную кучу поддерева с корневым узлом i, что является индексом в arr[]. n - размер кучи
            void heapify(int[] arr, int n, int i)
            {
                int largest = i;
                // Инициализируем наибольший элемент как корень
                int l = 2 * i + 1; // left = 2*i + 1
                int r = 2 * i + 2; // right = 2*i + 2

                // Если левый дочерний элемент больше корня
                if (l < n && arr[l] > arr[largest])
                    largest = l;

                // Если правый дочерний элемент больше, чем самый большой элемент на данный момент
                if (r < n && arr[r] > arr[largest])
                    largest = r;

                // Если самый большой элемент не корень
                if (largest != i)
                {
                    int swap = arr[i];
                    arr[i] = arr[largest];
                    arr[largest] = swap;

                    // Рекурсивно преобразуем в двоичную кучу затронутое поддерево
                    heapify(arr, n, largest);
                }
            }
            public static void printArray(int[] arr)
            {
                int n = arr.Length;
                for (int i = 0; i < n; ++i)
                    Console.Write(arr[i] + " ");
                Console.WriteLine("");
            }
        }


        // quicksort не смотрите оно не доделано
        public static void QuickSort(int[] arr, int start, int end)
        {
            if (start >= end)
                return;

            int pivot = Partition(arr, start, end);
            QuickSort(arr, start, pivot - 1);
            QuickSort(arr, pivot + 1, end);
        }

        private static int Partition(int[] arr, int start, int end)
        {
            int marker = start;
            int pivotValue = arr[end];

            for (int i = start; i < end; i++)
            {
                if (arr[i] < pivotValue)
                {
                    int temp = arr[marker];
                    arr[marker] = arr[i];
                    arr[i] = temp;
                    marker += 1;
                }
            }

            arr[end] = arr[marker];
            arr[marker] = pivotValue;

            return marker;
        }

        public class MergeSort
        {
            public static int[] Sort(int[] arr)
            {
                if (arr.Length == 1)
                {
                    return arr;
                }

                int mid_point = arr.Length / 2;

                return Merge(Sort(arr.Take(mid_point).ToArray()), Sort(arr.Skip(mid_point).ToArray()));
            }

            public static int[] Merge(int[] arr1, int[] arr2)
            {
                int a = 0, b = 0;
                int[] merged = new int[arr1.Length + arr2.Length];

                for (int i = 0; i < arr1.Length + arr2.Length; i++)
                {
                    if (b < arr2.Length && a < arr1.Length)
                    {
                        if (arr1[a] > arr2[b] && b < arr2.Length)
                        {
                            merged[i] = arr2[b++];
                        }
                        else
                            merged[i] = arr1[a++];
                    }
                    else
                    {
                        if (b < arr2.Length)
                        {
                            merged[i] = arr2[b++];
                        }
                        else
                            merged[i] = arr1[a++];
                    }
                }
                return merged;
            }
        }
    }
}
