using System;
using NUnit.Framework;
using SpreadsheetEngine;

namespace SpreadsheetEngineTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }

        [Test]
        public void TestCell()
        {
            Cell c = new Cell(4, 9);
            Assert.True();
        }
    }
}