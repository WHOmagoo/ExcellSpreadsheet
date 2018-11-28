using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CptS321
{
    public class NumericalValueNode : ExpNode
    {
        protected Value value;

        public NumericalValueNode()
        {
            
        }
        
        public NumericalValueNode(Value value = null)
        {
            this.value = value;
        }

        public override void add(ExpNode node)
        {
            if (parent == null)
            {
                setParent(node);
            }
            else
            {
                parent.add(node);
            }
        }

        public override double Eval()
        {
            return value.getValue();
        }
    }
}