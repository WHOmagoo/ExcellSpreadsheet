using System;

namespace CptS321
{
    public abstract class UnaryOperator : Operator
    {
        protected internal override bool inClassAdd(ExpNode node)
        {
            if (right == null)
            {
                right = node;
                node.parent = this;
                return true;
            }

            if (!right.inClassAdd(node))
            {
                node.left = right;
                node.parent = this;
                right.parent = node;
                right = node;
            }

            return true;
        }

        protected UnaryOperator(int prescedence) : base(prescedence)
        {
        }
    }
}
