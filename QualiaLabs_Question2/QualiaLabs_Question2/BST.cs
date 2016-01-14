using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualiaLabs
{
    class BSTNode
    {
        public string value;
        public double key;
        public BSTNode left;
        public BSTNode right;
        public BSTNode parent;

        public BSTNode(double key, string val, BSTNode parent)
        {
            this.key = key;
            this.value = val;
            this.right = null;
            this.left = null;
            this.parent = parent;
        }
    }
    class BST
    {
        public BSTNode root = null;

        public void insert(double key, string val)
        {
            BSTNode itr = root;
            BSTNode parent = null;

            if (itr == null)
            {
                root = new BSTNode(key, val, parent);
                return;
            }

            while (itr != null)
            {
                parent = itr;
                if (itr.key >= key)
                    if (itr.left != null)
                        itr = itr.left;
                    else
                    {
                        itr.left = new BSTNode(key, val, parent);
                        return;
                    }
                else
                    if (itr.right != null)
                        itr = itr.right;
                    else
                    {
                        itr.right = new BSTNode(key, val, parent);
                        return;
                    }
            }
        }

        public List<string> inOrderTraversal()
        {
            return inOrderTraversal(root, new List<string>());
        }

        public List<string> inOrderTraversal(BSTNode n, List<string> list)
        {
            if (n == null)
                return list;
            inOrderTraversal(n.left, list);
            list.Add(n.value);
            inOrderTraversal(n.right, list);
            return list;
        }
    }
}
