using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st10083869.prog7312.poe
{
    public class RedBlackTree<T> where T : IComparable<T>
    {
        private enum Color { RED, BLACK }

        private class Node
        {
            public T Data;
            public Node Left, Right, Parent;
            public Color Color;

            public Node(T data)
            {
                Data = data;
                Left = Right = Parent = null;
                Color = Color.RED;
            }
        }

        private Node root;

        public void Insert(T data)
        {
            Node newNode = new Node(data);
            if (root == null)
            {
                root = newNode;
                root.Color = Color.BLACK;
                return;
            }

            Node current = root;
            Node parent = null;

            while (current != null)
            {
                parent = current;
                if (data.CompareTo(current.Data) < 0)
                    current = current.Left;
                else if (data.CompareTo(current.Data) > 0)
                    current = current.Right;
                else
                    return; // Duplicate value, do not insert
            }

            newNode.Parent = parent;
            if (data.CompareTo(parent.Data) < 0)
                parent.Left = newNode;
            else
                parent.Right = newNode;

            FixViolation(newNode);
        }

        private void FixViolation(Node node)
        {
            while (node != root && node.Parent != null && node.Parent.Color == Color.RED)
            {
                Node parent = node.Parent;
                Node grandParent = parent.Parent;

                if (grandParent == null)
                    break;

                if (parent == grandParent.Left)
                {
                    Node uncle = grandParent.Right;

                    if (uncle != null && uncle.Color == Color.RED)
                    {
                        grandParent.Color = Color.RED;
                        parent.Color = Color.BLACK;
                        uncle.Color = Color.BLACK;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Right)
                        {
                            RotateLeft(parent);
                            node = parent;
                            parent = node.Parent;
                        }

                        RotateRight(grandParent);
                        SwapColors(parent, grandParent);
                        node = parent;
                    }
                }
                else
                {
                    Node uncle = grandParent.Left;

                    if (uncle != null && uncle.Color == Color.RED)
                    {
                        grandParent.Color = Color.RED;
                        parent.Color = Color.BLACK;
                        uncle.Color = Color.BLACK;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Left)
                        {
                            RotateRight(parent);
                            node = parent;
                            parent = node.Parent;
                        }

                        RotateLeft(grandParent);
                        SwapColors(parent, grandParent);
                        node = parent;
                    }
                }
            }

            root.Color = Color.BLACK;
        }

        private void RotateLeft(Node node)
        {
            Node rightChild = node.Right;
            node.Right = rightChild.Left;

            if (rightChild.Left != null)
                rightChild.Left.Parent = node;

            rightChild.Parent = node.Parent;

            if (node.Parent == null)
                root = rightChild;
            else if (node == node.Parent.Left)
                node.Parent.Left = rightChild;
            else
                node.Parent.Right = rightChild;

            rightChild.Left = node;
            node.Parent = rightChild;
        }

        private void RotateRight(Node node)
        {
            Node leftChild = node.Left;
            node.Left = leftChild.Right;

            if (leftChild.Right != null)
                leftChild.Right.Parent = node;

            leftChild.Parent = node.Parent;

            if (node.Parent == null)
                root = leftChild;
            else if (node == node.Parent.Right)
                node.Parent.Right = leftChild;
            else
                node.Parent.Left = leftChild;

            leftChild.Right = node;
            node.Parent = leftChild;
        }

        private void SwapColors(Node node1, Node node2)
        {
            Color temp = node1.Color;
            node1.Color = node2.Color;
            node2.Color = temp;
        }

        public List<T> InOrderTraversal()
        {
            List<T> result = new List<T>();
            InOrderTraversalRec(root, result);
            return result;
        }

        private void InOrderTraversalRec(Node node, List<T> result)
        {
            if (node != null)
            {
                InOrderTraversalRec(node.Left, result);
                result.Add(node.Data);
                InOrderTraversalRec(node.Right, result);
            }
        }
    }
}