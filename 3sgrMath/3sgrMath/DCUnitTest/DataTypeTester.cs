using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FTInt a = 2;
            FTInt b = 2;
            Assert.AreEqual(4,a + b);
        }
        [TestMethod]
        public void SubstractDoubleFromString()
        {
            FTDouble a = 2;
            FTString b = "4";
            try
            {
                var r = b - a;
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(ex.Message, "Operator '-' cannot be applied to operands of type 'System.String' and 'System.Double'.");
            }
        }
    }
}
