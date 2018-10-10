using System;
using System.ComponentModel;
using System.Windows.Forms;
using SpreadsheetEngine;

namespace Spreadsheet
{
    public class SpreadsheetView : DataGridView
    {

        private SpreadsheetEngine.Spreadsheet _spreadsheet;
        public SpreadsheetView(SpreadsheetEngine.Spreadsheet s)
        {
            int rows = s.RowCount;
            int cols = s.ColCount;
            ColumnCount = cols;
            RowCount = rows + 1;

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;

            ScrollBars = ScrollBars.Both;

            _spreadsheet = s;

            Dock = DockStyle.Fill;


            DoubleBuffered = true;


            CellValueChanged += SpreadsheetView_CellValueChanged;
            for (int i = 0; i < cols; i++)
            {
                Columns[i].Name = HeaderConverter.Convert(i + 1);
            }

            for (int i = 0; i < rows; i++)
            {
                Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void SpreadsheetView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SpreadsheetView sv = sender as SpreadsheetView;

            if (sv != null)
            {
                Cell c = sv._spreadsheet.getCell(e.RowIndex, e.ColumnIndex);
                
                if (c != null)
                {
                    c.setText(Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                else
                {
                    Console.WriteLine("Trying to access invalid cell " + e.RowIndex + ", " + e.ColumnIndex);
                }
            }
            else
            {
                Console.WriteLine("sv was not a spreadsheetview");
            }
        }
    }
}