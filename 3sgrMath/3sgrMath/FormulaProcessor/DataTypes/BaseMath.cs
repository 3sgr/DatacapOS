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
                    case "^": return (a.Value ^ b.Value).ToString();
                    case "/": return (a.Value / b.Value).ToString();
                    case "*": return (a.Value * b.Value).ToString();
                    case "-": return (a.Value - b.Value).ToString();
                    case "+": return (a.Value + b.Value).ToString();
                    case ">": return (a.Value > b.Value).ToString();
                    case "<": return (a.Value < b.Value).ToString();
                    case "<=":return (a.Value <= b.Value).ToString();
                    case "=<":return (a.Value <= b.Value).ToString();
                    case ">=": return (a.Value >= b.Value).ToString();
                    case "=>": return (a.Value >= b.Value).ToString();
                    case "==": return (a.Value == b.Value).ToString();
                    case "!=":return (a.Value != b.Value).ToString();
                    case "&": return (a.Value & b.Value).ToString();
                    case "|":return (a.Value | b.Value).ToString();
                    //! - figure out what/how to deal with NOT
                    default: throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, op, a.Value.GetType(), b.Value.GetType()));
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, op, a.Value.GetType(), b.Value.GetType()),ex);
            }
        }
    }
}