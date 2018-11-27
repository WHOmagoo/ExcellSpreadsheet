using System;
using System.Runtime.Serialization;

namespace CptS321 {
    
    [Serializable]
    public abstract class ExpNode
    {
        protected string varName;
        
        private int prescedence;

        protected ExpNode()
        {
            prescedence = 0;
            left = null;
            right = null;
            parent = null;
        }

        public ExpNode left {private set; get;}
        public ExpNode right {private set; get;}
        public ExpNode parent {private set; get;}

//        public ExpNode left
//        {
//            get { return _left; }
//            private set { _left = value; }
//        }
//        
//        public ExpNode right
//        {
//            get { return _right; }
//            private set { _right = value; }
//        }
//        
//        public ExpNode parent
//        {
//            get { return _parent; }
//            private set { _parent = value; }
//        }

        public virtual void SetVar(string varName, double varValue)
        {
            if (left != null)
            {
                left.SetVar(varName, varValue);
            }

            if (right != null)
            {

                right.SetVar(varName, varValue);
            }
        }

        public int getPrescedence()
        {
            return prescedence;
        }

        public void setPresedence(int newPrescedence)
        {
            prescedence = newPrescedence;
        }

        public ExpNode findRoot()
        {
            return parent == null ? this : parent.findRoot();
        }

        public abstract void add(ExpNode node); 

        protected internal void setParent(ExpNode newParent)
        {
            if (parent != null)
            {
                parent.right = newParent;
            }

            newParent.parent = parent;
            newParent.left = this;
            parent = newParent;


        }

        protected internal void setChild(ExpNode newChild)
        {
            newChild.parent = this;
            if (right != null)
            {
                left = right;
            }

            right = newChild;
        }

        public abstract double Eval();
        
        //Deserialization constructor.
        public ExpNode(SerializationInfo info, StreamingContext ctxt)
        {
            Console.WriteLine("Deserialize Data");
            //Get the values from info and assign them to the appropriate properties
            left = (ExpNode)info.GetValue("Left", typeof(ExpNode));
            left.setParent(this);
            right = (ExpNode)info.GetValue("Rightzz", typeof(ExpNode));
            right.setParent(this);
        }
        
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            Console.WriteLine("Serializng data");
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("Left", left);
            info.AddValue("Rightzz", right);
        }
        
    }
}