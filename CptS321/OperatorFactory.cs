using CptS321.Properties;

namespace CptS321
{
    public class OperatorFactory
    {
        public static BinaryOperator makeBinaryOperator(string symbol)
        {
            if (symbol.Equals("+"))
            {
                return new AdditionOperator();
            } else if (symbol.Equals("-"))
            {
                return new SubtractionOperator();
            } else if (symbol.Equals("/"))
            {
                return new DivisionOperator();
            } else if (symbol.Equals("*"))
            {
                return new MultiplicationOperator();
            }

            return null;
        }
        
        public static UnaryOperator makeUnaryOperator(string symbol)
        {
            if (symbol.Equals("-"))
            {
                return new NegativeOperator();
            }else if (symbol.Equals("("))
            {
                return new ParenthesesOperator(false);
            }else if (symbol.Equals(")"))
            {
                return new ParenthesesOperator(true);
            }

            return null;
        }
            
    }
}