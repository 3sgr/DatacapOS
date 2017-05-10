namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public abstract class BaseFormatTranslator
    {
        public abstract object Factory(string value);
    }
    public class FormatTranslator : BaseFormatTranslator
    {
        public override object Factory(string value)
        {
            int i;
            if (int.TryParse(value, out i)) return new BaseOperand<int>(i);
            double d;
            if (double.TryParse(value, out d)) return new BaseOperand<double>(d);
            if (value.Contains(@"""")) value = value.Replace(@"""", "");
            return new BaseOperand<string>(value);
        }
    }
}