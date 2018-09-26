using System;
using System.Linq;
using BSTtree;
using NUnit.Framework;

namespace BSTTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestParseInput()
        {
            string input1 = "12 15 17 20 33 10";
            int[] result1 = {12,15,17,20,33,10};

            Assert.True(Program.parseInput(input1).SequenceEqual(result1));
        }

        [Test]
        public void TestCount()
        {

            //values should not have a duplicate integer in them
            int[] values = {5, 8, 3, 2, 1, 9, 7, 6, 0,4};
            BST<int> bst = new BST<int>(5);

            for (int i = 1; i < values.Length; i++)
            {
                bst.Insert(values[i]);
            }
            
            Assert.AreEqual(bst.Count(), values.Length);
        }

        [Test]
        public void TestDuplicateAdd()
        {
            //no duplicates should exist in values
            int[] values = {2, 1, 3};
            BST<int> bst = new BST<int>(values[0]);

            for (int i = 1; i < values.Length; i++)
            {
                //add the same value twice
                bst.Insert(values[i]);
                bst.Insert(values[i]);
            }
            
            Assert.AreEqual(values.Length, bst.Count());
        }

        [Test]
        public void TestGetOptimalDepth()
        {
            int[] values = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17};
            BST<int> bst = new BST<int>(0);
            
            //Edge case for level 1, 1 item in bst
            Assert.AreEqual(1, bst.getOptimalDepth());
            
            //Edge case for level 2, 2 items in bst
            bst.Insert(values[1]);
            Assert.AreEqual(2, bst.getOptimalDepth());
            
            //Edge case for level 2, 3 items in bst
            bst.Insert(values[2]);
            Assert.AreEqual(2, bst.getOptimalDepth());
            
            //Edge case for level 3, 4 items in bst
            bst.Insert(values[3]);
            Assert.AreEqual(3, bst.getOptimalDepth());

            for (int i = 4; i < 6; i++)
            {
                bst.Insert(values[i]);
                Assert.AreEqual(3, bst.getOptimalDepth());
            }
            
            //Edge case for level 3, 7 items in bst
            bst.Insert(values[6]);
            Assert.AreEqual(3, bst.getOptimalDepth());
            
            //Edge case for level 4, 8 items in bst
            bst.Insert(values[7]);
            Assert.AreEqual(4, bst.getOptimalDepth());

            for (int i = 8; i < 14; i++)
            {
                bst.Insert(values[i]);
                Assert.AreEqual(4, bst.getOptimalDepth());
            }

            //Edge case for level 4, 15 items in bst
            bst.Insert(values[14]);
            Assert.AreEqual(4, bst.getOptimalDepth());
            
            //Edge case for level 5, 16 items in bst
            bst.Insert(values[15]);
            Assert.AreEqual(5, bst.getOptimalDepth());
            
            for (int i = 16; i < 18; i++)
            {
                bst.Insert(values[i]);
                Assert.AreEqual(5, bst.getOptimalDepth());
            }
        }

        [Test]
        public void TestDepth()
        {
            //assumes that the tree does not self balance
            int[] values1 = {8, 12, 10};
            int[] values2 = {8, 4, 2, 1};
            int[] values3 = {8, 4, 6, 5, 2};
            int[] values4 = {8, 4, 2, 1, 3, 7, 12, 10, 11, 14};
            int[] values5 = {8, 12};
            int[] values6 = {8};
            
            int result1 = 3;
            int result2 = 4;
            int result3 = 4;
            int result4 = 4;
            int result5 = 2;
            int result6 = 1;

            BST<int> tree1 = makeTree(values1);
            BST<int> tree2 = makeTree(values2);
            BST<int> tree3 = makeTree(values3);
            BST<int> tree4 = makeTree(values4);
            BST<int> tree5 = makeTree(values5);
            BST<int> tree6 = makeTree(values6);
            
            Assert.AreEqual(tree1.depth(), result1);
            Assert.AreEqual(tree2.depth(), result2);
            Assert.AreEqual(tree3.depth(), result3);
            Assert.AreEqual(tree4.depth(), result4);
            Assert.AreEqual(tree5.depth(), result5);
            Assert.AreEqual(tree6.depth(), result6);
        }

        private BST<int> makeTree(int[] values)
        {
            BST<int> root = new BST<int>(values[0]);
            for (int i = 1; i < values.Length; i++)
            {
                root.Insert(values[i]);
            }

            return root;
        }
    }
}