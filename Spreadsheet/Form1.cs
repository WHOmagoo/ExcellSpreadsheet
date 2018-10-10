using System;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using System.Windows.Forms;
using Spreadsheet = SpreadsheetEngine.Spreadsheet;

namespace Spreadsheet
{
    public partial class Form1 : Form
    {
        private SpreadsheetView _spreadsheetView;
        private SpreadsheetEngine.Spreadsheet _spreadsheet;
        
        public Form1(String tittle = "")
        {
            Text = tittle;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Size = new Size(1920, 1080);
            
            _spreadsheet = new SpreadsheetEngine.Spreadsheet(50,50);
            
            _spreadsheetView = new SpreadsheetView(_spreadsheet);
            
            Controls.Add(_spreadsheetView);
        }
    }
}