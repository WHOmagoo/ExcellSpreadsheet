using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using CptS321.Annotations;

namespace CptS321
{
    [Serializable]
    public class ExpTree : INotifyPropertyChanged
    {
        private ExpNode root;
        private string expression;
        
        private static string operatorsRegexString = @"(?<operator>[\/+\-\*])";
        private static string unaryOperatorsRegexString = @"(?<unaryOperator>[\-\(\)])";
        private static string variableNamematch = @"(?<variableName>\w+)";

        private Dictionary<string, Value> _variableNodes = new Dictionary<string, Value>();
        
        public ExpTree(String expression)
        {
            this.expression = expression;
            String regexString = String.Format(@"({0}|{1}|{2})", variableNamematch, operatorsRegexString, unaryOperatorsRegexString);
            Regex regex = new Regex(regexString);

            Match match = Regex.Match(expression, regexString);
            
            List<ExpNode> expressions = new List<ExpNode>();
            
            bool prevIsOperator = true;
            int unclosedParenthesesCount = 0;
            
            while (match.Success)   
            {

                bool isOperator = match.Groups["operator"].Success;
                bool isUnaryOperator = match.Groups["unaryOperator"].Success;

                if (isOperator)
                {
                    ExpNode op = OperatorFactory.makeBinaryOperator(match.Value);
                    if (!prevIsOperator)
                    {
                        expressions.Add(op);
                    }
                    else if (op is SubtractionOperator)
                    {
                        expressions.Add(new NegativeOperator());
                    }
                    else
                    {
                        throw new Exception("Malformed input, looking for operator");
                    }
                } else if (isUnaryOperator)
                {
                    ExpNode op = OperatorFactory.makeUnaryOperator(match.Value);
                    ParenthesesOperator pOP = op as ParenthesesOperator;
                    if (pOP != null)
                    {
                        if (pOP.isClosingParenthesis())
                        {
                            isOperator = false;
                            unclosedParenthesesCount--;
                        }
                        else
                        {
                            isOperator = true;
                            unclosedParenthesesCount++;
                        }

                        if (unclosedParenthesesCount < 0)
                        {
                            throw new Exception("Too many closing parenthases");
                        }
                    }
                    expressions.Add(op);

                }
                else
                {
                    if (prevIsOperator)
                    {
                        try
                        {
                            double d = Double.Parse(match.Value);
                            NumericalValueNode val = new NumericalValueNode(new Value(d));
                            expressions.Add(val);
                        }
                        catch (Exception e)
                        {
                            if (!_variableNodes.ContainsKey(match.Value))
                            {
                                Value val = new Value(0);
                                _variableNodes.Add(match.Value, val);
                            }

                            expressions.Add(new VariableNode(_variableNodes[match.Value]));
                        }
                    }
                    else
                    {
                        throw new Exception("Malformed input");
                    }
                }

                prevIsOperator = isOperator;
                match = match.NextMatch();
            }
            
            if (expressions.Count == 0)
            {
                throw new Exception("Nothing entered");
            }

            if (unclosedParenthesesCount != 0)
            {
                throw new Exception("Too many open parentheses entered");
            }


            ExpNode prev = null;
            
            foreach (ExpNode node in expressions)
            {
                if (prev == null)
                {
                    prev = node;
                }
                else
                {
                    prev.add(node);
                    prev = node;
                }
            }

            root = prev.findRoot();
        }
        
        public void SetVar(string varName, double varValue)
        {
            if (_variableNodes.ContainsKey(varName))
            {
                _variableNodes[varName].setValue(varValue);
            }
        }

        public List<string> getVariableNames()
        {
            return new List<string>(_variableNodes.Keys);
        }

        public double Eval()
        {
            return root.Eval();
        }

        public override string ToString()
        {
            return expression;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void onVariableNodeChanged(VariableNode node)
        {
            PropertyChanged?.Invoke(node, new PropertyChangedEventArgs("ValueNode"));
        }
        
        //Deserialization constructor.
        public ExpTree(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            expression = (string)info.GetValue("Expression", typeof(string));
            root = (ExpNode)info.GetValue("EmployeeName", typeof(ExpNode));
        }
        
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("Expression", expression);
            info.AddValue("rootnode", root);
        }
        
        
    }
}