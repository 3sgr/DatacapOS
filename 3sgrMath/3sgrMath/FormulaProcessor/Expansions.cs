//using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;
//
//namespace SSSGroup.Datacap.CustomActions.FormulaProcessor
//{
//    public class NewBaseFormatTranslator : BaseFormatTranslator
//    {
//        public override object Factory(string value)
//        {
//            int i;
//            if (int.TryParse(value, out i)) return new BaseOperand<int>(i);
//            double d;
//            if (double.TryParse(value, out d)) return new BaseOperand<double>(d);
//
//            if (value.Contains(@"""")) value = value.Replace(@"""", "");
//            return new BaseOperand<string>(value);
//        }
//    }
//}
