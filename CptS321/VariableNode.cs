using System;
using SpreadsheetEngine;

namespace CptS321
{
    public class VariableNode : NumericalValueNode
    {
        private Value value;
        private string name;

        public VariableNode(string variableName)
        {
            name = variableName;
        }

        public override void SetVar(string varName, double value)
        {
            if (name.Equals(varName))
            {
                if (this.value != null)
                {
                    this.value.setValue(value);
                }
                else
                {
                    this.value = new Value(value);
                }
            }
        }
        
        public override double Eval()
        {
            if (value == null)
            {
                throw new Exception(String.Format("Var {0} was not set prior to evaluation", name));
            }
            return value.getValue();
        }
    }
}