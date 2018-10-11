using System;
using System.Windows.Forms;

namespace Spreadsheet
{
        
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Log.Log.getLog().addOutputStream(Console.Out);
            
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