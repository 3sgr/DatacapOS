using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace _3sgrMath
{
    //Datacap-specific functions (i.e. search, count, date/time format, etc.)
    public partial class Actions
    {
        //https://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx
        String[] formats = { "dd MMM yyyy hh:mm tt PST",
                           "dd MMM yyyy hh:mm tt PDT" };

        /// <summary>
        /// Read value stored in smart parameter
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private string ReadSmartParameter(string parameter)
        {
            var localSmartObj = new dcSmart.SmartNav(this);
            return parameter.StartsWith("@") ? localSmartObj.MetaWord(parameter) : parameter;
        }

        private bool CallFormulaProcessor(string param)
        {
            //decide if the formula is of type A+B or A=B+C
            var assignTo = "";
            var _formula = "";

            if (TestForAssignement(param, out assignTo, out _formula))
            {
            }
            return true;
        }

        /// <summary>
        /// Attempts to find "=" in formula that is not part of the string
        /// </summary>
        /// <param name="param">Expression to analyze</param>
        /// <param name="target">DCO Node that result of calculation to assign to</param>
        /// <param name="formula">'Distilled' formula</param>
        /// <returns></returns>
        public bool TestForAssignement(string param,  out string target, out string formula)
        {
            formula = param.Trim();
            target = "";
            if (!formula.Contains("="))
                return false;
            var ar = param.Split('='); //TODO: Replaces this parsing with more sophisticated that takes into consideration existance of '=' in string parameters and possibly validates the 'assgnTo' value
            if (string.IsNullOrEmpty(ar[0])|| string.IsNullOrEmpty(ar[1]))
                return false;
            target = ar[0].Trim();
            formula = ar[1].Trim();
            return true;
        }

        private bool CountXmlNodes(string parameter)
        {
            var bRes = true;
            try
            {
                var xPath = ReadSmartParameter(parameter);
                var sr = new StringReader(CurrentDCO.XML);
                XPathDocument doc = new XPathDocument(sr);
                var docNav = doc.CreateNavigator();
                var i = docNav.Select(xPath).Count;
                return true;

            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog("There was an exception: " + ex.Message);
                bRes = false;
            }

            return bRes;
        }
        public bool SumXmlNodes(string parameter)
        {
            var bRes = true;
            dcSmart.SmartNav localSmartObj = new dcSmart.SmartNav(this);

            try
            {
                var xPath = parameter.StartsWith("@") ? localSmartObj.MetaWord(parameter) : parameter;
                var sr = new StringReader(CurrentDCO.XML);
                XPathDocument doc = new XPathDocument(sr);
                var docNav = doc.CreateNavigator();
                var q = docNav.Compile(xPath);
                double res = (double)docNav.Evaluate(q);
                return true;
            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog("There was an exception: " + ex.Message);
                bRes = false;
            }

            localSmartObj = null;
            return bRes;
        }
        public bool SumFields(string parameter, string fieldName)
        {
            var bRes = true;
            dcSmart.SmartNav localSmartObj = new dcSmart.SmartNav(this);

            try
            {
                var xPath = parameter.StartsWith("@") ? localSmartObj.MetaWord(parameter) : parameter;
                var sr = new StringReader(CurrentDCO.XML);
                XPathDocument doc = new XPathDocument(sr);
                var docNav = doc.CreateNavigator();
                var q = docNav.Compile(xPath);
                for (var i = 0; i < CurrentDCO.NumOfChildren(); i++)
                {
                    var child = CurrentDCO.GetChild(i);
                    var type = child.ObjectType();
                    for (var k = 0; k < child.NumOfChildren(); k++)
                    {
                        var gChild = child.GetChild(k);
                        var gType = gChild.ObjectType();
                        for (var g = 0; g < gChild.NumOfChildren(); g++)
                        {
                            var ggChild = gChild.GetChild(g);
                            var ggType = ggChild.ObjectType();
                        }
                    }
                }
                double res = (double)docNav.Evaluate(q);
                return true;
            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog("There was an exception: " + ex.Message);
                bRes = false;
            }

            localSmartObj = null;
            return bRes;
        }
        public bool SelectNodes(string parameter)
        {
            var bRes = true;
            dcSmart.SmartNav localSmartObj = new dcSmart.SmartNav(this);

            try
            {
                var xPath = parameter.StartsWith("@") ? localSmartObj.MetaWord(parameter) : parameter;
                var sr = new StringReader(CurrentDCO.XML);
                XPathDocument doc = new XPathDocument(sr);
                var docNav = doc.CreateNavigator();
                var q = docNav.Compile(xPath);
                double res = (double)docNav.Evaluate(q);
                return true;
            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog("There was an exception: " + ex.Message);
                bRes = false;
            }

            localSmartObj = null;
            return bRes;
        }
    }
}
