using System;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.Resources;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public class BaseMath
    {
        public static string DoMath(dynamic a, dynamic b, string op)
        {
            try
            {
                switch (op)
                {//TODO: Possiblt add check for valid operation
                    case "^" :return (a.Value ^  b.Value).ToString();
                    case "/" :return (a.Value /  b.Value).ToString();
                    case "*" :return (a.Value *  b.Value).ToString();
                    case "-" :return (a.Value -  b.Value).ToString();
                    case "+" :return (a.Value +  b.Value).ToString();
                    case ">" :return (a.Value >  b.Value).ToString();
                    case "<" :return (a.Value <  b.Value).ToString();
                    case "<=":return (a.Value <= b.Value).ToString();
                    case "=<":return (a.Value <= b.Value).ToString();
                    case ">=":return (a.Value >= b.Value).ToString();
                    case "=>":return (a.Value >= b.Value).ToString();
                    case "==":return (a.Value == b.Value).ToString();
                    case "!=":return (a.Value != b.Value).ToString();
                    case "&" :return (Convert.ToBoolean (a.Value) & Convert.ToBoolean(b.Value)).ToString();  //String to Boolean and then compare
                    case "|" :return (Convert.ToBoolean(a.Value)  | Convert.ToBoolean(b.Value)).ToString();  //String to Boolean and then compare
                //  case "!" :                                                                                 It does not possible out very well, since the parameter is only one;
                    default: throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, op, a.Value.GetType(), b.Value.GetType()));
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, op, a.Value.GetType(), b.Value.GetType()),ex);
            }
        }
        public static string DoMath(dynamic a, string op)                                                // Option with one parameter for NOT
        {
          if (op == "!") {bool res = !Convert.ToBoolean(a.Value); return (Convert.ToString(res));}      // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!    
            else
          throw new InvalidOperationException();
        }
                                                                                                        // Option with one parameter for NOT
    }
}