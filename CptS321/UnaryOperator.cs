using System;

namespace CptS321
{
    public abstract class UnaryOperator : Operator
    {
        protected UnaryOperator(int prescedence) : base(prescedence)
        {
        }
    }
}
