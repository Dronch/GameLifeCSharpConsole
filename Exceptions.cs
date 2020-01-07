using System;

namespace GameLifeCSharpConsole
{
    [Serializable]
    public class InvalidFieldHeightException : Exception
    {       
        public InvalidFieldHeightException() { }
        public InvalidFieldHeightException(int incorrectHeight, int correctHeight)
            : base(string.Format($"Given field is {incorrectHeight} high. " +
                $"Every line must be {correctHeight} symbols high."))
        { }
    }


    [Serializable]
    public class InvalidFieldWidthException : Exception
    {
        public InvalidFieldWidthException() { }
        public InvalidFieldWidthException(int incorrectWidth, int correctWidth)
            : base(string.Format($"Given line is {incorrectWidth} long. " +
                $"Every line must be {correctWidth} symbols long.")) 
        { }
    }


    [Serializable]
    public class NoActiveCellsException : Exception
    {
        public NoActiveCellsException() 
            : base(string.Format("Every field must contain at least one active cell!")) 
        { }
    }


    [Serializable]
    public class InvalidInputSymbolsException : Exception
    {
        public InvalidInputSymbolsException() 
        { }
        public InvalidInputSymbolsException(char invalidSymbol) 
            : base(string.Format($"Invalid symbol {invalidSymbol}. The input file must consist only of '1' and '0' symbols.") )
            
        { }
    }
}
