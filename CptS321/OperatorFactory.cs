using System;
using System.Collections.Generic;

namespace CptS321
{
    public class OperatorFactory
    {
        private static Dictionary<string, Type> xmlSymbolToType = new Dictionary<string, Type>()
        {
            {"+", typeof(AdditionOperator)},
            {"-", typeof(SubtractionOperator)},
            {"/", typeof(DivisionOperator)},
            {"*", typeof(MultiplicationOperator)},
            {"(", typeof(ParenthesesOperator)},
            {")", typeof(ParenthesesOperator)}
        };

        private static Dictionary<Type, string> typeToXMLSymbol = reverse(xmlSymbolToType);

        private static Dictionary<A,B> reverse<A,B>(Dictionary<B,A> dic) 
        {
            Dictionary<A,B> reversed = new Dictionary<A, B>(dic.Count);
            
            foreach (B b in dic.Keys)
            {
                reversed.Add(dic[b], b);
            }

            return reversed;
        }
        
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