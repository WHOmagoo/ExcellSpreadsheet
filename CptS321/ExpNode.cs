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
        }
        
        public ExpNode left { get;    private set; }
        public ExpNode right { get;   private set; }
        public ExpNode parent { get;  private set; }

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

        public ExpNode add(ExpNode node)
        {
            if (!inClassAdd(node))
            {
                node.parent = parent;
                node.left = this;
                parent = node;
            }
            

            ExpNode root = this;
            while (root.parent != null)
            {
                root = root.parent;
            }

            return root;
        }
        
        protected internal virtual bool inClassAdd(ExpNode node)
        {
            if (right == null)
            {
                right = node;
                node.parent = this;
                return true;
            }
            
            if (right is BinaryOperator)
            {
                if (!right.inClassAdd(node))
                {
                    
                }

                return true;
            } else 
            {
                parent = node;
                node.left = this;
                return true;
            }
        }

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
            right = newChild;
        }

        public abstract double Eval();
        
    }
}