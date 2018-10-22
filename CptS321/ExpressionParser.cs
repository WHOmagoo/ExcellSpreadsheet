//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Text.RegularExpressions;
//
//namespace CptS321
//{
//    public class ExpressionParser
//    {
//        private static string operatorsRegexString = @"[\/+\-\*]";
//        private static string unaryOperatorsRegexString = @"\-";
//        private static string variableNamematch = @"\w+"; 
//        
//        public static MatchCollection parse(string message)
//        {
//            String regexString = @"";
//            Regex regex = new Regex(regexString);
//
//            MatchCollection result = regex.Matches(message);
//
//            bool prevIsOperator = false;
//            
//            foreach (Match match in result)
//            {
//                GroupCollection cg = match.Groups;
//
//                bool isOperator = false;
//                
//                foreach(Group g in cg)
//                {
//                    if (g.Index == 2)
//                    {
//                        isOperator = true;
//                    }
//                }
//
//                
//                //capture group 2 which should be unary negative
//                if (isOperator)
//                {
//                    ExpNode op = OperatorFactory.makeBinaryOperator(match.Value);
//                }
//            }
//
//            return result;
//
//        }
//
//        public static bool isValidlyFormated(string message)
//        {
//            String regexString = String.Format("{0}");
//            Regex regex = new Regex();
//        }
//        
//        public static bool isOperator(string s)
//        {
//            Regex regex = new Regex(operatorsRegexString);
//        }
//    }
//}