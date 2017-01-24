using System;
using System.Linq;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            PrintArray(arr);

            //===========================

            Console.WriteLine("Pierre sort");
            PierreSort.Sort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Bubble sort");
            arr = new int[]{ 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            BubbleSort.Sort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Tree sort");
            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            TreeSort.Sort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Merge sort");
            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            MergeSort.Sort(arr, 0, arr.Length - 1);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Quick sort");
            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            QuickSort.Sort(arr, 0, arr.Length - 1);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Generic Sorting");

            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            var sortedArray1 = arr.OrderBy(x => x).ToList();
            PrintArray(sortedArray1);

            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            Array.Sort(arr);
            PrintArray(arr);

            string[] array2 = { "ddd", "ccc", "aaa", "bbb" };
            var sortedArray2 = array2.OrderBy(x => x).ToList();
            PrintArray(sortedArray2);

            var arr3 = array2.ToList();
            arr3.Sort((x, y) => string.Compare(x, y));
            PrintArray(arr3);

            Array.Sort(array2);
            PrintArray(array2.ToList());

            //===========================

            Console.ReadKey();
        }

        private static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        private static void PrintArray<T>(System.Collections.Generic.List<T> list)
        {
            list.ForEach(a => Console.Write(a + " "));
            Console.WriteLine();
        }
    }

    class QuickSort
    {
        // sorting
        public static void Sort(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int pivotIndex = Partition(arr, start, end);
                Sort(arr, start, pivotIndex - 1);
                Sort(arr, pivotIndex + 1, end);
            }
        }

        // partitioning array on the key so that the left part is <=key, right part > key
        private static int Partition(int[] arr, int start, int end)
        {
            int key = arr[end];
            int pivotIndex = start;
            for (int i = start; i < end; i++)
            {
                if (arr[i] <= key)
                {
                    Swap(ref arr[pivotIndex], ref arr[i]);
                    pivotIndex++;
                }
            }
            Swap(ref arr[pivotIndex], ref arr[end]);
            return pivotIndex;
        }

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }

    class MergeSort
    {
        //merge sort recurcive

        public static void Sort(int[] input, int startIndex, int endIndex)
        {
            int mid;

            if (endIndex > startIndex)
            {
                mid = (endIndex + startIndex) / 2;
                Sort(input, startIndex, mid);
                Sort(input, (mid + 1), endIndex);
                Merge(input, startIndex, (mid + 1), endIndex);
            }
        }

        public static void Merge(int[] input, int left, int mid, int right)
        {
            //Merge procedure takes theta(n) time
            int[] temp = new int[input.Length];
            int i, leftEnd, lengthOfInput, tmpPos;
            leftEnd = mid - 1;
            tmpPos = left;
            lengthOfInput = right - left + 1;

            //selecting smaller element from left sorted array or right sorted array and placing them in temp array.
            while ((left <= leftEnd) && (mid <= right))
            {
                if (input[left] <= input[mid])
                {
                    temp[tmpPos++] = input[left++];
                }
                else
                {
                    temp[tmpPos++] = input[mid++];
                }
            }

            //placing remaining elements in temp from left sorted array
            while (left <= leftEnd)
            {
                temp[tmpPos++] = input[left++];
            }

            //placing remaining element in temp from right sorted array
            while (mid <= right)
            {
                temp[tmpPos++] = input[mid++];
            }

            //placing temp array to input
            for (i = 0; i < lengthOfInput; i++)
            {
                input[right] = temp[right];
                right--;
            }
        }
    }

    class BubbleSort
    {
        // sorting
        public static void Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int sort = 0; sort < arr.Length - 1; sort++)
                {
                    if (arr[sort] > arr[sort + 1])
                    {
                        Swap(ref arr[sort + 1], ref arr[sort]);
                    }
                }
            }
        }

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }

    class PierreSort
    {
        // sorting
        public static void Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var IndexOfSmallest = FindIndexOfSmallest(arr, i + 1);
                if (arr[IndexOfSmallest] < arr[i])
                {
                    Swap(ref arr[IndexOfSmallest], ref arr[i]);
                }
            }
        }

        private static int FindIndexOfSmallest(int[] arr, int low)
        {
            int smallest = arr[low];
            int index = low;
            for (int i = low; i < arr.Length; i++)
            {
                if (smallest > arr[i])
                {
                    smallest = arr[i];
                    index = i;
                }
            }
            return index;
        }

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }

    class TreeSort
    {
        // sorting
        public static void Sort(int[] arr)
        {
            Node tree = null;
            for (int i = 0; i < arr.Length; i++)
            {
                TreeInsert(ref tree, arr[i]);
            }
            int index = 0;
            TreePrint(arr, ref index, tree);
        }

        private static void TreeInsert(ref Node root, int value)
        {
            if (root == null)
            {
                root = new Node()
                {
                    Value = value,
                    Left = null,
                    Right = null
                };
                return;
            }
            if (value < root.Value)
            {
                TreeInsert(ref root.Left, value);
            }
            else
            {
                TreeInsert(ref root.Right, value);
            }
        }

        private static void TreePrint(int[] arr, ref int index, Node node)
        {
            if (node == null)
            {
                return;
            }
            TreePrint(arr, ref index, node.Left);
            arr[index++] = node.Value;
            TreePrint(arr, ref index, node.Right);
        }

        class Node
        {

            public int Value;
            public Node Left;
            public Node Right;
        }
    }
}
