using System;
using System.ComponentModel;
using CptS321;

namespace SpreadsheetEngine
{
    
    public abstract class Cell : INotifyPropertyChanged
    {
        public readonly int RowIndex;
        public readonly int ColIndex;

        private string Text;
        private string Value;

        private ExpTree expressionTree;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(int col, int row)
        {
            RowIndex = row;
            ColIndex = col;

            Text = "";
            Value = "";
        }

        public string getText()
        {
            return Text;
        }

        public void setExpTree(ExpTree newExpTree)
        {
            expressionTree = newExpTree;

            try
            {
               setValue(expressionTree.Eval().ToString());
                
            }
            catch (Exception e)
            {
                setValue("#VALUE");
            }
        }

        public void setText(string newText)
        {
            if (!string.Equals(newText, Text))
            {
                Text = newText;
                OnPropertyChanged("Text");
            }
        }

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                Log.Log.getLog().logMessage("Cell {0} OnPropertyChanged. Modified {1}", this, propertyName);
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Log.Log.getLog().logMessage("Handler was null in Cell");
            }
        }

        public void onLinkChange(object sender, PropertyChangedEventArgs e)
        {
            Cell link = sender as Cell;

            if ("Value".Equals(e.PropertyName) &&link != null)
            {
                string cellName = HeaderConverter.getCellName(new Tuple<int, int>(link.ColIndex, link.RowIndex));
                try
                {
                    expressionTree.SetVar(cellName, Int32.Parse(link.getValue()));
                    setValue(expressionTree.Eval().ToString());
                }
                catch (Exception exception)
                {
                    setValue("#VALUE");
                }
            }
        }

        internal void setValue(string newValue)
        {
            if (!string.Equals(Value, newValue))
            {
                Value = newValue;
                OnPropertyChanged("Value");
            }
        }

        public string getValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return String.Format("({0},{1})", RowIndex, ColIndex);
        }
    }
}