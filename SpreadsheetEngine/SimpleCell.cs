using System;

namespace SpreadsheetEngine
{
    [Serializable]
    public class SimpleCell : Cell
    {
        public SimpleCell()
        {
            
        }
        
        public SimpleCell(int col, int row) : base(col, row)
        {
            //calls the parent constructor
        }
    }
}