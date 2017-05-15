using System;
using System.Collections.Generic;
using System.Globalization;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.Resources;


namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public static class Functions
    {
        private static readonly FormatTranslator FT = new FormatTranslator();
        public static Dictionary<string, Func<string, string>> MathFunctions = new Dictionary<string, Func<string, string>> { { "e", E }, { "pi", Pi}, { "sin", Sin}, { "abs", Abs }, { "acos", ACos}, { "asin", Asin}, { "pow", Pow }, { "cos", Cos} };

        public static string E(string anyValue)
        {
            return Math.E.ToString(CultureInfo.InvariantCulture);
        }

        public static string Pi(string anyValue)
        {
            return Math.PI.ToString(CultureInfo.InvariantCulture);
        }
        public static string Sin(string rad)
        {
            return Math.Sin(double.Parse(rad)).ToString(CultureInfo.InvariantCulture);
        }
        public static string Abs(string v)
        {
            dynamic cv = FT.Factory(v);
            return Math.Abs(cv.Value).ToString(CultureInfo.InvariantCulture);            
        }
        public static string ACos(string rad)
        {
            return Math.Acos(double.Parse(rad)).ToString(CultureInfo.InvariantCulture);
        }
        public static string Asin(string rad)
        {
            return Math.Asin(double.Parse(rad)).ToString(CultureInfo.InvariantCulture);
            
        }
        public static string Pow(string comaDelimitedArgs)
        {
            var r = comaDelimitedArgs.Split(Templates.Delimiter[0]);
            return Math.Pow(double.Parse(r[0]), double.Parse(r[1])).ToString(CultureInfo.InvariantCulture);
        }
        public static string Cos(string rad)
        {
            return Math.Cos(double.Parse(rad)).ToString(CultureInfo.InvariantCulture);
        }
    }
}
