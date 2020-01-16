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
        public void CellStaysActiveAfterPredictCellStatusValueThree()
        {
            Cell activeCell = CellFactory.Create(true);
            activeCell.PredictCellStatus(3);
            Assert.IsTrue(activeCell.WillBeActive);
        }

        [Test]
        public void CellBecomesActiveAfterPredictGenerationValueThree()
        {

            Cell deadCell = CellFactory.Create(false);
            deadCell.PredictCellStatus(3);
            Assert.IsTrue(deadCell.WillBeActive);
        }

        [Test]
        public void CellStaysAliveAfterPredictGenerationValueTwo()
        {

            Cell deadCell = CellFactory.Create(true);
            deadCell.PredictCellStatus(2);
            Assert.IsTrue(deadCell.WillBeActive);
        }

        [Test]
        public void CellStaysDeadAfterPredictGenerationValueTwo()
        {

            Cell deadCell = CellFactory.Create(false);
            deadCell.PredictCellStatus(2);
            Assert.IsFalse(deadCell.WillBeActive);
        }

        [Test]
        public void CellDiesAfterPredictGenerationValueOne()
        {

            Cell deadCell = CellFactory.Create(true);
            deadCell.PredictCellStatus(1);
            Assert.IsFalse(deadCell.WillBeActive);
        }

        public void CellStaysDeadAfterPredictGenerationValueOne()
        {

            Cell deadCell = CellFactory.Create(false);
            deadCell.PredictCellStatus(1);
            Assert.IsFalse(deadCell.WillBeActive);
        }

    }
}
