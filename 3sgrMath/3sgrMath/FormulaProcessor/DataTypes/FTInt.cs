using System.Globalization;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public class FTInt : BaseOperand<int>
    {
        public FTInt(int value) : base(value)
        {
        }
        public static implicit operator FTInt(int val)
        {
            return new FTInt(val);
        }
        public static implicit operator string (FTInt val)
        {
            return new FTDouble(val.Value).Value.ToString(CultureInfo.InvariantCulture);
        }
        public override string ToString()
        {
            return Value.ToString();
        }
        public static int operator +(FTInt a, FTInt b)
        {
            return a.Value + b.Value;
        }
    }
}