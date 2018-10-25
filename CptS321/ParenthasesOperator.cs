using System;

namespace CptS321
{
    public class ParenthasesOperator : UnaryOperator
    {
        private bool isClosingParenthesis; //True for open false for right
        
        public ParenthasesOperator(bool isClosingParenthesis) : base(Int32.MaxValue)
        {
            this.isClosingParenthesis = isClosingParenthesis;
        }

        public override double Eval()
        {
            throw new System.NotImplementedException();
        }

        protected internal override bool inClassAdd(ExpNode node)
        {
            ParenthasesOperator adding = node as ParenthasesOperator;
            if (adding != null && isClosingParenthesis)
            {
                parent.right = right;
                right.parent = parent;
                return true;
            }

            return right.inClassAdd(node);
        }
    }
}