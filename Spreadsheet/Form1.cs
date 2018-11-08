using System;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using System.Windows.Forms;
using SpreadsheetEngine;
using Spreadsheet = SpreadsheetEngine.Spreadsheet;

namespace Spreadsheet
{
    public partial class Form1 : Form
    {
        private SpreadsheetView _spreadsheetView;
        private SpreadsheetEngine.Spreadsheet _spreadsheet;
        private TextBox equationText;

        private int curRow;

        private int curCol;

        private bool equationChangedByProgram = false;
        
        public Form1(String tittle = "")
        {
            Text = tittle;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.ColumnCount = 1;
            layout.RowCount = 2;

            layout.Dock = DockStyle.Fill;
            
            Size = new Size(1920, 1080);

            equationText = new TextBox();

            equationText.AutoSize = true;
            equationText.Dock = DockStyle.Fill;
            
            _spreadsheet = new SpreadsheetEngine.Spreadsheet(50,50);
            
            _spreadsheetView = new SpreadsheetView(_spreadsheet);

            _spreadsheetView.Dock = DockStyle.Fill;

            _spreadsheetView.CellEnter += onCellSelected;
            
            layout.Controls.Add(equationText);
            layout.Controls.Add(_spreadsheetView);

            equationText.TextChanged += OnEquationChange;
            
            Controls.Add(layout);
        }

        private void onCellSelected(object sender, DataGridViewCellEventArgs e)
        {
            Cell curCell = _spreadsheet.getCell(e.ColumnIndex, e.RowIndex);
            if (!equationText.Text.Equals(curCell.getText()))
            {
                equationChangedByProgram = true;
                equationText.Text = _spreadsheet.getCell(e.ColumnIndex, e.RowIndex).getText();
            }

            curCol = e.ColumnIndex;
            curRow = e.RowIndex;
        }
        
        
        private void OnEquationChange(object sender, EventArgs args)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null && equationChangedByProgram == false)
            {
                Cell curCell = _spreadsheet.getCell(curCol, curRow);
                curCell.setText(textBox.Text);
            }
            else
            {
                equationChangedByProgram = false;
            }
        }
    }
}