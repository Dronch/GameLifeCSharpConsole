using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace GameLifeCSharpConsole.UnitTests
{
    [TestFixture]
    class TestingExceptions
    {
        [Test]
        public void WrongInputFieldHeightThrowsException()
        {
            Field field = new Field(20, 40, false);
            string path = "inputWrongHeight.txt";
            Assert.Throws<InvalidFieldHeightException>(() => field.Init(path));
        }

        [Test]
        public void WrongInputFieldWidthThrowsException()
        {
            Field field = new Field(20, 40, false);
            string path = "inputWrongWidth.txt";
            Assert.Throws<InvalidFieldWidthException>(() => field.Init(path));
        }

        [Test]
        public void WrongInputFieldSymbolsThrowsException()
        {
            Field field = new Field(20, 40, false);
            string path = "inputWrongSymbols.txt";
            Assert.Throws<InvalidInputSymbolsException>(() => field.Init(path));
        }

        [Test]
        public void WrongInputFieldAllZeroesThrowsException()
        {
            Field field = new Field(20, 40, false);
            string path = "inputAllZeroes.txt";
            field.Init(path);
            Assert.Throws<NoActiveCellsException>(() => field.DrawField());

            //bool hasAnyActiveCells = false;
            //for (int y = 0; y < field.Height; y++)
            //{
            //    for (int x = 0; x < field.Width; x++)
            //    {
            //        if (field.GetCell(y, x).IsActive || field.GetCell(y, x).WillBeActive)
            //        {
            //            hasAnyActiveCells = true;
            //        }
            //    }
            //}

            //if (!hasAnyActiveCells)
            //{
            //    e = new NoActiveCellsException();
            //}

            //Assert.AreEqual(e.Message, new NoActiveCellsException().Message);
        }

    }

}
