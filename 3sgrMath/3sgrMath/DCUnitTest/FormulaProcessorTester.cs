using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSSGroup.Datacap.CustomActions.FormulaProcessor;
using SSSGroup.Datacap.CustomActions.FormulaProcessor.Resources;

namespace DCUnitTest
{
    [TestClass]
    public class FormulaProcessorTester
    {
        public Dictionary<string, string> FormulaFuncDataSet = new Dictionary<string, string>()
        {
            {"pow(2,3)", "8"},
            {"abs(-3)", "3"},
            {"sin(0)", "0"},
            {"cos(0)", "1"},
            {"(12+12)>pow(2,3)", "True"},
            {"pow(2,3)==2+2*2+2", "True"},
            {"pow( 2 , 3 ) == 2 + 2 * 2 + 2 ", "True"},
        };
        public Dictionary<string, string> FormulaDataSet = new Dictionary<string, string>()
        {
            {"!(2>3)|(7<3)&(4==4)","True"},
            { "!(2>3)","True"},
            {"(2>1)&(0<1)","True" },
            {"(2>3)|(0<0)","False" },
            { "\"3\"+\"2\"+\"1\"+(\"11\"+\"12\"+\"13\")", "111534"},
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
            {"2-3", "-1"},
            {"2+2*2", "6"},
            {"(2+2)*2", "8"},
            {"2^3", "1"}, // ^ is bitwise XOR
            {"2*2==2+2", "True"}
        };
        public Dictionary<string, string> FormulaReduceDataSet = new Dictionary<string, string>()
        {
            {"\" 3 \"+\" 2 \"+\"2\"", "\" 3 \"\" 2 \"+\"2\"+"},
            {"2+2*2", "222*+"},
            {"(2+2)*2", "22+2*"},
            {"1==1", "11=="},
            {"\"3\"+\"2\"+\"2\"", "\"3\"\"2\"+\"2\"+"},
            {"3+2+2", "32+2+" },
            {"\"3\"+\"2\"", "\"3\"\"2\"+"},
            {"(3+2+2)", "32+2+"},
            {"( 3 + 2 + 2 )", "32+2+"},
            {"( 3 + 5 * 7 - 2 )", "357*+2-"},
            {"3+2+1+(11+12+13)", "32+1+1112+13++"},
            {@"1^2/3*4-5+6<7>8==9=>10>=11<=12=<13!=14&15|16!17", @"12^3/4*5-6+789101112131415&!==<<=>==>==><1617!|" },
        };

        [TestMethod]
        public void TestStringRegexMatch()
        {
            //match everything between paranthesis for function argument.
            var r = new Regex(Templates.RegexParentMatch, RegexOptions.IgnorePatternWhitespace);
            var query = @"count(*//P)+sum(count(count(*//P)))";
            //var query = @"funcPow((3),2) * (9+1)";
            var matches= r.Matches(query);
            foreach (var match in matches)
            {
                Debug.WriteLine(match);
            }
        }
        [TestMethod]
        public void FormulaParceTest()
        {
            foreach (var test in FormulaReduceDataSet)
            {
                try
                {
                    Debug.WriteLine($"testing formula:'{test.Key}' expected result:{test.Value}");
                    using (var reader = new StringReader(test.Key))
                    {
                        var parser = new Parser();
                        var tokens = parser.Tokenize(reader).ToList();
                        //Console.WriteLine(string.Join("\n", tokens));
                        var str = parser.Sort(tokens).Aggregate("", (current, t) => current + t.Value);
                        Assert.AreEqual(test.Value, str);
                    }
                    Debug.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed. Exception:{ex.Message}");
                    throw;
                }
            }
        }

        [TestMethod]
        public void SimpleFormulaTest()
        {
            foreach (var test in FormulaDataSet)
            {
                try
                {
                    Debug.WriteLine($"testing formula:'{test.Key}' expected result:{test.Value}");
                    var parser = new Parser();
                    using (var reader = new StringReader(test.Key))
                    {
                        var tokens = parser.Tokenize(reader).ToList();
                        //Console.WriteLine(string.Join("\n", tokens));
                        var res = Calculator.Evaluate(parser.Sort(tokens), Functions.MathFunctions);
                        Assert.AreEqual(test.Value, res);
                        Debug.WriteLine("Success");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed. Exception:{ex.Message}");
                    throw;
                }
            }
        }

        [TestMethod]
        public void FunctionFormulaTest()
        {
            var tok = "pow(2,3)";
            var fName = tok.Remove(tok.IndexOf("("));
            var fArg = tok.Remove(0, tok.IndexOf("(")+1);
            fArg = fArg.Remove(fArg.LastIndexOf(")"));
            tok = "abs(-1)";
            fName = tok.Remove(tok.IndexOf("("));
            fArg = tok.Remove(0, tok.IndexOf("(")+1);
            fArg = fArg.Remove(fArg.LastIndexOf(")"));
            tok = "Pi()";
            fName = tok.Remove(tok.IndexOf("("));
            fArg = tok.Remove(0, tok.IndexOf("(")+1);
            fArg = fArg.Remove(fArg.LastIndexOf(")"));

            foreach (var test in FormulaFuncDataSet)
            {
                
                try
                {
                    Debug.WriteLine($"testing formula:'{test.Key}' expected result:{test.Value}");
                    var parser = new Parser();
                    using (var reader = new StringReader(test.Key))
                    {
                        var tokens = parser.Tokenize(reader).ToList();
                        //Console.WriteLine(string.Join("\n", tokens));
                        var sorted = parser.Sort(tokens);
                        var res = Calculator.Evaluate(sorted, Functions.MathFunctions);
                        Assert.AreEqual(test.Value, res);
                        Debug.WriteLine("Success");
                    }
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
