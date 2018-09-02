using System;
using System.Linq;
using NUnit.Framework;
using BSTtree;

namespace BSTtreeTest
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void TestParseInput()
        {
            string input1 = "12 15 17 20 33 10";
            int[] result1 = {12,15,17,20,33,10};

            Assert.True(Enumerable.SequenceEqual(Program.parseInput(input1), result1));
        }
    }
}                         