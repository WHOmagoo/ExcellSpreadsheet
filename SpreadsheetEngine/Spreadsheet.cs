using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;

namespace SpreadsheetEngine
{
    public class Spreadsheet
    {
        private int rowCount;
        private int colCount;
        
        private List<List<Cell>> cells;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Spreadsheet(int rowCount = 40, int colCount = 40)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;

            cells = new List<List<Cell>>(rowCount);
            
            while (0 <= --rowCount)
            {

                int col = colCount;
                List<Cell> currentCols = new List<Cell>(colCount);
                
                while (0 <= --col)
                {
                    Cell cell = new SimpleCell(rowCount, col);

                    cell.PropertyChanged += cellPropertyChanged;
                    
                    currentCols.Add(cell);    
                }
                
                cells.Add(currentCols);
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

        public Cell getCell(int row, int col)
        {
            if (cells.Count > row && cells[row].Count > col && row >= 0 && col >= 0)
            {
                return cells[row][col];
            }

            return null;
        }

        public void cellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            
            
            Cell cell = sender as Cell;
            
            Log.Log.getLog().logMessage("Property changed {0}.", e.PropertyName);

            if (cell != null && e.PropertyName == "Text")
            {   
                if (cell.getText().StartsWith("="))
                {

                    Console.WriteLine("Setting value for equation");
                    //TODO create a class that can evaluate values for equations
                    string cellContents = cell.getText();

                    string[] parts = cellContents.Split(',');

                    try
                    {
                        int rowNum = Int32.Parse(parts[0].Substring(1));
                        int colNum = HeaderConverter.Convert(parts[1]);

                        Cell copyValue = getCell(rowNum - 1, colNum);
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
                Log.Log.getLog().logMessage("({0},{1}) Text = {2}", cell.ColIndex, cell.RowIndex, cell.getText());
                
                OnPropertyChanged(cell, "Value");
                
            }
            else
            {
                Log.Log.getLog().logMessage("Unknown type changed");
            }
        }
        
        protected void OnPropertyChanged(Cell sender, string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                Log.Log.getLog().logMessage("Cell property changed. We changed {0}", propertyName);
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Log.Log.getLog().logMessage("Handler was null in Cell");
            }
        }
        
    }
}