using System;
using Ai;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class Test8Puzzle
    {
        [TestMethod]
        [TestCategory("Unit Test")]
        public void TestGetPosition()
        {
            int index, x, y;
            Ai_8Puzzle.GetPosition(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },out index, out x, out y);
            Assert.AreEqual(x, 0);
            Assert.AreEqual(y, 0);

            Ai_8Puzzle.GetPosition(new int[] { 1, 0, 2, 3, 4, 5, 6, 7, 8 }, out index, out x, out y);
            Assert.AreEqual(x, 1);
            Assert.AreEqual(y, 0);


            Ai_8Puzzle.GetPosition(new int[] { 1, 5, 2, 3, 4, 0, 6, 7, 8 }, out index, out x, out y);
            Assert.AreEqual(x, 2);
            Assert.AreEqual(y, 1);


            Ai_8Puzzle.GetPosition(new int[] { 1, 5, 2, 3, 4, 7, 6, 0, 8 }, out index, out x, out y);
            Assert.AreEqual(x, 1);
            Assert.AreEqual(y, 2);

        }

        [TestMethod]
        [TestCategory("Unit Test")]
        public void TestGetIndexFromXY()
        {
            int index;
            index  =Ai_8Puzzle.GetIndexFromXY(0,0);
            Assert.AreEqual(index, 0);

            index = Ai_8Puzzle.GetIndexFromXY(2, 0);
            Assert.AreEqual(index, 2);

            index = Ai_8Puzzle.GetIndexFromXY(1, 1);
            Assert.AreEqual(index, 4);
            

        } 

        [TestMethod]
        [TestCategory("Unit Test")]
        public void TestCanMoveUp()
        {
            int[] data;
            bool canMove = false;

            canMove  = Ai_8Puzzle.TryMoveDown(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveDown(new int[] { 1, 0, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveDown(new int[] { 1, 2, 0, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveDown(new int[] { 1, 2, 3, 0, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(data[3], 1);
            Assert.AreEqual(data[0], 0);
          
            canMove = Ai_8Puzzle.TryMoveDown(new int[] { 1, 2, 3, 4, 5, 6, 0, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveDown(new int[] { 1, 2, 3, 8, 4, 5, 6, 7, 0 }, out data);
            Assert.AreEqual(data[5], 0);
            Assert.AreEqual(data[8], 5);

            canMove = Ai_8Puzzle.TryMoveDown(new int[] { 1, 2, 3, 8, 0, 4, 6, 7, 5 }, out data);
            Assert.AreEqual(data[1], 0);
            Assert.AreEqual(data[4], 2);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        public void TestCanMoveDown()
        {
            int[] data;
            bool canMove = false;

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 1, 0, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 1, 2, 0, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 1, 2, 3, 0, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(data[6], 0);
            Assert.AreEqual(data[3], 6);

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 1, 2, 3, 4, 5, 6, 0, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 1, 2, 3, 4, 5, 6, 7, 0, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 1, 2, 3, 8, 4, 5, 6, 7, 0 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveUp(new int[] { 1, 2, 3, 8, 0, 4, 6, 7, 5 }, out data);
            Assert.AreEqual(canMove, true);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        public void TestCanMoveLeft()
        {
            int[] data;
            bool canMove = false;

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 1, 0, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 1, 2, 0, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 1, 2, 3, 0, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 1, 2, 3, 4, 5, 6, 0, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 1, 2, 3, 8, 4, 5, 6, 7, 0 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 1, 2, 3, 8, 0, 4, 6, 7, 5 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveRight(new int[] { 1, 2, 3, 8, 0, 4, 6, 7, 5 }, out data);
            Assert.AreEqual(data[3], 0);
            Assert.AreEqual(data[4], 8);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        public void TestCanMoveRight()
        {
            int[] data;
            bool canMove = false;

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 1, 0, 2, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 1, 2, 0, 3, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 1, 2, 3, 0, 4, 5, 6, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 1, 2, 3, 4, 5, 6, 0, 7, 8 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 1, 2, 3, 8, 4, 5, 6, 7, 0 }, out data);
            Assert.AreEqual(canMove, false);

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 1, 2, 3, 8, 0, 4, 6, 7, 5 }, out data);
            Assert.AreEqual(canMove, true);

            canMove = Ai_8Puzzle.TryMoveLeft(new int[] { 1, 2, 3, 8, 0, 4, 6, 7, 5 }, out data);
            Assert.AreEqual(data[5], 0);
            Assert.AreEqual(data[4], 4);
        }

    }
}
