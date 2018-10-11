using System;
using System.Windows.Forms;
using SpreadsheetEngine;

namespace Spreadsheet
{
        
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Log.Log.getLog().addOutputStream(Console.Out);

            for (int i = 0; i < 28 + 0 * Math.Pow(2,14); i++)
            {
                Console.WriteLine(i + "=" +HeaderConverter.Convert(i));
            }

            int n = 701;
            Console.WriteLine((n / 26 / 25) % 26);
            Console.WriteLine((n / 26) % 26);
            Console.WriteLine(n % 26);

            Application.EnableVisualStyles();
            Application.Run(new Form1());

        }
    }

        
//        public static void Main(string[] args)
//        {
//            SpreadsheetEngine.Spreadsheet s = new SpreadsheetEngine.Spreadsheet();
//
//            Cell cell = s.getCell(5, 5);
//            cell.setText("Bobbert mann!");
//        }
   
}