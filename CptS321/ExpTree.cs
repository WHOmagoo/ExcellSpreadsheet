using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CptS321.Properties;
using SpreadsheetEngine;

namespace CptS321
{
    public class ExpTree
    {
        private ExpNode root;
        private string expression;
        
        private static string operatorsRegexString = @"(?<operator>[\/+\-\*])";
        private static string unaryOperatorsRegexString = @"(?<unaryOperator>[\-\(\)])";
        private static string variableNamematch = @"(?<variableName>\w+)";
        
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
                            ExpNode expNode = new VariableNode(match.Value);
                            expressions.Add(expNode);
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
            if (varName != null)
            {
                root.SetVar(varName, varValue);
            }
        }

        public double Eval()
        {
            return root.Eval();
        }

        public override string ToString()
        {
            return expression;
        }
    }
}