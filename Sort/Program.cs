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

            PierreSort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Bubble sort");

            arr = new int[]{ 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            BubbleSort(arr);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Tree sort");

            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            TreeSort(arr);

            //===========================

            Console.WriteLine("Merge sort");

            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            CMergeSort.MergeSort(arr, 0, arr.Length - 1);
            PrintArray(arr);

            //===========================

            Console.WriteLine("Generic Sorting");

            SortArrays();

            //===========================

            Console.WriteLine("Quick sort");

            arr = new int[] { 10, 4, 7, 6, 3, 5, 2, 1, 9, 8, 0 };
            QuickSort quick = new QuickSort();
            quick.QuickSorting(arr, 0, arr.Length - 1);
            PrintArray(arr);

            Console.ReadKey();
        }

        private static void BubbleSort(int[] arr)
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

        private static void PierreSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var IndexOfSmallest = FindIndexOfSmallest(arr, i+1);
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

        private static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        private static void SortArrays()
        {
            string[] array2 = { "black", "yellow", "white", "red" };
            var sortedArray2 = array2.OrderBy(x => x).ToList();
            sortedArray2.ToList().ForEach(a => Console.Write(a + " ")); Console.WriteLine();

            int[] array1 = { 4, 7, 6, 3, 5, 2, 1, 9, 8, 0, 10 };
            var sortedArray1 = array1.OrderBy(x => x).ToList();
            sortedArray1.ToList().ForEach(a => Console.Write(a + " ")); Console.WriteLine();

            array1 = new int[] { 4, 7, 6, 3, 5, 2, 1, 9, 8, 0, 10 };
            Array.Sort(array1);
            sortedArray1.ToList().ForEach(a => Console.Write(a + " ")); Console.WriteLine();

            var ArrayList = array2.ToList();
            ArrayList.Sort((x, y) => string.Compare(x, y));
            ArrayList.ForEach(a => Console.Write(a + " ")); Console.WriteLine();
        }


        private static void TreeSort(int[] arr)
        {
            Node tree = null;
            for (int i = 0; i < arr.Length; i++)
            {
                TreeInsert(ref tree, arr[i]);
            }
            TreePrint(tree);
            Console.WriteLine();
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

        private static void TreePrint(Node node)
        {
            if (node == null)
            {
                return;
            }
            TreePrint(node.Left);
            Console.Write(node.Value + " ");
            TreePrint(node.Right);
        }

    }

    class Node
    {

        public int Value;
        public Node Left;
        public Node Right;
    }


    class QuickSort
    {

        // partitioning array on the key so that the left part is <=key, right part > key
        private int Partition(int[] arr, int start, int end)
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


        // sorting
        public void QuickSorting(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int pivotIndex = Partition(arr, start, end);
                QuickSorting(arr, start, pivotIndex - 1);
                QuickSorting(arr, pivotIndex + 1, end);
            }
        }
        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }

    class CMergeSort
    {
        //merge sort recurcive

        public static void MergeSort(int[] input, int startIndex, int endIndex)
        {
            int mid;

            if (endIndex > startIndex)
            {
                mid = (endIndex + startIndex) / 2;
                MergeSort(input, startIndex, mid);
                MergeSort(input, (mid + 1), endIndex);
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
}
