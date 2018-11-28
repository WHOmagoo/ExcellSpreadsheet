using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CptS321.Annotations;

namespace CptS321
{
    public class ExpTree : INotifyPropertyChanged, IXmlSerializable
    {
        private ExpNode root;
        private string expression;
        
        private static string operatorsRegexString = @"(?<operator>[\/+\-\*])";
        private static string unaryOperatorsRegexString = @"(?<unaryOperator>[\-\(\)])";
        private static string variableNamematch = @"(?<variableName>\w+)";
        private static string numberMatch = @"(?<number>(\d+\.?\d*|\d*\.?\d+))";

        private Dictionary<string, Value> _variableNodes = new Dictionary<string, Value>();

        public ExpTree()
        {
            
        }
        
        public ExpTree(String expression)
        {
            InitializeExpTree(expression);
        }

        private void InitializeExpTree(string expression)
        {
            this.expression = expression;
            String regexString = String.Format(@"({0}|{1}|{2}|{3})", operatorsRegexString, unaryOperatorsRegexString, numberMatch, variableNamematch);
            Regex regex = new Regex(regexString);

            Match match = Regex.Match(expression, regexString);
            
            List<ExpNode> expressions = new List<ExpNode>();
            
            bool prevIsOperator = true;
            int unclosedParenthesesCount = 0;
            
            while (match.Success)   
            {

                bool isOperator = match.Groups["operator"].Success;
                bool isUnaryOperator = match.Groups["unaryOperator"].Success;
                bool isNumberOrVariable = match.Groups["number"].Success || match.Groups["variableName"].Success;

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
                else if (isNumberOrVariable)
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
                else
                {
                    string message = String.Format("Malformed input\nThe token {0} didn't match anything equation form", match.Value);
                    throw new Exception(message);
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
            PropertyChanged.Invoke(node, new PropertyChangedEventArgs("ValueNode"));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            string newExpression = reader.GetAttribute("Expression");
            InitializeExpTree(newExpression);
            reader.ReadStartElement("ExpressionTree");
            string sQuantity = reader.GetAttribute("Quantity");
            reader.ReadStartElement("Variables");
            int count = -1;
            
            try
            {
                count = Int32.Parse(sQuantity);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception converting quantity to an int");
            }

            for (int i = 0; i < count; i++)
            {
                string key = reader.GetAttribute("Key");
                string sValue = reader.GetAttribute("Value");
                reader.ReadStartElement("Variable");

                if (_variableNodes.ContainsKey(key))
                {
                    try
                    {
                        _variableNodes[key].setValue(Double.Parse(sValue));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error parsing the double");
                    }
                }
            }

            if (count > 0)
            {
                reader.ReadEndElement();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ExpressionTree");
            writer.WriteAttributeString("Expression", expression);

            writer.WriteStartElement("Variables");
            writer.WriteAttributeString("Quantity", _variableNodes.Count.ToString());
            
            foreach (string key in _variableNodes.Keys)
            {
                writer.WriteStartElement("Variable");
                writer.WriteAttributeString("Key", key);
                writer.WriteAttributeString("Value", _variableNodes[key].getValue().ToString(CultureInfo.InvariantCulture));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            
            writer.WriteEndElement();
        }
    }
}