using System;

namespace CptS321
{
    [Serializable]
    public abstract class Operator : ExpNode
    {
        protected Operator(int prescedence)
        {
            setPresedence(prescedence);
        }
    }
}