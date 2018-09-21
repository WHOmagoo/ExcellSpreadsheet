namespace BSTtree
{
    public abstract class BinTree<T>
    {
        public abstract void Insert<T>(T val);

        public abstract bool Contains<T>(T val);

        public abstract void InOrder();

        public abstract void PreOrder();

        public abstract void PostOrder();
    }
}