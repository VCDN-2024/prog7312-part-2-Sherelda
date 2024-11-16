using st10083869.prog7312.poe.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace st10083869.prog7312.poe
{
    /// <summary>
    /// Interaction logic for Service_Request_Status.xaml
    /// </summary>
    public partial class ServiceRequestStatusWindow : Window
    {
        private BinarySearchTree bst = new BinarySearchTree();
        private PriorityQueue priorityQueue = new PriorityQueue();
        private ObservableCollection<ServiceRequest> requestList = new ObservableCollection<ServiceRequest>();

        public ServiceRequestStatusWindow()
        {
            InitializeComponent();
            // Example Data
            var request1 = new ServiceRequest(1, "Sewage overflow", "Pending", 3, "Area A", DateTime.Now);
            var request2 = new ServiceRequest(2, "Water tanker", "Resolved", 5, "Area B", DateTime.Now);
            var request3 = new ServiceRequest(3, "Garbage collection", "Pending", 4, "Area", DateTime.Now);

            // Insert into BST and Priority Queue
            bst.Insert(request1);
            bst.Insert(request2);
            bst.Insert(request3);
            priorityQueue.Insert(request1);
            priorityQueue.Insert(request2);
            priorityQueue.Insert(request3);

            // Add to ObservableCollection for ListView binding
            requestList.Add(request1);
            requestList.Add(request2);
            requestList.Add(request3);
            lvRequests.ItemsSource = requestList;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int id))
            {
                var result = bst.Search(id);
                if (result != null)
                    MessageBox.Show($"Request Found: {result.Description}, Status: {result.Status}");
                else
                    MessageBox.Show("Request not found.");
            }
        }

        private void ViewPriorityQueueButton_Click(object sender, RoutedEventArgs e)
        {
            var highestPriority = priorityQueue.ExtractMax();
            if (highestPriority != null)
                MessageBox.Show($"Highest Priority: {highestPriority.Description}, Priority: {highestPriority.Priority}");
            else
                MessageBox.Show("No requests in the queue.");
        }

        private void ViewStructureButton_Click(object sender, RoutedEventArgs e)
        {
            tvVisualization.Items.Clear();
            TreeViewItem root = new TreeViewItem();

            switch (cmbDataStructure.SelectedIndex)
            {
                case 0: // Binary Search Tree
                    root.Header = "Binary Search Tree";
                    VisualizeBST(root);
                    break;
                case 1: // AVL Tree
                    root.Header = "AVL Tree";
                    MessageBox.Show("AVL Tree visualization not implemented yet.");
                    break;
                case 2: // Red-Black Tree
                    root.Header = "Red-Black Tree";
                    MessageBox.Show("Red-Black Tree visualization not implemented yet.");
                    break;
                case 3: // Priority Queue
                    root.Header = "Priority Queue";
                    VisualizePriorityQueue(root);
                    break;
            }

            tvVisualization.Items.Add(root);
        }

        private void VisualizeBST(TreeViewItem root)
        {
            // Simple visualization - you can expand this based on your BST implementation
            foreach (var request in requestList.OrderBy(r => r.Id))
            {
                var item = new TreeViewItem
                {
                    Header = $"ID: {request.Id} - {request.Description}"
                };
                root.Items.Add(item);
            }
        }

        private void VisualizePriorityQueue(TreeViewItem root)
        {
            // Create a temporary list to store items while we extract them
            var tempList = new List<ServiceRequest>();

            // Extract all items from priority queue and add to visualization
            while (true)
            {
                var request = priorityQueue.ExtractMax();
                if (request == null) break;

                tempList.Add(request);
                var item = new TreeViewItem
                {
                    Header = $"Priority {request.Priority}: {request.Description}"
                };
                root.Items.Add(item);
            }

            // Reinsert all items back into the priority queue
            foreach (var request in tempList)
            {
                priorityQueue.Insert(request);
            }
        }

        private void ViewServiceAreasButton_Click(object sender, RoutedEventArgs e)
        {
            tvVisualization.Items.Clear();
            var root = new TreeViewItem { Header = "Service Areas" };

            // Group requests by location
            var areaGroups = requestList.GroupBy(r => r.Location);

            foreach (var group in areaGroups)
            {
                var areaNode = new TreeViewItem { Header = group.Key };
                foreach (var request in group)
                {
                    areaNode.Items.Add(new TreeViewItem
                    {
                        Header = $"ID: {request.Id} - {request.Description}"
                    });
                }
                root.Items.Add(areaNode);
            }

            tvVisualization.Items.Add(root);
        }

        private void BackToMainButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the current window and return to the main menu
        }
    }
}