using System;
using System.Runtime.Serialization;

namespace CptS321
{
    [Serializable]
    public class VariableNode : NumericalValueNode
    {
        private string name;

        public VariableNode(Value val)
        {
            value = val;
        }

        public VariableNode(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            name = info.GetString("Name");
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("Name", name);
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