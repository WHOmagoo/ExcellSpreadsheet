using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CptS321
{
    public class Value
    {
        private double value;

        public Value()
        {
        }
        
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
    }
}