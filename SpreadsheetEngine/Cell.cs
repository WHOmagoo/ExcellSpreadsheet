using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CptS321;

namespace SpreadsheetEngine
{
    
    [Serializable]
    public abstract class Cell : INotifyPropertyChanged, IXmlSerializable
    {
        public int RowIndex { get; private set; }
        public int ColIndex { get; private set; }

        private string Text;
        private string Value;

        private ExpTree expressionTree;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Cell()
        {
            
        }
        
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
                Console.WriteLine("Error>>>");
                Console.WriteLine(e.Message);
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
                    expressionTree.SetVar(cellName, Double.Parse(link.getValue()));
                    setValue(expressionTree.Eval().ToString());
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error>");
                    Console.WriteLine(exception.Message);
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            string rowIndex = reader.GetAttribute("RowIndex");
            string colIndex = reader.GetAttribute("ColIndex");
            string sHasExpTree = reader.GetAttribute("ContainsExpression");

            bool hasExpTree;
            
            try
            {
                RowIndex = Int32.Parse(rowIndex);
                ColIndex = Int32.Parse(colIndex);
                hasExpTree = Boolean.Parse(sHasExpTree);
            }
            catch
            {
                Console.WriteLine("Could not load the row and col index");
                return;
            }
            
            reader.ReadStartElement();
            
            reader.ReadStartElement("Text");
            Text = reader.ReadString();
            reader.ReadEndElement();
            
            reader.ReadStartElement("Value");
            Value = reader.ReadString();
            reader.ReadEndElement();

            if (hasExpTree)
            {
                expressionTree = new ExpTree();
                expressionTree.ReadXml(reader);
            }
            
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Cell");
            writer.WriteAttributeString("RowIndex", RowIndex.ToString());
            writer.WriteAttributeString("ColIndex", ColIndex.ToString());
            writer.WriteAttributeString("ContainsExpression", (expressionTree != null).ToString());
            
            writer.WriteStartElement("Text");
            writer.WriteString(Text);
            writer.WriteEndElement();
            
            writer.WriteStartElement("Value");
            writer.WriteString(Value);
            writer.WriteEndElement();

            if (expressionTree != null)
            {
                expressionTree.WriteXml(writer);
            }

            writer.WriteEndElement();
            
        }
    }
}