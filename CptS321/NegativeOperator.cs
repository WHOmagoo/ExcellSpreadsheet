namespace CptS321.Properties
{
    public class NegativeOperator : UnaryOperator
    {   
        public override double Eval()
        {
            return -1 * right.Eval();
        }

        public NegativeOperator() : base(1)
        {
        }
    }
}