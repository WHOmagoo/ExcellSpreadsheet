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

        public override void InOrder()
        {
            if (left != null)
            {
                left.InOrder();
            }

            Console.Write(value + ", ");

            if (right != null)
            {
                right.InOrder();
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
                    left.Insert(newBST);
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
                    right.Insert(newBST);
                }
            }
            else
            {
                return;
            }
        }

        
        public override void Insert(T value)
        {            
            Insert(new BST<T>(value));
        }

        public override bool Contains(T val)
        {

            if (value.CompareTo(val) == 0)
            {
                return true;
            }

            if (value.CompareTo(val) < 0)
            {
                if (left != null) return left.Contains(val);
                else return false;
            }
            else
            {
                if (right != null) return right.Contains(val);
                else return false;
            }
        }

        public override void PreOrder()
        {
            Console.Write(value + ", ");
            if (left != null)
            {
                left.PreOrder();
            }

            if (right != null)
            {
                right.PreOrder();
            }
        }

        public override void PostOrder()
        {
            if (left != null)
            {
                left.PostOrder();
            }

            if (right != null)
            {
                right.PostOrder();
            }

            Console.Write(value + ", ");
        }

        public static bool operator==(BST<T> a, BST<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;
            
            return a.value.CompareTo(b) == 0;
        }

        public static bool operator !=(BST<T> a, BST<T> b)
        {
            return !(a == b);
        }

        public static bool operator <(BST<T> a, BST<T> b)
        {
            if (ReferenceEquals(a, b)) return false;
            if (ReferenceEquals(a, null)) return true;
            if (ReferenceEquals(b, null)) return false;
            return a.value.CompareTo(b.value) < 0;
        }

        public static bool operator >(BST<T> a, BST<T> b)
        {
            if (ReferenceEquals(a, b)) return false;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return true;
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

        //We don't know how to compare to objects that aren't BST's so we throw an error
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}