using System;
using NUnit.Framework;
using SpreadsheetEngine;

namespace SpreadsheetEngineTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestSimpleCell()
        {
            
            SimpleCell c = new SimpleCell(9, 4);
            
            Assert.True(c.RowIndex == 4);
            Assert.True(c.ColIndex == 9);
            Assert.True(c.works());
        }

        [Test]
        public void TestHeaderConverterIntToString()
        {
            Assert.AreEqual("A", HeaderConverter.Convert(0));
            Assert.AreEqual("I", HeaderConverter.Convert(8));
            Assert.AreEqual("Z", HeaderConverter.Convert(25));
            Assert.AreEqual("AA", HeaderConverter.Convert(26));
            Assert.AreEqual("AB", HeaderConverter.Convert(27));
            Assert.AreEqual("AZ", HeaderConverter.Convert(51));
            Assert.AreEqual("BA", HeaderConverter.Convert(52));
            Assert.AreEqual("ZZ", HeaderConverter.Convert(701));
            Assert.AreEqual("AAA", HeaderConverter.Convert(702));
        }

        [Test]
        public void TestHeaderConverterStringToInt()
        {
           Assert.AreEqual(0, HeaderConverter.Convert("A")); 
           Assert.AreEqual(8, HeaderConverter.Convert("I")); 
           Assert.AreEqual(25, HeaderConverter.Convert("Z")); 
           Assert.AreEqual(26, HeaderConverter.Convert("AA")); 
           Assert.AreEqual(27, HeaderConverter.Convert("AB")); 
           Assert.AreEqual(51, HeaderConverter.Convert("AZ")); 
           Assert.AreEqual(52, HeaderConverter.Convert("BA")); 
           Assert.AreEqual(702, HeaderConverter.Convert("AAA")); 
        }

        [Test]
        public void TestGetCellLocationA1()
        {
            Assert.AreEqual(Tuple.Create(0,0), HeaderConverter.getCellLocation("A1"));
        }
        
        [Test]
        public void TestGetCellLocationCX456()
        {
            Assert.AreEqual(Tuple.Create(HeaderConverter.Convert("CX"),455), HeaderConverter.getCellLocation("CX456"));
        }

        [Test]
        public void TestGetCellLocationThrowsErrorNegativeNumber()
        {
            var error = Assert.Throws<ArgumentException>(() => HeaderConverter.getCellLocation("AB-32"));
            Assert.That(error.Message, Is.EqualTo("The supplied string did not match a valid cell form"));
        }
        
        [Test]
        public void TestGetCellLocationThrowsErrorMissingNumber()
        {
            var error = Assert.Throws<ArgumentException>(() => HeaderConverter.getCellLocation("AB"));
            Assert.That(error.Message, Is.EqualTo("The supplied string did not match a valid cell form"));
        }
        
        [Test]
        public void TestGetCellLocationThrowsErrorMissingLetters()
        {
            var error = Assert.Throws<ArgumentException>(() => HeaderConverter.getCellLocation("57488"));
            Assert.That(error.Message, Is.EqualTo("The supplied string did not match a valid cell form"));
        }
    }
}