using System;
using System.Collections.Generic;

namespace BSTtree
{
    public class BST<T> : BinTree<T>, IComparable where T : IComparable
    {
        private BST<T> left;
        private BST<T> right;

        private T value;
        
        public BST(T value)
        {
            this.value = value;
        }

        public void add(BST<T> newBST)
        {
            
        }

        public int Count()
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

            int heightL = 0;
            int heightR = 0;
            
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

        public T getValue()
        {
            return value;
        }

        public void printSorted()
        {
            if (left != null)
            {
                left.printSorted();
            }

            Console.Write(value + ", ");

            if (right != null)
            {
                right.printSorted();
            }
        }

        public int getOptimalDepth()
        {
            return (int) Math.Ceiling(Math.Log(Count() + 1, 2));
        }

        public void printStats()
        {
            System.Console.WriteLine("Number of items = " + Count());
            System.Console.WriteLine("Maximum Depth = " + depth());
            System.Console.WriteLine("Optimal Depth possible for " + Count() + " items: " + getOptimalDepth());
        }

        public void Insert(BST<T> newBST)
        {
            if (newBST < this)
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
            else if(newBST > this)
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

        
        public override void Insert(T value)
        {
            BST<T> newBST = new BST<T>((T) value);
            
            if (newBST < this)
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
            else if(newBST > this)
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

        public override bool Contains(T val)
        {
            
            throw new NotImplementedException();
        }

        public override void InOrder()
        {
            throw new NotImplementedException();
        }

        public override void PreOrder()
        {
            throw new NotImplementedException();
        }

        public override void PostOrder()
        {
            throw new NotImplementedException();
        }

        public static bool operator==(BST<T> a, BST<T> b)
        {
            return a.value.CompareTo(b) == 0;
        }

        public static bool operator !=(BST<T> a, BST<T> b)
        {
            return a.value.CompareTo(b.value) != 0;
        }

        public static bool operator <(BST<T> a, BST<T> b)
        {
            return a.value.CompareTo(b.value) < 0;
        }

        public static bool operator >(BST<T> a, BST<T> b)
        {
            return a.value.CompareTo(b.value) > 0;
        }

        public static bool operator >=(BST<T> a, BST<T> b)
        {
            return a == b || a > b;
        }

        public static bool operator <=(BST<T> a, BST<T> b)
        {
            return a == b || a < b;
        }

        public int CompareTo(BST<T> other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return value.CompareTo(other.value);
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}