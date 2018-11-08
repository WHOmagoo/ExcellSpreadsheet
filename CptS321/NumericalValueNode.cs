namespace CptS321
{
    public class NumericalValueNode : ExpNode
    {
        private Value value;
        
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