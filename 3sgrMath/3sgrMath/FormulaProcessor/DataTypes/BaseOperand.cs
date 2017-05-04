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
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
