using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Xml.Serialization.Configuration;
using CptS321.Properties;
using SpreadsheetEngine;

namespace CptS321
{
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
        
    }
}