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

        protected internal ExpNode left;
        protected internal ExpNode right;
        protected internal ExpNode parent;

        protected ExpNode()
        {
        }

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

        public abstract double Eval();
        
    }
}