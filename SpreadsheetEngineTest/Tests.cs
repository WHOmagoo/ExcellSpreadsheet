using System;
using CptS321;
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
        public void TestExpTree()
        {
            Assert.AreEqual(12, new ExpTree("3*4").Eval());
            Assert.AreEqual(7, new ExpTree("3+4").Eval());
            Assert.AreEqual(-1, new ExpTree("3-4").Eval());
            Assert.AreEqual(2, new ExpTree("8/4").Eval());
            
        }

        [Test]
        public void TestExpTreeVariable()
        {
            ExpTree exp = new ExpTree("varA+varB+nd4spd");
            exp.SetVar("varA", 1);
            exp.SetVar("varB", 2);
            exp.SetVar("nd4spd", 3);
            
            Assert.AreEqual(6, exp.Eval());
            
            exp = new ExpTree("-varA*varB*-nd4spd");
            exp.SetVar("varA", -1);
            exp.SetVar("varB", 2);
            exp.SetVar("nd4spd", -3);
            
            Assert.AreEqual(6, exp.Eval());
        }
    }
}