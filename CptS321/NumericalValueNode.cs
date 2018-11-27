using System;
using System.Runtime.Serialization;

namespace CptS321
{
    [Serializable]
    public class NumericalValueNode : ExpNode
    {
        protected Value value;
        
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
        
        //Deserialization constructor.
        public NumericalValueNode(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            value = (Value) info.GetValue("Value", typeof(Value));
        }
        
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("Value", value);
        }
    }
}