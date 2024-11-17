using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace st10083869.prog7312.poe
{
    public class Graph<T>
    {
        private Dictionary<T, List<Tuple<T, int>>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<T, List<Tuple<T, int>>>();
        }

        public void AddVertex(T vertex)
        {
            if (!adjacencyList.ContainsKey(vertex))
            {
                adjacencyList[vertex] = new List<Tuple<T, int>>();
            }
        }

        public void AddEdge(T source, T destination, int weight)
        {
            if (!adjacencyList.ContainsKey(source))
                AddVertex(source);
            if (!adjacencyList.ContainsKey(destination))
                AddVertex(destination);
            adjacencyList[source].Add(new Tuple<T, int>(destination, weight));
            adjacencyList[destination].Add(new Tuple<T, int>(source, weight));
        }

        public List<T> DepthFirstSearch(T startVertex)
        {
            var visited = new HashSet<T>();
            var result = new List<T>();
            DFSUtil(startVertex, visited, result);
            return result;
        }

        private void DFSUtil(T vertex, HashSet<T> visited, List<T> result)
        {
            visited.Add(vertex);
            result.Add(vertex);
            foreach (var neighbor in adjacencyList[vertex])
            {
                if (!visited.Contains(neighbor.Item1))
                {
                    DFSUtil(neighbor.Item1, visited, result);
                }
            }
        }

        public List<Tuple<T, T, int>> MinimumSpanningTree()
        {
            var result = new List<Tuple<T, T, int>>();
            var allVertices = adjacencyList.Keys.ToList();
            var visited = new HashSet<T>();
            if (allVertices.Count == 0)
                return result;
            visited.Add(allVertices[0]);
            var priorityQueue = new List<Tuple<T, T, int>>();
            foreach (var neighbor in adjacencyList[allVertices[0]])
            {
                priorityQueue.Add(new Tuple<T, T, int>(allVertices[0], neighbor.Item1, neighbor.Item2));
            }
            while (priorityQueue.Count > 0 && visited.Count < allVertices.Count)
            {
                priorityQueue.Sort((x, y) => x.Item3.CompareTo(y.Item3));
                var edge = priorityQueue[0];
                priorityQueue.RemoveAt(0);
                if (visited.Contains(edge.Item2))
                    continue;
                result.Add(edge);
                visited.Add(edge.Item2);
                foreach (var neighbor in adjacencyList[edge.Item2])
                {
                    if (!visited.Contains(neighbor.Item1))
                    {
                        priorityQueue.Add(new Tuple<T, T, int>(edge.Item2, neighbor.Item1, neighbor.Item2));
                    }
                }
            }
            return result;
        }

        public Dictionary<T, List<Tuple<T, int>>> GetAdjacencyList()
        {
            return new Dictionary<T, List<Tuple<T, int>>>(adjacencyList);
        }

        public List<T> GetAllVertices()
        {
            return new List<T>(adjacencyList.Keys);
        }
    }
}