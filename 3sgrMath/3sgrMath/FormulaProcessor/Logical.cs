//using System;
//using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;
//namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
//{
//    class Logical
//    {
//        #region Operation
//        public string ProcesScale(string left, string sign, string right, FormatTranslator baseFormat)
//        {
//            string res = "";
//            switch (sign)
//            {
//             case ">" : More    (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
//             case "<" : Less    (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
//             case ">=": MoreEq  (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
//             case "<=": LessEq  (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
//             case "==": Equal   (baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
//             case "!=": NotEqual(baseFormat.Factory(left), baseFormat.Factory(right), out res); break;
//             case "!" : NotAtAll(baseFormat.Factory(left),                            out res); break;
//
//             default: res = ContinuationLg(left, sign, right, baseFormat); break;
//            }
//            return res;
//        }
//        public static Func<int, int> More(object a, object b, out string res)
//        {
//            res = ((dynamic)a > (dynamic)b).ToString();
//            return null;
//        }
//        public static Func<int, int> Less(object a, object b, out string res)
//        {
//            res = ((dynamic)a < (dynamic)b).ToString();
//            return null;
//        }
//        public static Func<int, int> MoreEq(object a, object b, out string res)
//        {
//            res = ((dynamic)a >= (dynamic)b).ToString();
//            return null;
//        }
//        public static Func<int, int> LessEq(object a, object b, out string res)
//        {
//            res = ((dynamic)a <= (dynamic)b).ToString();
//            return null;
//        }
//        public static Func<int, int> Equal(object a, object b, out string res)
//        {
//            res = ((dynamic)a == (dynamic)b).ToString();
//            return null;
//        }
//        public static Func<int, int> NotEqual(object a, object b, out string res)
//        {
//            res = ((dynamic)a != (dynamic)b).ToString();
//            return null;
//        }
//        public static Func<int, int> NotAtAll(object a, out string res)
//        {
//            res = (!(dynamic)a.ToString());
//            return null;
//        }
//        #endregion
//        public string ContinuationLg(string left, string sign, string right, FormatTranslator baseFormat)
//        {
//            return "!!!";
//        }
//    }
//
//}