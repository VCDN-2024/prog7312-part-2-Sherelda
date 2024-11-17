using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st10083869.prog7312.poe
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> heap;

        public MinHeap()
        {
            heap = new List<T>();
        }

        public void Insert(T item)
        {
            heap.Add(item);
            int i = heap.Count - 1;
            while (i > 0 && heap[(i - 1) / 2].CompareTo(heap[i]) > 0)
            {
                T temp = heap[(i - 1) / 2];
                heap[(i - 1) / 2] = heap[i];
                heap[i] = temp;
                i = (i - 1) / 2;
            }
        }

        public T ExtractMin()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            T min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            int i = 0;
            while (true)
            {
                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;
                int smallest = i;

                if (leftChild < heap.Count && heap[leftChild].CompareTo(heap[smallest]) < 0)
                    smallest = leftChild;

                if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[smallest]) < 0)
                    smallest = rightChild;

                if (smallest == i)
                    break;

                T temp = heap[i];
                heap[i] = heap[smallest];
                heap[smallest] = temp;
                i = smallest;
            }

            return min;
        }

        public List<T> GetAllItems()
        {
            return new List<T>(heap);
        }
    }
}