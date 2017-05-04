using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;

namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
{
    public class NewBaseFormatTranslator : BaseFormatTranslator
    {
        public override object Factory(string value)
        {
            double d;
            int i;
            if (double.TryParse(value, out d))
                return new FTDouble(d);
            if (int.TryParse(value, out i))
                return new FTInt(i);
            return new FTString(value);
        }
    }
}
