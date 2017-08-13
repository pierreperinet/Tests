using System;
using System.Collections;
using System.Linq;

namespace Sort
{
    internal class Program
    {
        private static void Main()
        {
            int[] arr = { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            PrintArray(arr);

            //===========================

            Console.WriteLine("Pierre sort");
            PierreSort.Sort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Bubble sort");
            arr = new[]{ 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            BubbleSort.Sort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Tree sort");
            arr = new[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            TreeSort.Sort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Merge sort");
            arr = new[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            MergeSort.Sort(arr, 0, arr.Length - 1);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Quick sort");
            arr = new[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            QuickSort.Sort(arr, 0, arr.Length - 1);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Generic Sorting");

            arr = new[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            var sortedArray1 = arr.OrderBy(x => x).ToList();
            PrintArray(sortedArray1);

            arr = new[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            Array.Sort(arr);
            PrintArray(arr);

            arr = new[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            var al = new ArrayList(arr);
            al.Sort();
            PrintArray(al);

            string[] array2 = { "ddd", "ccc", "aaa", "bbb" };
            var sortedArray2 = array2.OrderBy(x => x).ToList();
            PrintArray(sortedArray2);

            var arr3 = array2.ToList();
            arr3.Sort(string.CompareOrdinal);
            PrintArray(arr3);

            Array.Sort(array2);
            PrintArray(array2);

            //===========================

            Console.ReadKey();
        }

        private static void PrintArray(IEnumerable items)
        {
            foreach (var i in items)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }

    internal class QuickSort
    {
        // sorting
        public static void Sort(int[] arr, int start, int end)
        {
            if (start >= end) return;
            var pivotIndex = Partition(arr, start, end);
            Sort(arr, start, pivotIndex - 1);
            Sort(arr, pivotIndex + 1, end);
        }

        // partitioning array on the key so that the left part is <=key, right part > key
        private static int Partition(int[] arr, int start, int end)
        {
            var key = arr[end];
            var pivotIndex = start;
            for (var i = start; i < end; i++)
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

    internal class MergeSort
    {
        //merge sort recurcive

        public static void Sort(int[] input, int startIndex, int endIndex)
        {
            if (endIndex > startIndex)
            {
                var mid = (endIndex + startIndex) / 2;
                Sort(input, startIndex, mid);
                Sort(input, (mid + 1), endIndex);
                Merge(input, startIndex, (mid + 1), endIndex);
            }
        }

        public static void Merge(int[] input, int left, int mid, int right)
        {
            //Merge procedure takes theta(n) time
            var temp = new int[input.Length];
            int i;
            var leftEnd = mid - 1;
            var tmpPos = left;
            var lengthOfInput = right - left + 1;

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

    internal class BubbleSort
    {
        // sorting
        public static void Sort(int[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                for (var sort = 0; sort < arr.Length - 1; sort++)
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

    internal class PierreSort
    {
        // sorting
        public static void Sort(int[] arr)
        {
            for (var i = 0; i < arr.Length - 1; i++)
            {
                var indexOfSmallest = FindIndexOfSmallest(arr, i + 1);
                if (arr[indexOfSmallest] < arr[i])
                {
                    Swap(ref arr[indexOfSmallest], ref arr[i]);
                }
            }
        }

        private static int FindIndexOfSmallest(int[] arr, int low)
        {
            var smallest = arr[low];
            var index = low;
            for (var i = low; i < arr.Length; i++)
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

    internal class TreeSort
    {
        // sorting
        public static void Sort(int[] arr)
        {
            Node tree = null;
            foreach (var t in arr)
            {
                TreeInsert(ref tree, t);
            }
            var index = 0;
            TreePrint(arr, ref index, tree);
        }

        private static void TreeInsert(ref Node root, int value)
        {
            if (root == null)
            {
                root = new Node
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

        private class Node
        {

            public int Value;
            public Node Left;
            public Node Right;
        }
    }
}
