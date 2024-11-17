using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace st10083869.prog7312.poe
{
    public partial class ServiceRequestStatusWindow : Window
    {
        private class ServiceRequest
        {
            public int RequestId { get; set; }
            public string RequestDescription { get; set; }
            public string RequestStatus { get; set; }
            public int RequestPriority { get; set; }
            public string RequestLocation { get; set; }
            public DateTime RequestDate { get; set; }

            public ServiceRequest(int id, string description, string status, int priority, string location, DateTime date)
            {
                RequestId = id;
                RequestDescription = description;
                RequestStatus = status;
                RequestPriority = priority;
                RequestLocation = location;
                RequestDate = date;
            }
        }

        // AVL Tree Implementation
        private class AVLNode
        {
            public ServiceRequest Data;
            public AVLNode Left, Right;
            public int Height;

            public AVLNode(ServiceRequest data)
            {
                Data = data;
                Height = 1;
            }
        }

        private class AVLTree
        {
            private AVLNode root;

            private int GetHeight(AVLNode node)
            {
                return node?.Height ?? 0;
            }

            private int GetBalance(AVLNode node)
            {
                if (node == null) return 0;
                return GetHeight(node.Left) - GetHeight(node.Right);
            }

            private AVLNode RightRotate(AVLNode y)
            {
                AVLNode x = y.Left;
                AVLNode T2 = x.Right;

                x.Right = y;
                y.Left = T2;

                y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
                x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

                return x;
            }

            private AVLNode LeftRotate(AVLNode x)
            {
                AVLNode y = x.Right;
                AVLNode T2 = y.Left;

                y.Left = x;
                x.Right = T2;

                x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
                y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

                return y;
            }

            public void Insert(ServiceRequest value)
            {
                root = InsertRec(root, value);
            }

            private AVLNode InsertRec(AVLNode node, ServiceRequest value)
            {
                if (node == null)
                    return new AVLNode(value);

                if (value.RequestId < node.Data.RequestId)
                    node.Left = InsertRec(node.Left, value);
                else if (value.RequestId > node.Data.RequestId)
                    node.Right = InsertRec(node.Right, value);
                else
                    return node;

                node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

                int balance = GetBalance(node);

                // Left Left Case
                if (balance > 1 && value.RequestId < node.Left.Data.RequestId)
                    return RightRotate(node);

                // Right Right Case
                if (balance < -1 && value.RequestId > node.Right.Data.RequestId)
                    return LeftRotate(node);

                // Left Right Case
                if (balance > 1 && value.RequestId > node.Left.Data.RequestId)
                {
                    node.Left = LeftRotate(node.Left);
                    return RightRotate(node);
                }

                // Right Left Case
                if (balance < -1 && value.RequestId < node.Right.Data.RequestId)
                {
                    node.Right = RightRotate(node.Right);
                    return LeftRotate(node);
                }

                return node;
            }

            public ServiceRequest Search(int id)
            {
                return SearchRec(root, id)?.Data;
            }

            private AVLNode SearchRec(AVLNode node, int id)
            {
                if (node == null || node.Data.RequestId == id)
                    return node;

                if (id < node.Data.RequestId)
                    return SearchRec(node.Left, id);
                return SearchRec(node.Right, id);
            }

            public List<ServiceRequest> GetInOrderTraversal()
            {
                var result = new List<ServiceRequest>();
                InOrderTraversal(root, result);
                return result;
            }

            private void InOrderTraversal(AVLNode node, List<ServiceRequest> result)
            {
                if (node != null)
                {
                    InOrderTraversal(node.Left, result);
                    result.Add(node.Data);
                    InOrderTraversal(node.Right, result);
                }
            }
        }

        // Max Heap Implementation
        private class MaxHeap
        {
            private List<ServiceRequest> heap = new List<ServiceRequest>();

            private int Parent(int i) => (i - 1) / 2;
            private int LeftChild(int i) => 2 * i + 1;
            private int RightChild(int i) => 2 * i + 2;

            private void Swap(int i, int j)
            {
                var temp = heap[i];
                heap[i] = heap[j];
                heap[j] = temp;
            }

            public void Insert(ServiceRequest request)
            {
                heap.Add(request);
                HeapifyUp(heap.Count - 1);
            }

            private void HeapifyUp(int i)
            {
                while (i > 0 && heap[Parent(i)].RequestPriority < heap[i].RequestPriority)
                {
                    Swap(i, Parent(i));
                    i = Parent(i);
                }
            }

            public ServiceRequest ExtractMax()
            {
                if (heap.Count == 0) return null;

                ServiceRequest max = heap[0];
                heap[0] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);

                if (heap.Count > 0)
                    HeapifyDown(0);

                return max;
            }

            private void HeapifyDown(int i)
            {
                int maxIndex = i;
                int left = LeftChild(i);
                int right = RightChild(i);

                if (left < heap.Count && heap[left].RequestPriority > heap[maxIndex].RequestPriority)
                    maxIndex = left;

                if (right < heap.Count && heap[right].RequestPriority > heap[maxIndex].RequestPriority)
                    maxIndex = right;

                if (i != maxIndex)
                {
                    Swap(i, maxIndex);
                    HeapifyDown(maxIndex);
                }
            }

            public List<ServiceRequest> GetAllRequests()
            {
                return new List<ServiceRequest>(heap);
            }
        }

        // Class members
        private readonly AVLTree avlTree;
        private readonly MaxHeap maxHeap;
        private readonly ObservableCollection<ServiceRequest> requestList;
        private readonly ObservableCollection<ServiceRequest> filteredList;
        private readonly Dictionary<string, int> graphData;

        public ServiceRequestStatusWindow()
        {
            InitializeComponent();
            avlTree = new AVLTree();
            maxHeap = new MaxHeap();
            requestList = new ObservableCollection<ServiceRequest>();
            filteredList = new ObservableCollection<ServiceRequest>();
            graphData = new Dictionary<string, int>();

            LoadHardcodedData();
            InitializeChart();
        }

        private void LoadHardcodedData()
        {
            var requests = new List<ServiceRequest>
            {
                new ServiceRequest(1, "Home affairs system updates", "Critical", 5, "All areas", DateTime.Now.AddDays(-2)),
                new ServiceRequest(2, "Trash bag's delivery ", "Pending", 3, "Suburban Area", DateTime.Now.AddDays(-5)),
                new ServiceRequest(3, "Fix potholes", "In Progress", 2, "Residential", DateTime.Now.AddDays(-3)),
                new ServiceRequest(4, "water tanker needed", "Completed", 1, "Business District", DateTime.Now.AddDays(-7)),
                new ServiceRequest(5, "Parks need to be cleaned", "Scheduled", 2, "Park Zone", DateTime.Now.AddDays(-1)),
                new ServiceRequest(6, "Sewer blockage", "Critical", 5, "Industrial Area", DateTime.Now),
                new ServiceRequest(7, "Traffic light repair", "In Progress", 4, "Downtown", DateTime.Now.AddDays(-4)),
                new ServiceRequest(8, "New road signs", "Pending", 2, "Residential", DateTime.Now.AddDays(-6)),
                new ServiceRequest(9, "Bridge construction", "Scheduled", 3, "Flood Zone", DateTime.Now.AddDays(-2)),
                new ServiceRequest(10, "water meter checks", "In Progress", 1, "Urban Center", DateTime.Now.AddDays(-1))
            };

            foreach (var request in requests)
            {
                avlTree.Insert(request);
                maxHeap.Insert(request);
                requestList.Add(request);
                filteredList.Add(request);
            }

            lvRequests.ItemsSource = filteredList;
            UpdateChart();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSearchBy.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search category", "Search Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a search term", "Search Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string searchTerm = txtSearch.Text.ToLower().Trim();
            filteredList.Clear();
            List<ServiceRequest> searchResults = new List<ServiceRequest>();

            switch (cmbSearchBy.SelectedIndex)
            {
                case 0: // ID
                    if (int.TryParse(searchTerm, out int id))
                    {
                        var result = avlTree.Search(id);
                        if (result != null)
                            searchResults.Add(result);
                    }
                    break;

                case 1: // Description
                    searchResults = requestList.Where(r =>
                        r.RequestDescription.ToLower().Contains(searchTerm)).ToList();
                    break;

                case 2: // Status
                    searchResults = requestList.Where(r =>
                        r.RequestStatus.ToLower().Contains(searchTerm)).ToList();
                    break;

                case 3: // Priority
                    if (int.TryParse(searchTerm, out int priority))
                    {
                        searchResults = requestList.Where(r =>
                            r.RequestPriority == priority).ToList();
                    }
                    break;

                case 4: // Location
                    searchResults = requestList.Where(r =>
                        r.RequestLocation.ToLower().Contains(searchTerm)).ToList();
                    break;
            }

            foreach (var result in searchResults)
            {
                filteredList.Add(result);
            }

            if (filteredList.Count == 0)
            {
                MessageBox.Show("No matching records found", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetSearch();
            }

            UpdateChart();
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            ResetSearch();
        }

        private void ResetSearch()
        {
            txtSearch.Clear();
            cmbSearchBy.SelectedIndex = -1;
            filteredList.Clear();
            foreach (var request in requestList)
            {
                filteredList.Add(request);
            }
            UpdateChart();
        }

        private void SortByPriorityButton_Click(object sender, RoutedEventArgs e)
        {
            var sorted = new ObservableCollection<ServiceRequest>(
                filteredList.OrderByDescending(r => r.RequestPriority)
            );
            filteredList.Clear();
            foreach (var request in sorted)
            {
                filteredList.Add(request);
            }
        }

        private void SortByDateButton_Click(object sender, RoutedEventArgs e)
        {
            var sorted = new ObservableCollection<ServiceRequest>(
                filteredList.OrderByDescending(r => r.RequestDate)
            );
            filteredList.Clear();
            foreach (var request in sorted)
            {
                filteredList.Add(request);
            }
        }

        private void InitializeChart()
        {
            if (graphCanvas == null || graphCanvas.ActualWidth <= 0 || graphCanvas.ActualHeight <= 0)
                return;

            graphCanvas.Children.Clear();

            // Draw Y-Axis
            Line yAxis = new Line
            {
                X1 = 0,
                Y1 = 0,
                X2 = 0,
                Y2 = graphCanvas.ActualHeight - 30,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            graphCanvas.Children.Add(yAxis);

            // Draw X-Axis
            Line xAxis = new Line
            {
                X1 = 0,
                Y1 = graphCanvas.ActualHeight - 30,
                X2 = graphCanvas.ActualWidth,
                Y2 = graphCanvas.ActualHeight - 30,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            graphCanvas.Children.Add(xAxis);
        }

        private void UpdateChart()
        {
            if (graphCanvas == null || graphCanvas.ActualWidth <= 0 || graphCanvas.ActualHeight <= 0)
                return;

            graphData.Clear();
            var statusGroups = filteredList
                .GroupBy(r => r.RequestStatus)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var status in new[] { "Critical", "In Progress", "Pending", "Scheduled", "Completed" })
            {
                graphData[status] = statusGroups.ContainsKey(status) ? statusGroups[status] : 0;
            }

            graphCanvas.Children.Clear();
            InitializeChart();

            int maxValue = Math.Max(1, graphData.Values.Max());
            int yAxisMax = ((maxValue + 4) / 5) * 5;

            // Draw Y-axis labels
            for (int i = 0; i <= 5; i++)
            {
                int value = i * (yAxisMax / 5);
                TextBlock label = new TextBlock
                {
                    Text = value.ToString(),
                    FontSize = 12
                };
                Canvas.SetLeft(label, -35);
                Canvas.SetTop(label, graphCanvas.ActualHeight - 35 - (i * (graphCanvas.ActualHeight - 40) / 5));
                graphCanvas.Children.Add(label);
            }

            double availableWidth = Math.Max(0, graphCanvas.ActualWidth - 40);
            double barWidth = Math.Max(20, availableWidth / (graphData.Count * 2));
            double spacing = Math.Max(5, barWidth / 2);

            int index = 0;
            foreach (var kvp in graphData)
            {
                double barHeight = Math.Max(0, (kvp.Value * (graphCanvas.ActualHeight - 60)) / yAxisMax);

                if (barWidth > 0 && barHeight >= 0)
                {
                    Rectangle bar = new Rectangle
                    {
                        Width = barWidth,
                        Height = barHeight,
                        Fill = GetBarColor(index),
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };

                    double leftPosition = spacing + (index * (barWidth + spacing));
                    Canvas.SetLeft(bar, leftPosition);
                    Canvas.SetTop(bar, graphCanvas.ActualHeight - 30 - barHeight);
                    graphCanvas.Children.Add(bar);

                    TextBlock label = new TextBlock
                    {
                        Text = kvp.Key,
                        FontSize = 10,
                        TextWrapping = TextWrapping.Wrap,
                        Width = barWidth * 1.5,
                        TextAlignment = TextAlignment.Center
                    };
                    Canvas.SetLeft(label, leftPosition - barWidth * 0.25);
                    Canvas.SetTop(label, graphCanvas.ActualHeight - 25);
                    graphCanvas.Children.Add(label);

                    if (kvp.Value > 0)
                    {
                        TextBlock valueLabel = new TextBlock
                        {
                            Text = kvp.Value.ToString(),
                            FontSize = 10,
                            TextAlignment = TextAlignment.Center,
                            Width = barWidth
                        };
                        Canvas.SetLeft(valueLabel, leftPosition);
                        Canvas.SetTop(valueLabel, Math.Max(5, graphCanvas.ActualHeight - 35 - barHeight));
                        graphCanvas.Children.Add(valueLabel);
                    }
                }
                index++;
            }
        }

        private Brush GetBarColor(int index)
        {
            var colors = new Brush[]
            {
                new SolidColorBrush(Color.FromRgb(231, 76, 60)),   // Red for Critical
                new SolidColorBrush(Color.FromRgb(52, 152, 219)),  // Blue for In Progress
                new SolidColorBrush(Color.FromRgb(241, 196, 15)),  // Yellow for Pending
                new SolidColorBrush(Color.FromRgb(46, 204, 113)),  // Green for Scheduled
                new SolidColorBrush(Color.FromRgb(155, 89, 182))   // Purple for Completed
            };
            return colors[index % colors.Length];
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (sizeInfo.NewSize.Width > 0 && sizeInfo.NewSize.Height > 0)
            {
                UpdateChart();
            }
        }

        private void BackToMainButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to return to the main menu?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                 MainWindow mainWindow = new MainWindow();
    mainWindow.Show();
    this.Close();
            }
        }
    }
}