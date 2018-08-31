using System;

namespace BSTtree
{
    public class BST
    {
        private BST left;
        private BST right;

        private int value;
        
        public BST(int value)
        {
            this.value = value;
        }

        public void add(BST newBST)
        {
            if (newBST.getValue() < value)
            {
                if (left == null)
                {
                    left = newBST;
                }
                else
                {
                    left.add(newBST);
                }
            }
            else if(newBST.getValue() > value)
            {
                if (right == null)
                {
                    right = newBST;
                }
                else
                {
                    right.add(newBST);
                }
            }
            else
            {
                return;
            }
        }

        private int Count()
        {
            int count = 1;
            if (left != null)
            {
                count += left.Count();
            }

            if (right != null)
            {
                count += right.Count();
            }

            return count;
        }

        public int depth()
        {

            int heightL = -1;
            int heightR = -1;
            
            if (left != null)
            {
                heightL = left.depth();
            }

            if (right != null)
            {
                heightR = right.depth();
            }

            return Math.Max(heightL, heightR) + 1;
        }

        public int getValue()
        {
            return value;
        }

        public void printSorted()
        {
            if (left != null)
            {
                left.printSorted();
            }

            Console.WriteLine(value);

            if (right != null)
            {
                right.printSorted();
            }
        }

        public int getOptimalDepth()
        {
            return (int) Math.Ceiling(Math.Log(Count(), 2));
        }

        public void printStats()
        {
            System.Console.WriteLine("Number of items = " + Count());
            System.Console.WriteLine("Maximum Depth = " + depth());
            System.Console.WriteLine("Optimal Depth possible for " + Count() + " items: " + getOptimalDepth());
        }
    }
}