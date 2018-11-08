namespace CptS321
{
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