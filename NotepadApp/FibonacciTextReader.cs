using System;
using System.IO;
using System.Net;
using System.Numerics;
using System.Xml.XPath;

namespace NotepadApp
{
    public class FibonacciTextReader : TextReader
    {
        private int availableLines;
        private int curLineNumber = 0;
        private BigInteger prevNum = 0;
        private BigInteger prevPrevNum = 0;
        
        public FibonacciTextReader(int availableLines)
        {
            this.availableLines = availableLines;
        }

        public override string ReadLine()
        {
            if (curLineNumber < availableLines)
            {
                BigInteger result;

                switch (curLineNumber)
                {
                        case 0 :
                            result = 0;
                            break;
                        case 1 :
                            result = 1;
                            break;
                        case 2 :
                            result = 1;
                            prevPrevNum = 1;
                            prevNum = 1;
                            break;
                        default:
                            result = prevNum + prevPrevNum;
                            prevPrevNum = prevNum;
                            prevNum = result;
                            break;
                }
                
                curLineNumber++;

                return (curLineNumber) + ": " + result + Environment.NewLine;
            }
            else
            {
                return null;
            }
        }
        
        public override string ReadToEnd()
        {
            String result = "";
            String nextLine = ReadLine();

            while (nextLine != null)
            {
                result += nextLine;
                nextLine = ReadLine();
            }

            return result;
        }
        
        
    }
}