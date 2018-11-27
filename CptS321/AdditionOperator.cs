using System;

namespace CptS321
{
    [Serializable]
    public class AdditionOperator : BinaryOperator
    {
        
        public override double Eval()
        {
            return left.Eval() + right.Eval();
        }

        public AdditionOperator() : base(1)
        {
        }
    }
}