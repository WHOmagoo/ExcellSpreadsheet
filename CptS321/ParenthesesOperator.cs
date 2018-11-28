using System;
using System.Security.Policy;

namespace CptS321
{
    public class ParenthesesOperator : UnaryOperator
    {
        private readonly bool IsClosingParenthesis;

        public ParenthesesOperator(bool isClosingParenthesis) : base(Int32.MinValue)
        {
            IsClosingParenthesis = isClosingParenthesis;
        }

        public bool isClosingParenthesis()
        {
            return IsClosingParenthesis;
        }

        public override void add(ExpNode node)
        {
            if (!IsClosingParenthesis)
            {
                if (left == null)
                {
                    ParenthesesOperator nodeAsParentheses = node as ParenthesesOperator;
                    if (nodeAsParentheses != null)
                    {
                        if (nodeAsParentheses.IsClosingParenthesis)
                        {
                            setChild(node);
                        }
                        else
                        {
                            setChild(nodeAsParentheses);
                        }
                    }
                    else
                    {
                        if (right == null)
                        {
                            setChild(node);
                        }
                        else
                        {
                            right.setParent(node);
                        }
                    }
                }
                else
                {
                    if (parent != null)
                    {
                        parent.add(node);
                    }
                    else
                    {
                        setParent(node);
                    }
                }
            }
            else
            {
                parent.add(node);
            }
        }

        public override double Eval()
        {
            return left.Eval();
        }
    }
}