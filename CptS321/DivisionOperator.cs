using System;

namespace CptS321
{
    [Serializable]

    public class DivisionOperator : BinaryOperator
    {
        public override double Eval()
        {
            return left.Eval() / right.Eval();
        }

        public DivisionOperator() : base(2)
        {
        }
    }
}