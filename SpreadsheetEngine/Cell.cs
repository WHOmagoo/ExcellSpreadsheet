using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using SpreadsheetEngine.Annotations;

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

        [NotifyPropertyChangedInvocator]
        public void setText(string newText)
        {
            if (!string.Equals(newText, Text))
            {
                OnPropertyChanged("Text");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}