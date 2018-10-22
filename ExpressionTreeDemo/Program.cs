using System;
using CptS321;

namespace ExpressionTreeDemo
{
    internal class Program
    {
        private static Action[] functions = {errorInInput, newExpression, setVariableName, EvaluateTree};
        private static string[] names = {"Error in input", "New Expression", "Set variable name", "Evaluate tree"};

        private static ExpTree expression;
        
        public static void Main(string[] args)
        {
            
            newExpression();
            int response = getResponse();

            while (response != functions.Length)
            {
                functions[response]();
                response = getResponse();
            }
        }

        private static int getResponse()
        {
            printOptions();
            string line = Console.ReadLine();
            try
            {
                int result = Int32.Parse(line);
                
                if (result <= 0 || result > functions.Length)
                {
                    return 0;
                }

                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private static void errorInInput()
        {
            Console.WriteLine("There was an error with your input");
        }
        
        private static void newExpression()
        {
            Console.Write("Please enter a new expression: ");

            string newExpression = Console.ReadLine();
            
            expression = new ExpTree(newExpression);
        }

        private static void setVariableName()
        {
            if (expression != null)
            {
                Console.Write("Enter variable name: ");
                string varName = Console.ReadLine();
                
                Console.Write("Enter new value: ");
                try
                {
                    string varValue = Console.ReadLine();
                    if (varValue != null)
                    {
                        double varVal = Double.Parse(varValue);
                        expression.SetVar(varName, varVal);
                    }
                    else
                    {
                        throw new Exception();
                    }                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("There was something wrong with the number you entered");
                }
            }
            
        }

        private static void EvaluateTree()
        {
            try
            {
                Console.WriteLine("Result = {0}", expression.Eval());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        private static void printOptions()
        {
            Console.WriteLine("Menu (expression = {0})", expression);
            for (int i = 1; i < names.Length; i++)
            {
                Console.WriteLine("  {0} = {1}", i, names[i]);
            }
            
            Console.Write("  {0} = Quit", functions.Length);
        }
        
    }
}