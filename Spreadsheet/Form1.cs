using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Xml;
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

        private AboutForm form = new AboutForm();
        
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
            _spreadsheet = new SpreadsheetEngine.Spreadsheet(5,5);

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
            
            var About = new ToolStripMenuItem();
            About.Name = "About";
            About.Text = "About";
            About.Click += OnAbout;

            menuStrip.Items.Add(About);
            
            Controls.Add(menuStrip);
        }

        private void OnAbout(object sender, EventArgs e)
        {
            form.Hide();
            form.Show();
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

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".\\";
                openFileDialog.Filter = "Spreadsheet files (*.spreadsheet)|*.spreadsheet|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader stream = new StreamReader(fileStream))
                    {
                        XmlReader reader = XmlReader.Create(stream);
                        _spreadsheet = new SpreadsheetEngine.Spreadsheet();
                        _spreadsheet.ReadXml(reader);
                    }
                }

                InitializeEquationText();
                InitializeSpreadsheetView();
            }
        }

        private void OnSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Spreadsheet file|*.spreadsheet";
            saveFileDialog1.Title = "Save a Spreadsheet File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {

                using (StreamWriter stream = new StreamWriter(saveFileDialog1.OpenFile()))
                {
                    

                    Console.WriteLine("Writing object");
                    using (XmlWriter writer = XmlWriter.Create(stream))
                    {
                        writer.WriteStartDocument();
                        _spreadsheet.WriteXml(writer);
                        writer.WriteEndDocument();
                        Console.WriteLine("Saving as");
                    }
                }
            }

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