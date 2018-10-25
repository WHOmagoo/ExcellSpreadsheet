using SpreadsheetEngine;

namespace CptS321
{
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