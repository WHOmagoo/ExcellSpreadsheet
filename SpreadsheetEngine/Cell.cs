using System;
using System.ComponentModel;

namespace SpreadsheetEngine
{
    
    public abstract class Cell : INotifyPropertyChanged
    {
        public readonly int RowIndex;
        public readonly int ColIndex;

        private string Text;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(int row, int col)
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
                Log.Log.getLog().logMessage("Cell property changed. We changed {0}", propertyName);
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Log.Log.getLog().logMessage("Handler was null in Cell");
            }
        }
    }
}