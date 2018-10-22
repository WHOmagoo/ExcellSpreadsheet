using System;
using CptS321;
using SpreadsheetEngine;

namespace SpreadsheetEngine
{
    public class NumericalValueNode : ExpNode
    {
        private Value value;
        
        public NumericalValueNode(Value value = null)
        {
            this.value = value;
        }

        protected internal override bool inClassAdd(ExpNode node)
        {
            return false;
        }

        public override double Eval()
        {
            return value.getValue();
        }
    }
}