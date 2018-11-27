using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Binary;
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
        private TableLayoutPanel _layout;

        public Form1(String tittle = "")
        {
            Text = tittle;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _layout = new TableLayoutPanel();
            _layout.ColumnCount = 1;
            _layout.RowCount = 2;

            _layout.Dock = DockStyle.Fill;
            
            Size = new Size(1920, 1080);
            _spreadsheet = new SpreadsheetEngine.Spreadsheet(50,50);

            InitializeEquationText();


            InitializeSpreadsheetView();

            Controls.Add(_layout);
            
            var menuStrip = new MenuStrip();
            menuStrip.Location = new Point(0,0);
            menuStrip.Name = "MenuStrip";
            var File = new ToolStripMenuItem();
            menuStrip.Items.Add(File);
            File.Name = "File";
            File.Text = "File";
            
            var SaveAs = new ToolStripMenuItem();
            File.DropDownItems.Add(SaveAs);
            SaveAs.Name = "SaveAs";
            SaveAs.Text = "Save As";
            SaveAs.Click += OnSaveAs;
            
            var Open = new ToolStripMenuItem();
            File.DropDownItems.Add(Open);
            Open.Name = "Open";
            Open.Text = "Open";
            Open.Click += OnOpen;
            
            Controls.Add(menuStrip);
        }

        private void InitializeEquationText()
        {
            
            _layout.Controls.Remove(equationText);
            equationText = new TextBox();

            equationText.AutoSize = true;
            equationText.Dock = DockStyle.Fill;
            equationText.TextChanged += OnEquationChange;
            _layout.Controls.Add(equationText);
        }

        private void InitializeSpreadsheetView()
        {
            _layout.Controls.Remove(_spreadsheetView);
            _spreadsheetView = new SpreadsheetView(_spreadsheet);

            _spreadsheetView.Dock = DockStyle.Fill;

            _spreadsheetView.CellEnter += onCellSelected;
            _layout.Controls.Add(_spreadsheetView);
        }

        private void OnOpen(object sender, EventArgs e)
        {
            
            FileStream fs = File.OpenRead("./output.test");
            BinaryFormatter serializer = new BinaryFormatter();
            _spreadsheet = (SpreadsheetEngine.Spreadsheet) serializer.Deserialize(fs);
            fs.Close();
            fs.Dispose();

            InitializeEquationText();
            InitializeSpreadsheetView();
            
        }

        private void OnSaveAs(object sender, EventArgs e)
        {
            Console.WriteLine("Writing object");
            FileStream fs = File.OpenWrite("./output.test");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(fs, _spreadsheet);

            fs.Flush();
            fs.Close();
            fs.Dispose();
            Console.WriteLine("Saving as");
            
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