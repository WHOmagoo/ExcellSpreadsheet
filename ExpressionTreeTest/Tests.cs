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

        [Test]
        public void TestOrderOfOperations()
        {
            ExpTree exp = new ExpTree("2+3*4");
            Assert.AreEqual(14, exp.Eval());

            exp = new ExpTree("2*3+4*5");
            Assert.AreEqual(26, exp.Eval());

            exp = new ExpTree("1+2*8/4");
            Assert.AreEqual(5, exp.Eval());

            exp = new ExpTree("8*4/2-2/2*9");
            Assert.AreEqual(7, exp.Eval());
        }

        [Test]
        public void TestParenthesis()
        {
            ExpTree exp = new ExpTree("()");
        }
    }
}