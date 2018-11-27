using System;

namespace CptS321
{
    [Serializable]
    public abstract class UnaryOperator : Operator
    {
        protected UnaryOperator(int prescedence) : base(prescedence)
        {
        }
    }
}
