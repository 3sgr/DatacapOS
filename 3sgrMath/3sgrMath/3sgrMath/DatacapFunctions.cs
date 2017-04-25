using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using SSSGroup.Datacap.CustomActions._3sgrMath.Resources;

namespace SSSGroup.Datacap.CustomActions._3sgrMath
{
    public partial class Actions
    {
        public void GenerateMasterXML()
        {
            if (!Changed)
                return;
            Changed = false;
            _masterDoc = new XmlDocument();
            _masterDoc.LoadXml(CurrentDCO.XML);
            var xnList = _masterDoc.SelectNodes(Const.DefaultSelectNodes);
            if (xnList == null) return;
            foreach (XmlNode xn in xnList)
            {
                var xmlNodeList = xn.SelectNodes(Const.DataFileAttributeSelectNodes);
                if (xmlNodeList != null)
                {
                    var fullPath = xmlNodeList[0].InnerText;
                    var pageXML = new XmlDocument();
                    pageXML.Load(fullPath);

                    var pageFragment = _masterDoc.CreateDocumentFragment();
                    if (2 > pageXML.ChildNodes.Count)
                        continue;
                    pageFragment.InnerXml = pageXML.ChildNodes[1].InnerXml;
                    xn.AppendChild(pageFragment);
                }
                //.AppendChild(copiedNode);
                Console.WriteLine(xn.InnerText);
            }
        }
        public int CountXmlNodes(string parameter)
        {
            GenerateMasterXML();
            var localSmartObj = new dcSmart.SmartNav(this);
            try
            {
                var xPath = parameter.StartsWith("@") ? localSmartObj.MetaWord(parameter) : parameter;
                var doc = new XPathDocument(new XmlNodeReader(_masterDoc));
                var docNav = doc.CreateNavigator();
                return docNav.Select(xPath).Count;
            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog(string.Format(Messages.Exception,ex.Message));
            }
            return 0;
        }
        public string SumXmlNodes(string parameter)
        {
            GenerateMasterXML();
            var localSmartObj = new dcSmart.SmartNav(this);

            try
            {
                var xPath = parameter.StartsWith("@") ? localSmartObj.MetaWord(parameter) : parameter;
                var doc = new XPathDocument(new XmlNodeReader(_masterDoc));
                var docNav = doc.CreateNavigator();
                var q = docNav.Compile(xPath);
                var o = docNav.Evaluate(q);
                return o?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog(string.Format(Messages.Exception, ex.Message));
            }
            return string.Empty;
        }
        public string SumASCII(string parameter)
        {
            GenerateMasterXML();
            var localSmartObj = new dcSmart.SmartNav(this);
            try
            {
                var xPath = parameter.StartsWith("@") ? localSmartObj.MetaWord(parameter) : parameter;
                var selection = _masterDoc.SelectNodes(xPath);
                double sum = 0;
                foreach (XmlNode node in selection)
                {
                    var str = ConvertFromASCII(node);
                    sum += string.IsNullOrEmpty(str) ? 0 : Convert.ToDouble(str);
                }
                return sum.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog(string.Format(Messages.Exception, ex.Message));
            }
            return string.Empty;
        }

        private string ConvertFromASCII(XmlNode node)
        {
            var sb = new StringBuilder();
            var xmlNodeList = node.SelectNodes("./ C");
            if (xmlNodeList == null) return sb.ToString();
            foreach (XmlNode ch in xmlNodeList)
            {
                sb.Append((char)(Convert.ToInt32(ch.InnerText)));
            }
            return sb.ToString();
        }
        //TODO: Replace with actual implementation
        public bool TestForAssignement(string theory, out string target, out string formula)
        {
            target = "";
            formula = "";
            return true;
        }
    }
}
