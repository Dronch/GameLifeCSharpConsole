using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace GameLifeCSharpConsole.UnitTests
{
    [TestFixture]
    class TestingCellClass
    {
        public Cell GetActiveCellSample()
        {
            Cell testCellActive = new Cell(true);
            testCellActive.ChangeGeneration();
            return testCellActive;
        }

        public Cell GetInactiveCellSample()
        {
            Cell testCellDead = new Cell(false);
            testCellDead.ChangeGeneration();
            return testCellDead;
        }

        [Test]
        public void ChangeGenerationTest()
        {
            Cell testCellActive = new Cell(true);
            bool wasActive = testCellActive.IsActive;
            testCellActive.ChangeGeneration();
            Assert.AreNotEqual(wasActive, testCellActive.IsActive);
        }

        [Test]
        public void PredictCellStatusInputWillBeBorn()
        {
            int activeCells = 3;
            //Testing both Active and Incative cell with 3 alive neighbors.
            //First case should be false because 
            Assert.IsFalse((GetActiveCellSample().IsActive == false) && activeCells == 3);
            Assert.IsTrue((GetInactiveCellSample().IsActive == false) && activeCells == 3);
        }

        [Test]
        public void PredictCellStatusWillDieInputIntTwo()
        {
            int activeCells = 2;

            //Testing both Active and Incative cell with 2 alive neighbors. 
            //Both cases should be false = shouldn't die/change status.
            Assert.IsFalse(GetActiveCellSample().IsActive && (activeCells < 2 || activeCells > 3));
            Assert.IsFalse(GetInactiveCellSample().IsActive && (activeCells < 2 || activeCells > 3));

        }

        [Test]
        public void PredictCellStatusWillDieInputIntOne()
        {
            int activeCells = 1;

            //Testing both Active and Incative cell with 2 alive neighbors. 
            //Fist case should be true. Second - false, because its dead already.
            Assert.IsTrue(GetActiveCellSample().IsActive && (activeCells < 2 || activeCells > 3));
            Assert.IsFalse(GetInactiveCellSample().IsActive && (activeCells < 2 || activeCells > 3));
        }
    }
}
