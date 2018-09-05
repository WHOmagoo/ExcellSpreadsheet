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

        [Test]
        public void TestCount()
        {

            //values should not have a duplicate integer in them
            int[] values = {5, 8, 3, 2, 1, 9, 7, 6, 0,4};
            BST[] bst = new BST[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                bst[i] = new BST(values[i]);
            }

            for (int i = 1; i < values.Length; i++)
            {
                bst[0].add(bst[i]);
            }
            
            Assert.AreEqual(bst[0].Count(), values.Length);
        }

        [Test]
        public void TestDuplicateAdd()
        {
            //no duplicates should exist in values
            int[] values = {2, 1, 3};
            BST[] bst = new BST[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                bst[i] = new BST(values[i]);
            }

            for (int i = 1; i < values.Length; i++)
            {
                //add the same value twice
                bst[0].add(bst[i]);
                bst[0].add(bst[i]);
            }
            
            Assert.AreEqual(values.Length, bst[0].Count());
        }

        [Test]
        public void TestGetOptimalDepth()
        {
            int[] values = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17};
            BST[] bst = new BST[18];

            for (int i = 0; i < 18; i++)
            {
                bst[i] = new BST(values[i]);
            }
            
            //Edge case for level 1, 1 item in bst
            Assert.AreEqual(1, bst[0].getOptimalDepth());
            
            //Edge case for level 2, 2 items in bst
            bst[0].add(bst[1]);
            Assert.AreEqual(2, bst[0].getOptimalDepth());
            
            //Edge case for level 2, 3 items in bst
            bst[0].add(bst[2]);
            Assert.AreEqual(2, bst[0].getOptimalDepth());
            
            //Edge case for level 3, 4 items in bst
            bst[0].add(bst[3]);
            Assert.AreEqual(3, bst[0].getOptimalDepth());

            for (int i = 4; i < 6; i++)
            {
                bst[0].add(bst[i]);
                Assert.AreEqual(3, bst[0].getOptimalDepth());
            }
            
            //Edge case for level 3, 7 items in bst
            bst[0].add(bst[6]);
            Assert.AreEqual(3, bst[0].getOptimalDepth());
            
            //Edge case for level 4, 8 items in bst
            bst[0].add(bst[7]);
            Assert.AreEqual(4, bst[0].getOptimalDepth());

            for (int i = 8; i < 14; i++)
            {
                bst[0].add(bst[i]);
                Assert.AreEqual(4, bst[0].getOptimalDepth());
            }

            //Edge case for level 4, 15 items in bst
            bst[0].add(bst[14]);
            Assert.AreEqual(4, bst[0].getOptimalDepth());
            
            //Edge case for level 5, 16 items in bst
            bst[0].add(bst[15]);
            Assert.AreEqual(5, bst[0].getOptimalDepth());
            
            for (int i = 16; i < 18; i++)
            {
                bst[0].add(bst[i]);
                Assert.AreEqual(5, bst[0].getOptimalDepth());
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

            BST tree1 = makeTree(values1);
            BST tree2 = makeTree(values2);
            BST tree3 = makeTree(values3);
            BST tree4 = makeTree(values4);
            BST tree5 = makeTree(values5);
            BST tree6 = makeTree(values6);
            
            Assert.AreEqual(tree1.depth(), result1);
            Assert.AreEqual(tree2.depth(), result2);
            Assert.AreEqual(tree3.depth(), result3);
            Assert.AreEqual(tree4.depth(), result4);
            Assert.AreEqual(tree5.depth(), result5);
            Assert.AreEqual(tree6.depth(), result6);
        }

        private BST makeTree(int[] values)
        {
            BST root = new BST(values[0]);
            for (int i = 1; i < values.Length; i++)
            {
                root.add(new BST(values[i]));
            }

            return root;
        }
    }
}                         