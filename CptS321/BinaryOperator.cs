using System;

namespace CptS321
{
    public abstract class BinaryOperator : Operator
    {

        protected internal override bool inClassAdd(ExpNode node)
        {
            if (right == null)
            {
                setChild(node);
//                node.parent = this;
//                right = node;
                return true;
            }
            
            if (!right.inClassAdd(node))
            {
                if (getPrescedence() < node.getPrescedence())
                {
                    right.setParent(node);
//                    node.parent = this;
//                    node.left = right;
//                    node.left.parent = node;
//                    right = node;
                    return true;
                }
                
                if (parent == null)
                {
                    setParent(node);
//                    parent = node;
//                    node.left = this;
                    return true;
                }

                return false;
            }

            return true;
        }

        protected BinaryOperator(int prescedence) : base(prescedence)
        {
        }
    }
}