using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using GameLifeCSharpConsole;

namespace GameLifeCSharpConsole.UnitTests
{
    [TestFixture]
    class TestingCellClass
    {
        [Test]
        public void ChangeGenerationTest()
        {
            Cell activeCell = new Cell(true);            
            bool wasActive = activeCell.IsActive;
            activeCell.ChangeGeneration();
            Assert.AreNotEqual(wasActive, activeCell.IsActive);
        }

        [Test]
        public void PredictCellStatusInputWillBeBorn()
        {
            int activeCells = 3;
            //Testing both Active and Incative cell with 3 alive neighbors.
            //First case should be false because 
            Assert.IsFalse((CellFactory.Create(true).IsActive == false) && activeCells == 3);
            Assert.IsTrue((CellFactory.Create(false).IsActive == false) && activeCells == 3);
        }

        [Test]
        public void PredictCellStatusWillDieInputIntTwo()
        {
            int activeCells = 2;

            //Testing both Active and Incative cell with 2 alive neighbors. 
            //Both cases should be false = shouldn't die/change status.
            Assert.IsFalse(CellFactory.Create(true).IsActive && (activeCells < 2 || activeCells > 3));
            Assert.IsFalse(CellFactory.Create(false).IsActive && (activeCells < 2 || activeCells > 3));

        }

        [Test]
        public void PredictCellStatusWillDieInputIntOne()
        {
            int activeCells = 1;

            //Testing both Active and Incative cell with 2 alive neighbors. 
            //Fist case should be true. Second - false, because its dead already.
            Assert.IsTrue(CellFactory.Create(true).IsActive && (activeCells < 2 || activeCells > 3));
            Assert.IsFalse(CellFactory.Create(false).IsActive && (activeCells < 2 || activeCells > 3));
        }
    }
}
