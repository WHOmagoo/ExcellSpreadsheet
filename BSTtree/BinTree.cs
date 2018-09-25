using System;

namespace BSTtree
{
    public abstract class BinTree<T> where T : IComparable
    {

    public abstract void Insert(T val);

    public abstract bool Contains(T val);

    public abstract void InOrder();

    public abstract void PreOrder();

    public abstract void PostOrder();
    }
}