using System;
using System.ComponentModel;

namespace SpreadsheetEngine
{
    
    public abstract class Cell : INotifyPropertyChanged
    {
        public readonly int RowIndex;
        public readonly int ColIndex;

        private string Text;
        private string Value;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(int col, int row)
        {
            RowIndex = row;
            ColIndex = col;
        }

        public string getText()
        {
            return Text;
        }

        public void setText(string newText)
        {
            if (!string.Equals(newText, Text))
            {
                Text = newText;
                OnPropertyChanged("Text");
            }
        }

        protected void OnPropertyChanged(string propertyName = null)
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