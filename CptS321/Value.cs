using System;
using System.Runtime.Serialization;

namespace CptS321
{
    [Serializable]
    public class Value
    {
        private double value;
        
        public Value(double value)
        {
            this.value = value;
        }

        public void setValue(double val)
        {
            value = val;
        }

        public double getValue()
        {
            return value;
        }
        
        //Deserialization constructor.
        public Value(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            value = (double) info.GetValue("Value", typeof(double));
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