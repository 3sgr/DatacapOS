using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.DataTypes;

namespace DCUnitTest
{
    [TestClass]
    public class DataTypeTester
    {
        [TestMethod]
        public void TwoIntSum()
        {
            var ft = new FormatTranslator();
            var a = "2";
            var b = "2";
            Assert.AreEqual("4", BaseMath.DoMath(ft.Factory(a), ft.Factory(b), "+"));
        }
        [TestMethod]
        public void SubstractDoubleFromString()
        {
            var ft = new FormatTranslator();
            try
            {
                var rr = BaseMath.DoMath(ft.Factory("4"), ft.Factory("4.4"),"*");
                Debug.WriteLine($"4*4.4={rr}");
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(ex.Message, "Operator '-' cannot be applied to operands of type 'System.String' and 'System.Double'.");
            }
            try
            {
                BaseMath.DoMath(ft.Factory("asd"), ft.Factory("asd"), "*");
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine(ex.Message);
                Assert.AreEqual(ex.Message, "Operator '*' cannot be applied to operands of type 'System.String' and 'System.String'.");
            }
        }
    }
}
