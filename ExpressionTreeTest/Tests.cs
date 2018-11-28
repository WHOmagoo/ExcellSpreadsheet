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
        public void TestParenthesisSimple()
        {
            ExpTree exp = new ExpTree("(1+2)");
            Assert.AreEqual(3, exp.Eval());
        }

        [Test]
        public void TestParenthesisLeft()
        {
            ExpTree exp = new ExpTree("(1+2)*3");
            Assert.AreEqual(9, exp.Eval());
        }

        [Test]
        public void TestParenthesisRight()
        {
            ExpTree exp = new ExpTree("2*(1+3)");
            Assert.AreEqual(8, exp.Eval());
        }

        [Test]
        public void TestParenthesisBoth()
        {
            ExpTree exp = new ExpTree("(2+3)*(1+4)");
            Assert.AreEqual(25, exp.Eval());
        }

        [Test]
        public void TestNestedParenthesis()
        {
            ExpTree exp = new ExpTree("((1+2)*3)*4");
            Assert.AreEqual(36, exp.Eval());
        }

        [Test]
        public void TestMutlipleNestedParenthesis()
        {
            ExpTree exp = new ExpTree("((1+2)*(3-2))*(4/2)");
            Assert.AreEqual(6, exp.Eval());
        }

        [Test]
        public void TestDoubleComplex()
        {
            ExpTree exp = new ExpTree("((1.1+2.2)*(3.1415-2.8))*(4.5/1.5)");
            Assert.AreEqual(((1.1+2.2)*(3.1415-2.8))*(4.5/1.5), exp.Eval());
        }

        [Test]
        public void TestDoubleWithNegative()
        {
            ExpTree exp = new ExpTree("-2.31");
            Assert.AreEqual(-2.31, exp.Eval());
        }
        
        [Test]
        public void TestDoubleWithNegativeNoLeadingDigit()
        {
            ExpTree exp = new ExpTree("-.31");
            Assert.AreEqual(-.31, exp.Eval());
        }
        
        [Test]
        public void TestDoubleWithNegativeNoTrailingDigit()
        {
            ExpTree exp = new ExpTree("-3.");
            Assert.AreEqual(-3, exp.Eval());
        }
        
        [Test]
        public void TestDoubleWithNegativeNoTrailingDigitAdded()
        {
            ExpTree exp = new ExpTree("-3.+2.1");
            Assert.AreEqual(-3+2.1, exp.Eval());
        }
    }
}