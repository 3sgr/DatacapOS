using System;
using System.Globalization;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public class FTDouble : BaseOperand<double>
    {
        public FTDouble(double value) : base(value)
        {
        }
        public static implicit operator FTDouble(double val)
        {
            return new FTDouble(val);
        }
        public static implicit operator string(FTDouble val)
        {
            return new FTDouble(val.Value).Value.ToString(CultureInfo.InvariantCulture);
        }
#region OverrideOperator+
        public static string operator +(FTDouble a, FTDouble b)
        {
            return (a.Value + b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public static string operator +(FTDouble a, FTInt b)
        {
            return (a.Value + b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public static string operator +(FTInt a, FTDouble b)
        {
            return (a.Value + b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public static string operator +(FTDouble a, FTString b)
        {
            return (a.Value + b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public static string operator +(FTString a, FTDouble b)
        {
            return (a.Value + b.Value).ToString(CultureInfo.InvariantCulture);
        }
        #endregion
        #region OverrideOperator-
        public static string operator -(FTDouble a, FTDouble b)
        {
            return (a.Value - b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public static string operator -(FTDouble a, FTInt b)
        {
            return (a.Value - b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public static string operator -(FTInt a, FTDouble b)
        {
            return (a.Value - b.Value).ToString(CultureInfo.InvariantCulture);
        }
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
        #endregion
    }
}