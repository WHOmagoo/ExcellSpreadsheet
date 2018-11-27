using System;

namespace CptS321
{
    [Serializable]
    public class SubtractionOperator  : BinaryOperator
    {

        public override double Eval()
        {
            return left.Eval() - right.Eval();
        }

        public SubtractionOperator() : base(1)
        {
        }
    }
}