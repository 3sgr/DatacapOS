using System;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public class FTException : Exception
    {
        public FTException()
        {
        }
        public FTException(string message) : base(message)
        {
        }
    }
}
