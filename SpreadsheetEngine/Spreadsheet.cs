using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CptS321;

namespace SpreadsheetEngine
{
    public class Spreadsheet : IXmlSerializable
    {
        private int rowCount;
        private int colCount;
        
        private Dictionary<Tuple<int, int>, Cell> cells;
        //private Dictionary<Cell, List<Cell>> valueLinks;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Spreadsheet()
        {
            
        }
        
        public Spreadsheet(int rowCount = 40, int colCount = 40)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;

            cells = new Dictionary<Tuple<int, int>, Cell>();
            
            //valueLinks = new Dictionary<Cell, List<Cell>>();
            
            for (int row = 0; row < rowCount; row++)
            {
                for(int col = 0; col < colCount; col++)
                {
                    Cell cell = new SimpleCell(col, row);

                    cell.PropertyChanged += cellPropertyChanged;
                        
                    cells.Add(new Tuple<int, int>(row, col), cell);
                }
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
        
        public Cell getCell(int col, int row)
        {
            Tuple<int, int> key = new Tuple<int, int>(row, col);
            if (cells.ContainsKey(key))
            {
                
                Cell cell = cells[key];
                Log.Log.getLog().logLine("Sending {0} from input ({1},{2}).", cell, row, col);
                return cell;
            }

            Log.Log.getLog().logLine("Failed to get cell ({0},{1})", row, col);
            return null;
        }

        public void cellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            
            
            Cell cell = sender as Cell;
            
            Log.Log.getLog().logMessage("CellPropertyChangedHandler in Spreadsheet {0}.", e.PropertyName);

            if (cell != null)
            {
                switch (e.PropertyName)
                {
                    case "Text": OnCellTextChange(cell); break;
                    case "Value": OnValueChangedEvent(cell); break;
                    default : Log.Log.getLog().logLine("Unknown property changed for {0},{1}", cell.RowIndex, cell.ColIndex);
                        break;
                }

            }
            else
            {
                Log.Log.getLog().logMessage("Unknown type changed");
                OnPropertyChanged(sender, e.PropertyName);
            }
        }

        private void OnCellTextChange(Cell cell)
        {
            if (cell.getText().StartsWith("="))
            {
                Console.WriteLine("Setting value for equation");

                try
                {
                    ExpTree expressionTree = new ExpTree(cell.getText().Substring(1));

                    foreach (string name in expressionTree.getVariableNames())
                    {
                        Tuple<int, int> location = HeaderConverter.getCellLocation(name);

                        Cell link = getCell(location.Item1, location.Item2);

                        try
                        {
                            double value = Double.Parse(link.getValue());

                            expressionTree.SetVar(name, value);
                        }
                        catch (Exception e)
                        {
                            //We could not parse the value of the cell, continue anyways
                        }

                        link.PropertyChanged += cell.onLinkChange;

                    }
                    
                    cell.setExpTree(expressionTree);
                }
                catch (Exception e)
                {
                    cell.setValue("ERROR");
                }
            }
            else
            {
                foreach (string name in cell.getSubscribedCellsName())
                {
                    Tuple<int, int> location = HeaderConverter.getCellLocation(name);

                    Cell link = getCell(location.Item1, location.Item2);

                    link.PropertyChanged -= cell.onLinkChange;
                }
                
                cell.setExpTree(null);
            }
            
            Log.Log.getLog().logMessage("({0},{1}) Text = {2}", cell.ColIndex, cell.RowIndex,
                cell.getText());

            OnPropertyChanged(cell, "Text");
        }

        private void OnValueChangedEvent(Cell cell)
        {
            Log.Log.getLog().logLine("{0}'s value was changed to {1}", cell, cell.getValue());
//            if (valueLinks.ContainsKey(cell))
//            {
//                foreach (var cellLinked in valueLinks[cell])
//                {
//                    Log.Log.getLog().logLine("Updating {0}'s value from {1}", cellLinked, cell);
//                    cellLinked.setValue(cell.getValue());
//                }
//            }
            
            OnPropertyChanged(cell, "Value");
        }

