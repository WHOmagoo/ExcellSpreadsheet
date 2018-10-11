using System;
using System.ComponentModel;
using Log;
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

            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;

            DoubleBuffered = true;
            
            for (int i = 0; i < cols; i++)
            {
                Columns[i].Name = HeaderConverter.Convert(i);
            }

            for (int i = 0; i < rows; i++)
            {
                Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

            
            CellValueChanged += SpreadsheetView_CellValueChanged;
            _spreadsheet.PropertyChanged += SpreadsheetView_SpreadsheetCellUpdated;
            
        }

        private void SpreadsheetView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            SpreadsheetView sv = sender as SpreadsheetView;

            if (sv != null)
            {
                Log.Log.getLog().logMessage("Accessing cell ({0},{1}) in SpreadsheetView", e.RowIndex, e.ColumnIndex);
                Cell c = sv._spreadsheet.getCell(e.RowIndex, e.ColumnIndex);
                
                if (c != null)
                {
                    c.setText(Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                else
                {
                    Log.Log.getLog().logMessage("Trying to access invalid cell ({0},{1}) ", e.RowIndex, e.ColumnIndex);
                }
            }
            else
            {
                Log.Log.getLog().logMessage("sv was not a spreadsheetview in SpreadsheetView");
            }
        }

        private void SpreadsheetView_SpreadsheetCellUpdated(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;

            if (cell != null)
            {
                Log.Log.getLog().logLine("Updating a cells value in gui");
                this[cell.ColIndex, cell.RowIndex].Value = cell.getValue();
            }
            else
            {
                Log.Log.getLog().logLine("Trying to update a non cell item");
            }
        }
    }
}