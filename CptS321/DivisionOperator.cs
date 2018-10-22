using SpreadsheetEngine;

namespace CptS321
{
    public class DivisionOperator : BinaryOperator
    {
        public override double Eval()
        {
            return left.Eval() / right.Eval();
        }
    }
}