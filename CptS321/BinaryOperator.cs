using System;

namespace CptS321
{
    public abstract class BinaryOperator : Operator
    {

        public override void add(ExpNode node)
        {
            if (right == null)
            {
                setChild(node);
            } else if (getPrescedence() < node.getPrescedence())
            {
                right.setParent(node);
            } else if (parent == null)
            {
                setParent(node);
            }
            else
            {
                parent.add(node);
            } 
        }

        protected BinaryOperator(int prescedence) : base(prescedence)
        {
        }
    }
}