﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using CptS321;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SpreadsheetEngine;

namespace SerializationTest
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void TestSerializeAndReloadMakesNewReference()
        {
            ExpTree expTree = new ExpTree("5");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreNotSame(expTree, newExpTree);
        }
        
        [Test]
        public void TestSingleNumber()
        {
            ExpTree expTree = new ExpTree("5");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.ToString(), newExpTree.ToString());
        }

        [Test]
        public void TestSimpleAddition()
        {
            ExpTree expTree = new ExpTree("5+1");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }
        
        [Test]
        public void TestSimpleSubtraction()
        {
            ExpTree expTree = new ExpTree("5-3");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }
        
        [Test]
        public void TestSimpleDivision()
        {
            ExpTree expTree = new ExpTree("12/3");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }
        
        [Test]
        public void TestSimpleMultiplication()
        {
            ExpTree expTree = new ExpTree("7*13");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }

        [Test]
        public void TestSimpleNegationOperator()
        {
            ExpTree expTree = new ExpTree("-6");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }
        
        [Test]
        public void TestMutlipleNestedParenthesisSerialized()
        {
            ExpTree expTree = new ExpTree("((1+2)*(3-2))*(4/2)");
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }

        [Test]
        public void TestComplexVariableNodesPreset()
        {
            ExpTree expTree = new ExpTree("((A1+A2)*(A3-A2))*(A4/A2)");
            expTree.SetVar("A1", 1);
            expTree.SetVar("A2", 2);
            expTree.SetVar("A3", 3);
            expTree.SetVar("A4", 4);
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }
        
        [Test]
        public void TestComplexVariableNodesUnset()
        {
            ExpTree expTree = new ExpTree("((A1+A2)*(A3-A2))*(A4/A2)");
            ExpTree newExpTree = serializeAndReload(expTree);
            expTree.SetVar("A1", 1);
            expTree.SetVar("A2", 2);
            expTree.SetVar("A3", 3);
            expTree.SetVar("A4", 4);
            newExpTree.SetVar("A1", 1);
            newExpTree.SetVar("A2", 2);
            newExpTree.SetVar("A3", 3);
            newExpTree.SetVar("A4", 4);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }

        [Test]
        public void TestSimpleVariableNode()
        {
            ExpTree expTree = new ExpTree("A1");
            expTree.SetVar("A1", 6);
            ExpTree newExpTree = serializeAndReload(expTree);
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }

        [Test]
        public void TestSimpleVariableNodeSerializedUnset()
        {
            ExpTree expTree = new ExpTree("A4");
            ExpTree newExpTree = serializeAndReload(expTree);
            
            newExpTree.SetVar("A4", 6);
            expTree.SetVar("A4", 6);
            
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }

        [Test]
        public void TestSpreadsheetSerialization()
        {
            Spreadsheet spreadsheet = new Spreadsheet(10,10);
            
            spreadsheet.getCell(0,0).setText("55");
            spreadsheet.getCell(1,0).setText("A1");
            spreadsheet.getCell(2,0).setText("A1-53");
            spreadsheet.getCell(3,0).setText("B1*C1");

            Spreadsheet newSpreadsheet = serializeAndReload(spreadsheet);

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Assert.AreEqual(spreadsheet.getCell(row, col).getText(), newSpreadsheet.getCell(row, col).getText());
                    Assert.AreEqual(spreadsheet.getCell(row, col).getValue(), newSpreadsheet.getCell(row, col).getValue());
                }
            }
        }

        private T serializeAndReload<T>(T oldObj) where T : IXmlSerializable, new()
        {
            MemoryStream stream = new MemoryStream();
            
//            BinaryFormatter serializer = new BinaryFormatter();
            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                writer.WriteStartDocument();
                oldObj.WriteXml(writer);            
                writer.WriteEndDocument();
            }

            stream.Flush();

            stream.Position = 0;
            
            StreamReader sw = new StreamReader(stream);
            string result = sw.ReadToEnd();

            Console.WriteLine(result);
            
            stream.Position = 0;

            T newExpTree;
            using (XmlReader reader = XmlReader.Create(stream))
            {
                newExpTree = new T();
                newExpTree.ReadXml(reader);
            }
//            serializer.Serialize(stream, oldObj);



            return newExpTree;
        }
    }
}