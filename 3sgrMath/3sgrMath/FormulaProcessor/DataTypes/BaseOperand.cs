using System.Collections.Generic;

namespace SSSGroup.Utilites.FormulaProcessor.DataTypes
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
            return obj.GetType() == GetType() && Equals((BaseOperand<T>)obj);
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
}