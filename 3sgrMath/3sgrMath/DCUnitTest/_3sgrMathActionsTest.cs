using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

// ReSharper disable InconsistentNaming

namespace DCUnitTest
{
    [TestClass]
    public class _3SgrMathActionsTest : BaseTestClass
    {
        [TestMethod]
        public void TestForAssignementTest()
        {
            var testCases = new Dictionary<string, string[]>
            {
                {"A=B+C", new[] {"A", "B+C", "True"}},
                {" A = B + C ", new[] {"A", "B + C", "True"}},
                {" B + C ", new[] {"", "B + C", "False"}},
                {"asdasdasd=Basdasdasda+Casdasdasd+asdasda-asda/asdasd", new[] { "asdasdasd", "Basdasdasda+Casdasdasd+asdasda-asda/asdasd", "True"}},
                {"AAA=B+C", new[] { "AAA", "B+C", "True"}},
                {"A=", new[] {"A", "B", "False"}}
            };
            foreach (var kvp in testCases)
            {
                var formula = "";
                var target = "";
                var res = ActionsInst.TestForAssignement(kvp.Key, out target, out formula);
                    Assert.AreEqual(res, Convert.ToBoolean(kvp.Value[2]));

                if (!res)
                {
                    Debug.WriteLine($"test: '{kvp.Key}', function call result:'{false}'");
                    continue;
                }
                Debug.WriteLine($"test: '{kvp.Key}', function call result:'{true}', target:'{target}', formula:'{formula}'");

                Assert.AreEqual(target, kvp.Value[0]);
                Assert.AreEqual(formula, kvp.Value[1]);
            }
        }
    }
}
