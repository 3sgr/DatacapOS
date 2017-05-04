using System;
using System.Globalization;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.Resources;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public class FTString : BaseOperand<string>
    {
        public FTString(string value) : base(value)
        {
        }
        public static implicit operator FTString(string val)
        {
            return new FTString(val);
        }
        public static implicit operator string(FTString val)
        {
            return new FTString(val.Value).Value;
        }

        #region OperatorOverride+
        public static string operator +(FTString a, FTString b)
        {
            return (a.Value + b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public static string operator +(FTString a, FTDouble b)
        {
            return a + b.ToString();
        }
        public static string operator +(FTInt a, FTString b)
        {
            return a.ToString() + b;
        }
        public static string operator +(FTString a, FTInt b)
        {
            return a + b.ToString();
        }
        public static string operator +(FTDouble a, FTString b)
        {
            return a.ToString() + b;
        }            
        #endregion
        #region OperatorOverride-
        public static string operator -(FTString a, FTString b)
        {
            throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, "-", a.GetTypeName(),b.GetTypeName()));
        }
        public static string operator -(FTString a, FTDouble b)
        {
            throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, "-", a.GetTypeName(),b.GetTypeName()));
        }
        public static string operator -(FTDouble a, FTString b)
        {
            throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, "-", a.GetTypeName(),b.GetTypeName()));
        }
        public static string operator -(FTInt a, FTString b)
        {
            throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, "-", a.GetTypeName(),b.GetTypeName()));
        }
        public static string operator -(FTString a, FTInt b)
        {
            throw new InvalidOperationException(string.Format(ErrorEx.InvalidOperation, "-", a.GetTypeName(),b.GetTypeName()));
        }
        #endregion
        public override string ToString()
        {
            return Value;
        }
    }
}