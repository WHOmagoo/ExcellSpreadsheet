using System;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CptS321
{
    public class VariableNode : NumericalValueNode
    {
        private string name;

        public VariableNode()
        {
            
        }
        
        public VariableNode(Value val)
        {
            value = val;
        }
        
        public override double Eval()
        {
            if (value == null)
            {
                throw new Exception(String.Format("Var {0} was not set prior to evaluation", name));
            }
            return value.getValue();
        }

        public string getName()
        {
            return name;
        }
    }
}