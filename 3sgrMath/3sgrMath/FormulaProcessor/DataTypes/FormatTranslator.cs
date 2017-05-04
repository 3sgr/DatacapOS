namespace SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes
{
    public abstract class BaseFormatTranslator
    {
        public abstract object Factory(string value);
    }
    public class FormatTranslator : BaseFormatTranslator
    {
        public dynamic GetType(string value)
        {
            int i;
            if (int.TryParse(value, out i)) return typeof (FTInt);
            double d;
            return double.TryParse(value, out d) ? typeof(FTDouble) : typeof (FTString);
            //return 
        }

        public override object Factory(string value)
        {
            int i;
            if (int.TryParse(value, out i)) return new FTInt(i);
            double d;
            if (double.TryParse(value, out d)) return new FTDouble(d);

            if (value.Contains(@"""")) value = value.Replace(@"""", "");
            return new FTString(value);
        }
    }
}