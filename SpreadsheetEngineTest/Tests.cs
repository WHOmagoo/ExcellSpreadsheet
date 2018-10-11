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
    }
}