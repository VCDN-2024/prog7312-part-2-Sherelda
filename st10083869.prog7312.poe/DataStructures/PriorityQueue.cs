using st10083869.prog7312.poe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st10083869.prog7312.poe.DataStructures
{
    public class PriorityQueue
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        public ServiceRequest ExtractMax()
        {
            if (heap.Count == 0) return null;
            var max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return max;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0 && heap[index].Priority > heap[(index - 1) / 2].Priority)
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        private void HeapifyDown(int index)
        {
            int maxIndex = index;
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            if (left < heap.Count && heap[left].Priority > heap[maxIndex].Priority)
                maxIndex = left;
            if (right < heap.Count && heap[right].Priority > heap[maxIndex].Priority)
                maxIndex = right;
            if (index != maxIndex)
            {
                Swap(index, maxIndex);
                HeapifyDown(maxIndex);
            }
        }

        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
public class PriorityQueue
{
    private List<ServiceRequest> heap = new List<ServiceRequest>();

    public void Insert(ServiceRequest request)
    {
        heap.Add(request);
        HeapifyUp(heap.Count - 1);
    }

    public ServiceRequest ExtractMax()
    {
        if (heap.Count == 0) return null;
        var max = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);
        return max;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && heap[index].Priority > heap[(index - 1) / 2].Priority)
        {
            Swap(index, (index - 1) / 2);
            index = (index - 1) / 2;
        }
    }

    private void HeapifyDown(int index)
    {
        int maxIndex = index;
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        if (left < heap.Count && heap[left].Priority > heap[maxIndex].Priority)
            maxIndex = left;
        if (right < heap.Count && heap[right].Priority > heap[maxIndex].Priority)
            maxIndex = right;
        if (index != maxIndex)
        {
            Swap(index, maxIndex);
            HeapifyDown(maxIndex);
        }
    }

    private void Swap(int i, int j)
    {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}
