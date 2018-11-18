using System;
using System.Diagnostics;

namespace FourSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //Кол-во элементов в рандомном массива
            Console.Write("Введите количество элементов для сортировки\n");
            int n = int.Parse(Console.ReadLine());

            //Базовый массив
            int[] pred_sort = new int[n];
            Random rnd = new Random();

            //Мин элемент
            Console.WriteLine("\nВведите минимальное значение рандомного элемента");
            int min = int.Parse(Console.ReadLine());

            //Макс элемент
            Console.WriteLine("\nВведите максимальное значение рандомного элемента");
            int max = int.Parse(Console.ReadLine());

            //Рандомная генерация массива
            Console.WriteLine("\nСгенерированный массив:");
            for (int i = 0; i < n; i++)
            {
                pred_sort[i] = rnd.Next(min, max);
                Console.Write(pred_sort[i] + " ");
            }
            Console.WriteLine();
            int[] after_sort;
            //Вызов сортировок\\

            // QUIK SORT - БЫСТРАЯ СОРТИРОВКА
            after_sort = pred_sort;
            Console.WriteLine("\nQuik Sort");
            var timer_quik = Stopwatch.StartNew();
            quickSort(ref after_sort, 0, pred_sort.Length - 1);
            timer_quik.Stop();           
            mas_Out(after_sort);

            // HEAP SORT - Сортировка пирамидой
            after_sort = pred_sort;
            Console.WriteLine("\nHeap Sort");
            var timer_heap = Stopwatch.StartNew();
            heap_sort(ref after_sort, pred_sort.Length);
            timer_heap.Stop();
            mas_Out(after_sort);

            // MERGE SORT - СОРТИРОВКА СЛИЯНИЕМ
            after_sort = pred_sort;
            Console.WriteLine("\nMerge Sort");
            var timer_merge = Stopwatch.StartNew();
            MergeSort(ref after_sort, 0, pred_sort.Length - 1);
            timer_merge.Stop();
            mas_Out(after_sort);

            //Вывод счетчиков
            Console.WriteLine("\nВыполнение QuikSort заняло {0} мс", timer_quik.ElapsedMilliseconds);
            Console.WriteLine("\nВыполнение HeapSort заняло {0} мс", timer_heap.ElapsedMilliseconds);
            Console.WriteLine("\nВыполнение MergeSort заняло {0} мс", timer_merge.ElapsedMilliseconds);

            //Незакрывалка
            Console.ReadKey();
        }
        //Вывод массива
        static void mas_Out(int[] mas)
        {
            int n = mas.Length;
            for (int i = 0; i < n; i++)
            {
                Console.Write(mas[i] + " ");
            }
            Console.WriteLine();
        }
        // Quik Sort
        static void quickSort(ref int[] a, int l, int r)
        {
            int temp;
            int x = a[l + (r - l) / 2];
            //запись эквивалентна (l+r)/2,
            //но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            //код в while обычно выносят в процедуру particle
            while (i <= j)
            {
                while (a[i] < x) i++; while (a[j] > x) j--;
                if (i <= j) { temp = a[i]; a[i] = a[j]; a[j] = temp; i++; j--; }
            }
            if (i < r)
            {
                quickSort(ref a, i, r);
            }           
            if (l < j)
            {
                quickSort(ref a, l, j);
            }

        }
        //HEAP SORT - Сортировка пирамидой
        public static void heap_sort(ref int[] arr, int n)
        {
            int temp;
            heap_make(arr, n);
            while (n > 0)
            {
                temp = arr[0];
                arr[0] = arr[n - 1];
                arr[n - 1] = temp;
                n--;
                heapify(arr, 0, n);
            }
        }
        public static void heapify(int[] arr, int pos, int n)
        {
            int temp;
            while (2 * pos + 1 < n)
            {
                int t = 2 * pos + 1; if (2 * pos + 2 < n && arr[2 * pos + 2] >= arr[t])
                {
                    t = 2 * pos + 2;
                }
                if (arr[pos] < arr[t]) { temp = arr[pos]; arr[pos] = arr[t]; arr[t] = temp; pos = t; } else break;
            }
        }
        public static void heap_make(int[] arr, int n)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                heapify(arr, i, n);
            }
        }
        //MERGE SORT - Сортировка слиянием
        public static void MergeSort(ref int[] input, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSort(ref input, left, middle);
                MergeSort(ref input, middle + 1, right);

                int[] leftArray = new int[middle - left + 1];
                int[] rightArray = new int[right - middle];

                Array.Copy(input, left, leftArray, 0, middle - left + 1);
                Array.Copy(input, middle + 1, rightArray, 0, right - middle);

                int i = 0;
                int j = 0;
                for (int k = left; k < right + 1; k++)
                {
                    if (i == leftArray.Length)
                    {
                        input[k] = rightArray[j];
                        j++;
                    }
                    else if (j == rightArray.Length)
                    {
                        input[k] = leftArray[i];
                        i++;
                    }
                    else if (leftArray[i] <= rightArray[j])
                    {
                        input[k] = leftArray[i];
                        i++;
                    }
                    else
                    {
                        input[k] = rightArray[j];
                        j++;
                    }
                }
            }
        }
    }
}       
