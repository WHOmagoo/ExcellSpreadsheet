namespace SpreadsheetEngine
{
    public class SimpleCell : Cell
    {
        public SimpleCell(int row, int col) : base(row, col)
        {
            //calls the parent constructor
        }

        public bool works()
        {
            return true;
        }
    }
}