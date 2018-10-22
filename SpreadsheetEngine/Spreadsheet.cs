using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpreadsheetEngine
{
    public class Spreadsheet
    {
        private int rowCount;
        private int colCount;
        
        private Dictionary<Tuple<int, int>, Cell> cells;
        private Dictionary<Cell, List<Cell>> valueLinks;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Spreadsheet(int rowCount = 40, int colCount = 40)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;

            cells = new Dictionary<Tuple<int, int>, Cell>();
            
            valueLinks = new Dictionary<Cell, List<Cell>>();
            
            for (int row = 0; row < rowCount; row++)
            {
                for(int col = 0; col < colCount; col++)
                {
                    Cell cell = new SimpleCell(col, row);

                    cell.PropertyChanged += cellPropertyChanged;
                        
                    cells.Add(new Tuple<int, int>(row, col), cell);
                }
            }
        }

        public int RowCount
        {
            get { return rowCount; }
        }

        public int ColCount
        {
            get { return colCount; }
        }

        public Cell getCell(int col, int row)
        {
            Tuple<int, int> key = new Tuple<int, int>(row, col);
            if (cells.ContainsKey(key))
            {
                
                 Cell cell = cells[key];
                Log.Log.getLog().logLine("Sending {0} from input ({1},{2}).", cell, row, col);
                return cell;
            }

            Log.Log.getLog().logLine("Failed to get cell ({0},{1})", row, col);
            return null;
        }

        public void cellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            
            
            Cell cell = sender as Cell;
            
            Log.Log.getLog().logMessage("CellPropertyChangedHandler in Spreadsheet {0}.", e.PropertyName);

            if (cell != null)
            {
                switch (e.PropertyName)
                {
                    case "Text": OnCellTextChange(cell); break;
                    case "Value": OnValueChangedEvent(cell); break;
                    default : Log.Log.getLog().logLine("Unknown property changed for {0},{1}", cell.RowIndex, cell.ColIndex);
                        break;
                }

            }
            else
            {
                Log.Log.getLog().logMessage("Unknown type changed");
                OnPropertyChanged(sender, e.PropertyName);
            }
        }

        private void OnCellTextChange(Cell cell)
        {
            if (cell.getText().StartsWith("="))
            {
                Console.WriteLine("Setting value for equation");
                //TODO refactor this to a class that can evaluate values for equations
                string cellContents = cell.getText();

                
//                Regex split = new Regex("([-+*/)/(])");
//                split.Split(cellContents);
                string[] parts = cellContents.Split(',');

                try
                {
                    int colNum = HeaderConverter.Convert(parts[0].Substring(1));
                    int rowNum = Int32.Parse(parts[1]);

                    Cell copyValue = getCell(colNum, rowNum - 1);

                    if (!valueLinks.ContainsKey(copyValue))
                    {
                        valueLinks.Add(copyValue, new List<Cell>());
                    }

                    valueLinks[copyValue].Add(cell);
                    Log.Log.getLog().logLine("{0} subscribing to {1}", cell, copyValue);
                    cell.setValue(copyValue.getValue());
                }
                catch (Exception error)
                {
                    cell.setValue("ERROR");
                }
            }
            else
            {
                cell.setValue(cell.getText());
            }

            //TODO check if the new text is an equation or not and update the value of the cell accordingly
            Log.Log.getLog().logMessage("({0},{1}) Text = {2}", cell.ColIndex, cell.RowIndex,
                cell.getText());

            OnPropertyChanged(cell, "Text");
        }

        private void OnValueChangedEvent(Cell cell)
        {
            Log.Log.getLog().logLine("{0}'s value was changed to {1}", cell, cell.getValue());
            if (valueLinks.ContainsKey(cell))
            {
                foreach (var cellLinked in valueLinks[cell])
                {
                    Log.Log.getLog().logLine("Updating {0}'s value from {1}", cellLinked, cell);
                    cellLinked.setValue(cell.getValue());
                }
            }
            
            OnPropertyChanged(cell, "Value");
        }

        protected void OnPropertyChanged(object sender, string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                Log.Log.getLog().logMessage("Cell OnPropertyChangedCell changed {0}", propertyName);
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Log.Log.getLog().logMessage("***Handler was null in Cell***");
            }
        }
        
    }
}