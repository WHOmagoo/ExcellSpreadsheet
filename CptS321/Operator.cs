namespace CptS321
{
    public abstract class Operator : ExpNode
    {
        protected Operator(int prescedence)
        {
            setPresedence(prescedence);
        }
    }
}