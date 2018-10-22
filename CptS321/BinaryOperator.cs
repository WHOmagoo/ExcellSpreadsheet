using System;

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
                if (parent != null)
                {
                    if (parent.left == this)
                    {
                        parent.left = node;
                    } else if (parent.right == this)
                    {
                        parent.right = node;
                    }
                }
                
                node.parent = parent;
                parent = node;
                node.left = this;
            }

            return true;
        }
        
    }
}