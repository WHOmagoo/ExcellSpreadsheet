using CptS321;
using NUnit.Framework;

namespace ExpressionTreeTest
{
    [TestFixture]
    public class Tests
    {
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