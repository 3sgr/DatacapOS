using System;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public class Arithmetic
    {
        #region Operation
        public string ProcesFormula(string left, string sign, string right, FormatTranslator baseFormat)
        {
            string res;
            switch (sign)
            {
                case "^": Exponentiation (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
                case "/": Division       (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
                case "*": Multiplication (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
                case "-": Subtraction    (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
                case "+": Addition       (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
                default: res = ContinuationAr(left, sign, right, baseFormat); break;
            }
            return res;
        }
        public static Func<int, int> Addition(object a, object b, out string res)
        {
            res = ((dynamic)a + (dynamic)b).ToString();
            return null;
        }
        public static Func<int, int> Subtraction(object a, object b, out string res)
        {
            res = ((dynamic)a - (dynamic)b).ToString();
            return null;
        }
        public static Func<int, int> Multiplication(object a, object b, out string res)
        {
            res = ((dynamic)a * (dynamic)b).ToString();
            return null;
        }
        public static Func<int, int> Division(object a, object b, out string res)
        {
            res = ((dynamic)a / (dynamic)b).ToString();
            return null;
        }
        public static Func<int, int> Exponentiation(object a, object b, out string res)
        {
            res = ((dynamic)a ^ (dynamic)b).ToString();
            return null;
        }
        #endregion
        public string ContinuationAr(string left, string sign, string right, FormatTranslator baseFormat)
        {
            return "!!!";
        }
    }
 }
