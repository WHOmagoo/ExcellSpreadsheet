namespace CptS321
{
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
    }
}