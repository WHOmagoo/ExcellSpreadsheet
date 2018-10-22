using System.Security.AccessControl;
using SpreadsheetEngine;

namespace CptS321
{
    public class AdditionOperator : BinaryOperator
    {
        
        public override double Eval()
        {
            return left.Eval() + right.Eval();
        }
    }
}