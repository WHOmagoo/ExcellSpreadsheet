using System;

namespace CptS321
{
    [Serializable]
    public class MultiplicationOperator : BinaryOperator
    {

        public override double Eval()
        {
            return left.Eval() * right.Eval();
        }

        public MultiplicationOperator() : base(2)
        {
        }
    }
}