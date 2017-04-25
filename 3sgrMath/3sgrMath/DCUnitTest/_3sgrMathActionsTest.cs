//
//Copyright(c) 2017 SSS Group Ltd.
//3sgr.com
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
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
                var res = oActions.TestForAssignement(kvp.Key, out target, out formula);
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
        public void RunAction()
        {
            Assert.IsTrue(oActions.ProcessFormula("test1"));
        }
        [TestMethod]
        public void CountNodes()
        {
            oActions.CountXmlNodes("*//P");
            oActions.SumXmlNodes("sum(//B/D/P/V[@n='STATUS']/text())");
            oActions.SumXmlNodes("sum(*//V[@n='STATUS']/text())");
            oActions.SumASCII("//B/D/P/F[@id='Invoice_Total']");
        }
    }
}
