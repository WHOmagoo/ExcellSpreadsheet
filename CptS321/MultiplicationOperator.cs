using SpreadsheetEngine;

namespace CptS321
{
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