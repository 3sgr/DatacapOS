using System;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public class BaseOperand<T>
    {
        public T Value { get; set; }
        public BaseOperand()
        {
        }
        public BaseOperand(T value)
        {
            Value = value;
        }
        public string GetTypeName()
        {
            return Value.GetType().ToString();
        }

        #region Arithmetic
        public static BaseOperand<T> operator +(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(Sum(op1.Value, op2.Value));
        }
        public static BaseOperand<T> operator -(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(Dif(op1.Value, op2.Value));
        }
        public static BaseOperand<T> operator *(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(Mul(op1.Value, op2.Value));
        }
        public static BaseOperand<T> operator /(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(Div(op1.Value, op2.Value));
        }
        private static T Sum(T a, T b)
        {
            return (dynamic)a + (dynamic)b;
        }
        private static T Dif(T a, T b)
        {
            return (dynamic)a - (dynamic)b;
        }
        private static T Mul(T a, T b)
        {
            return (dynamic)a * (dynamic)b;
        }
        private static T Div(T a, T b)
        {
            return (dynamic)a / (dynamic)b;
        }
        #endregion

        public override string ToString()
        {
            return Value.ToString();
        }

        #region Logical
        public static BaseOperand<T> operator >(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(MoreThan(op1.Value, op2.Value));
        }
        private static T MoreThan(T a, T b)
        {
            return ((dynamic)a < (dynamic)b);
        }
        public static BaseOperand<T> operator <(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(LessThan(op1.Value, op2.Value));
        }
        private static T LessThan(T a, T b)
        {
            return ((dynamic)a > (dynamic)b);
        }
        public static BaseOperand<T> operator >=(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(MoreEqual(op1.Value, op2.Value));
        }
        private static T MoreEqual(T a, T b)
        {
            return ((dynamic)a >= (dynamic)b);
        }
        public static BaseOperand<T> operator <=(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(LessEqual(op1.Value, op2.Value));
        }
        private static T LessEqual(T a, T b)
        {
            return ((dynamic)a <= (dynamic)b);
        }

        public static BaseOperand<T> operator == (BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(FullEqual(op1.Value, op2.Value));
        }
        private static T FullEqual(T a, T b)
        {
            return ((dynamic)a == (dynamic)b);
        }
        public static BaseOperand<T> operator !=(BaseOperand<T> op1, BaseOperand<T> op2)
        {
            return new BaseOperand<T>(NotEqual(op1.Value, op2.Value));
        }
        private static T NotEqual(T a, T b)
        {
            return ((dynamic)a != (dynamic)b);
        }
        public static BaseOperand<T>  operator ! (BaseOperand<T> op1 )
        {
            return new BaseOperand<T>(NotAll(op1.Value));
        }
        private static T NotAll(T a )
        {
            return (!(dynamic)a);
        }

        #endregion
    }
}
