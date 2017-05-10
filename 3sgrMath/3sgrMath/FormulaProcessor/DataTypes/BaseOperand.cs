using System;
using System.Collections.Generic;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.Resources;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public class BaseOperand<T>
    {
        protected bool Equals(BaseOperand<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((BaseOperand<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public T Value { get; }
        public BaseOperand()
        {
        }
        public BaseOperand(T value)
        {
            Value = value;
        }
        #region Arithmetic
        
        #endregion

        public override string ToString()
        {
            return Value.ToString();
        }
    }
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
