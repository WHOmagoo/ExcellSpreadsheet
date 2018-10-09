using System;
using SpreadsheetEngine;

namespace Spreadsheet
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Class1 test = new Class1(5);

            Console.WriteLine(test.getVal());
            
        }
    }
}