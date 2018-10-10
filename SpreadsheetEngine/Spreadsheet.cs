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
            
            Console.WriteLine("Property changed " + e.PropertyName + ".");

            if (cell != null && e.PropertyName == "Text")
            {
                //TODO check if the new text is an equation or not and update the value of the cell accordingly
                Console.WriteLine("(" + cell.ColIndex + "," + cell.RowIndex + ") = " + cell.getText());
            }
            else
            {
                Console.WriteLine("Unknown type changed");
            }
        }
    }
}