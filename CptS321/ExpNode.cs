using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CptS321 {
    
    public abstract class ExpNode
    {   
        private int prescedence;

        protected ExpNode()
        {
            prescedence = 0;
            left = null;
            right = null;
            parent = null;
        }

        protected ExpNode left;
        protected ExpNode right;
        protected ExpNode parent;

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
        public XmlSchema GetSchema()
        {
            return null;
        }
    }
}