        protected void OnPropertyChanged(object sender, string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                Log.Log.getLog().logMessage("Cell OnPropertyChangedCell changed {0}", propertyName);
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Log.Log.getLog().logMessage("***Handler was null in Cell***");
            }
        }

        public void Save(StreamWriter writer, ImageFormat jpeg)
        {
            Console.WriteLine("Saving");
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            string sColCount = reader.GetAttribute("ColCount");
            string sRowCount = reader.GetAttribute("RowCount");

            try
            {
                colCount = Int32.Parse(sColCount);
                rowCount = Int32.Parse(sRowCount);
            }
            catch
            {
                Console.WriteLine("Row count or Col count not specified in xml");
                return;
            }
            
            reader.ReadStartElement();

            string sCount = reader.GetAttribute("Count");
            
            cells = new Dictionary<Tuple<int, int>, Cell>();

            for (int col = 0; col < colCount; col++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    Cell cell = new SimpleCell(row, col);
                    cells.Add(Tuple.Create<int, int>(row, col), cell);
                }
            }
            
            int count;
            
            if (Int32.TryParse(sCount, out count) && count > 0)
            {
                reader.ReadStartElement("Cells");
            
                for (int i = 0; i < count; i++)
                {
                    if (i >= rowCount * colCount)
                    {
                        Console.WriteLine("Were high bois");
                    }
                    Cell cell = new SimpleCell();
                    cell.ReadXml(reader);
                
                    try
                    {
                        Cell curCell = cells[Tuple.Create(cell.RowIndex, cell.ColIndex)];
                        curCell.PropertyChanged += cellPropertyChanged;
                        curCell.CopyIn(cell);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to load {0}", cell);
                    }
                }
                
                reader.ReadEndElement();
            }
            
            reader.ReadEndElement();

//            string sCount = reader.GetAttribute("Count");
//            reader.ReadStartElement("ValueLinks");
//
//            int linksCount;
//            if(Int32.TryParse(sCount, out linksCount))
//            {
//
//                reader.ReadStartElement("LinkOrigin");
//                //.ReadAttributeValue("Col");
//                for (int i = 0; i < linksCount; i++)
//                {
//                    int valuesCount;
//                }
//            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Spreadsheet");
            writer.WriteAttributeString("ColCount", colCount.ToString());
            writer.WriteAttributeString("RowCount", rowCount.ToString());
            
            writer.WriteStartElement("Cells");
            
            List<Cell> cellsList = new List<Cell>();

            foreach (var key in cells.Keys)
            {
                Cell curCell = cells[key];

                if (curCell.hasData())
                {
                    cellsList.Add(curCell);   
                }
            }
            
            writer.WriteAttributeString("Count", cellsList.Count.ToString());
            
            foreach (var cell in cellsList)
            {
                cell.WriteXml(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            
//            writer.WriteStartElement("ValueLinks");
//            writer.WriteAttributeString("Count", valueLinks.Count.ToString());
//
//            foreach (Cell key in valueLinks.Keys)
//            {
//                writer.WriteStartElement("LinkOrigin");
//                writer.WriteAttributeString("Col", key.ColIndex.ToString());
//                writer.WriteAttributeString("Row", key.RowIndex.ToString());
//                writer.WriteAttributeString("LinksCount", valueLinks[key].Count.ToString());
//
//                foreach (Cell destination in valueLinks[key])
//                {
//                    writer.WriteStartElement("DestinationCell");
//                    writer.WriteAttributeString("Col", destination.ColIndex.ToString());
//                    writer.WriteAttributeString("Row", destination.RowIndex.ToString());
//                    writer.WriteEndElement();
//                }
//                
//                writer.WriteEndElement();
//                
//            }
//            
//            writer.WriteEndElement();
        }
    }
}