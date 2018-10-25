using System;
using System.Text.RegularExpressions;

namespace CptS321
{
    public abstract class BinaryOperator : Operator
    {

        protected internal override bool inClassAdd(ExpNode node)
        {
            if (right == null)
            {
                node.parent = this;
                right = node;
                return true;
            }
            
            if (!right.inClassAdd(node))
            {
                if (getPrescedence() < node.getPrescedence())
                {
                    node.parent = this;
                    node.left = right;
                    node.left.parent = node;
                    right = node;
                    return true;
                }
                
                if (parent == null)
                {
                    parent = node;
                    node.left = this;
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