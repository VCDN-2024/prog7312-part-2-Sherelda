using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace st10083869.prog7312.poe.DataStructures
{
    public class TreeNode
    {
        public ServiceRequest Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(ServiceRequest data)
        {
            Data = data;
        }
    }

    public class BinarySearchTree
    {
        public TreeNode Root { get; private set; }

        public void Insert(ServiceRequest request)
        {
            Root = Insert(Root, request);
        }

        private TreeNode Insert(TreeNode node, ServiceRequest request)
        {
            if (node == null) return new TreeNode(request);
            if (request.Id < node.Data.Id)
                node.Left = Insert(node.Left, request);
            else if (request.Id > node.Data.Id)
                node.Right = Insert(node.Right, request);
            return node;
        }

        public ServiceRequest Search(int id)
        {
            return Search(Root, id)?.Data;
        }

        private TreeNode Search(TreeNode node, int id)
        {
            if (node == null || node.Data.Id == id)
                return node;
            return id < node.Data.Id ? Search(node.Left, id) : Search(node.Right, id);
        }

        // Add In-Order Traversal method to get all service requests
        public List<ServiceRequest> GetAllRequests()
        {
            List<ServiceRequest> requests = new List<ServiceRequest>();
            InOrderTraversal(Root, requests);
            return requests;
        }

        private void InOrderTraversal(TreeNode node, List<ServiceRequest> requests)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, requests);
                requests.Add(node.Data);
                InOrderTraversal(node.Right, requests);
            }
        }
    }
}