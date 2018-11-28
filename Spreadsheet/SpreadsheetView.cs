using System;
using System.ComponentModel;
using Log;
using System.Windows.Forms;
using SpreadsheetEngine;

namespace Spreadsheet
{
    public class SpreadsheetView : DataGridView
    {

        private volatile bool userUpdatedCell = false;

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

            for (int row = 0; row < _spreadsheet.RowCount; row++)
            {
                for (int col = 0; col < _spreadsheet.ColCount; col++)
                {
                    Rows[row].Cells[col].Value = _spreadsheet.getCell(col, row).getValue();
                }
            }
            
            CellValueChanged += SpreadsheetView_CellValueChanged;
//            CellLeave += SpreadsheetView_CellLeft;
            CellEndEdit += SpreadsheetView_CellLeft;
            _spreadsheet.PropertyChanged += SpreadsheetView_SpreadsheetCellUpdated;
            
        }

        private void SpreadsheetView_CellLeft(object sender, DataGridViewCellEventArgs e)
        {
            userUpdatedCell = true;
        }

        private void SpreadsheetView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            SpreadsheetView sv = sender as SpreadsheetView;

            if (sv != null)
            {
                Log.Log.getLog().logMessage("Accessing cell ({0},{1}) in SpreadsheetView from Spreadsheet", e.RowIndex, e.ColumnIndex);
                Cell c = sv._spreadsheet.getCell(e.ColumnIndex, e.RowIndex);
                
                if (c != null)
                {
                    var text = Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                    if (userUpdatedCell)
                    {
                        userUpdatedCell = false;
                        if (text == null)
                        {
                            text = "";
                        }
                        c.setText(text.ToString());   
                    }
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
            
            Log.Log.getLog().logLine("GUI received property changed event {0}", e.PropertyName);

            if (cell != null)
            {
                Log.Log.getLog().logLine("Updating {0} value in gui", cell);
                this[cell.ColIndex, cell.RowIndex].Value = cell.getValue();
            }
            else
            {
                Log.Log.getLog().logLine("Trying to update a non cell item");
            }
        }
    }
}