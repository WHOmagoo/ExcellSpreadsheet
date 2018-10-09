using System.Collections;
using System.Collections.Generic;

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
                while (0 <= --colCount)
                {
                    
                }
            }
        }
    }
}