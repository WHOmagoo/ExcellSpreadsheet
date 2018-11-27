using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CptS321;
using NUnit.Framework;

namespace SerializationTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            ExpTree expTree = new ExpTree("5");

            MemoryStream stream = new MemoryStream(100);

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(stream, expTree);
            
            stream.Flush();

            stream.Position = 0;
            
            ExpTree newExpTree = (ExpTree) binaryFormatter.Deserialize(stream);
                
            Assert.AreEqual(expTree.Eval(), newExpTree.Eval());
        }
    }
}