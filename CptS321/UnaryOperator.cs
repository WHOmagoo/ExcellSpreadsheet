using System;

namespace CptS321
{
    public abstract class UnaryOperator : BinaryOperator
    {
        protected internal override bool inClassAdd(ExpNode node)
        {
            if (right == null)
            {
                right = node;
                node.parent = this;
                return true;
            }

            return right.inClassAdd(node);
        }
    }
}
