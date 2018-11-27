using System;

namespace CptS321
{
    [Serializable]
    public class NegativeOperator : UnaryOperator
    {   
        public override void add(ExpNode node)
        {
            if (right == null)
            {
                setChild(node);
            }else if (parent == null)
            {
                setParent(node);
            } else if (parent is NegativeOperator)
            {
                parent.setChild(null);
            }
            else{
                parent.add(node);
            }
        }
        
        public override double Eval()
        {
            return -1 * right.Eval();
        }

        public NegativeOperator() : base(1)
        {
        }
    }
}