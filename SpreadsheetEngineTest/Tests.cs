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
            
            SimpleCell c = new SimpleCell(4, 9);
            
            Assert.True(c.RowIndex == 4);
            Assert.True(c.ColIndex == 9);
            Assert.True(c.works());
        }

        [Test]
        public void TestHeaderConverter()
        {
            Assert.AreEqual("A", HeaderConverter.Convert(1));
            Assert.AreEqual("A", HeaderConverter.Convert(1));
            Assert.AreEqual("A", HeaderConverter.Convert(1));
            Assert.AreEqual("A", HeaderConverter.Convert(1));
            Assert.AreEqual("AZ", HeaderConverter.Convert(52));
            Assert.AreEqual("BA", HeaderConverter.Convert(53));
        }
    }
}