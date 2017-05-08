using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSSGroup.Datacap.CustomActions.FormulaProcessor;

namespace DCUnitTest
{
    [TestClass]
    public class FormulaProcessorTester
    {
        public Dictionary<string, string> FormulaDataSet = new Dictionary<string, string>()
        {
            {"\"3\"+\"2\"+\"1\"+(\"11\"+\"12\"+\"13\")", "111534"},
            {"\"3\"+\"2\"+\"2\"", "322" },
            {"\"3\"+\"2\"", "32"},
            {"3+2", "5"},
            {" 3 + 2 ", "5"},
            {" 3+2 ", "5"},
            {"3+2+1+(11+12+13)", "42"},
            {"(3+2+1+(11+12+13))", "42"},
            {"((3+2+1+(11+12+13)))", "42"},
            {"3-2", "1"},
            {"3*2", "6"},
        };

       [TestMethod]
        public void FormulaReduceTest()
        {
            var ps = new Parser { MainFormula = "\"3\"+\"2\"+\"2\"" };
            Assert.AreEqual(ps.MainFormula, "(?1+?2+?3)");
            ps = new Parser { MainFormula = "3+2+2" };
            Assert.AreEqual(ps.MainFormula, "(?1+?2+?3)");
            ps = new Parser { MainFormula = "(3+2+2)" };
            Assert.AreEqual(ps.MainFormula, "(?1+?2+?3)");
            ps = new Parser { MainFormula = "( 3 + 2 + 2 )" };
            Assert.AreEqual(ps.MainFormula, "(?1+?2+?3)");
            ps = new Parser { MainFormula = "3+2+1+(11+12+13)" };
            Assert.AreEqual(ps.MainFormula, "(?1+?2+?3+(?4+?5+?6))");
        }
       
        [TestMethod]
        public void SimpleFormulaTest()
        {
            foreach (var test in FormulaDataSet)
            {
                try
                {
                    Debug.WriteLine($"testing formula:'{test.Key}' expected result:{test.Value}");
                    var ps = new Parser {MainFormula = test.Key};
                    Assert.AreEqual(ps.SubstringPush(), test.Value);
                    Debug.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed. Exception:{ex.Message}");
                    throw;
                }
            }
        }
    }
}
