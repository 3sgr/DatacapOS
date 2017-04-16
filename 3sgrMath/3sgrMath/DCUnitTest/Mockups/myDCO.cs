using System;
using TDCOLib;

namespace DCUnitTest
{
    class MyDco : IDCO
    {
        int ICustomTMDco.Setup(string lpszDCOID)
        {
            throw new NotImplementedException();
        }

        string IDCO.Options
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ID { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string BatchDir { get; set; }
        public int BatchPriority { get; set; }
        public string ImageName { get; set; }
        public string Text { get; set; }
        public string ConfidenceString { get; set; }
        public string get_Variable(string lpszName)
        {
            throw new NotImplementedException();
        }

        public void set_Variable(string lpszName, string pVal)
        {
            throw new NotImplementedException();
        }

        public int get_CharValue(int nIndex)
        {
            throw new NotImplementedException();
        }

        public void set_CharValue(int nIndex, int pVal)
        {
            throw new NotImplementedException();
        }

        public int get_CharConfidence(int nIndex)
        {
            throw new NotImplementedException();
        }

        public void set_CharConfidence(int nIndex, int pVal)
        {
            throw new NotImplementedException();
        }

        public string get_AltText(int nIndex)
        {
            throw new NotImplementedException();
        }

        public void set_AltText(int nIndex, string pVal)
        {
            throw new NotImplementedException();
        }

        public string get_AltConfidenceString(int nIndex)
        {
            throw new NotImplementedException();
        }

        public void set_AltConfidenceString(int nIndex, string pVal)
        {
            throw new NotImplementedException();
        }

        public int get_OMRValue(int nIndex)
        {
            throw new NotImplementedException();
        }

        public void set_OMRValue(int nIndex, int pVal)
        {
            throw new NotImplementedException();
        }

        public string XML { get; set; }

        public bool Clear()
        {
            throw new NotImplementedException();
        }

        public bool CreateDocuments()
        {
            throw new NotImplementedException();
        }

        public bool AddVariable(string strName, object newValue)
        {
            throw new NotImplementedException();
        }

        public bool AddVariableFloat(string strName, double fValue)
        {
            throw new NotImplementedException();
        }

        public bool AddVariableInt(string strName, int nValue)
        {
            throw new NotImplementedException();
        }

        public bool AddVariableString(string strName, string strValue)
        {
            throw new NotImplementedException();
        }

        public int CheckIntegrity(out object pLastChecked)
        {
            throw new NotImplementedException();
        }

        public bool DeleteChild(int nIndex)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVariable(string lpszName)
        {
            throw new NotImplementedException();
        }

        public DCO FindChild(string lpszName)
        {
            throw new NotImplementedException();
        }

        public int FindChildIndex(string lpszName)
        {
            throw new NotImplementedException();
        }

        public int FindVariable(string lpszName)
        {
            throw new NotImplementedException();
        }

        public DCO GetChild(int nIndex)
        {
            throw new NotImplementedException();
        }

        public DCO Parent()
        {
            throw new NotImplementedException();
        }

        public bool GetPosition(out object pnLeft, out object pnTop, out object pnRight, out object pnBottom)
        {
            throw new NotImplementedException();
        }

        public bool MoveChild(int nOldIndex, int nNewIndex)
        {
            throw new NotImplementedException();
        }

        public int NumOfChildren()
        {
            throw new NotImplementedException();
        }

        public int NumOfVars()
        {
            throw new NotImplementedException();
        }

        public int ObjectType()
        {
            throw new NotImplementedException();
        }

        public bool Read(string lpszFileName)
        {
            throw new NotImplementedException();
        }

        public bool ReadSetup(string lpszFileName)
        {
            throw new NotImplementedException();
        }

        public bool SetPosition(int nLeft, int nTop, int nRight, int nBottom)
        {
            throw new NotImplementedException();
        }

        public DCOSetupNode SetupNode()
        {
            throw new NotImplementedException();
        }

        public DCOSetup SetupObject()
        {
            throw new NotImplementedException();
        }

        public bool Write(string lpszFileName)
        {
            throw new NotImplementedException();
        }

        public bool WriteSetup(string lpszFileName)
        {
            throw new NotImplementedException();
        }

        public DCO AddChild(int nType, string lpszID, int nIndex)
        {
            throw new NotImplementedException();
        }

        public bool ShowSetupDialog(string lpszFileName)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public string GetVariableName(int nIndex)
        {
            throw new NotImplementedException();
        }

        public object GetVariableValue(int nIndex)
        {
            throw new NotImplementedException();
        }

        public bool MoveIn(object pNewParent, int nIndex)
        {
            throw new NotImplementedException();
        }

        public int AddValue(int nValue, int nConfidence)
        {
            throw new NotImplementedException();
        }

        public bool DeleteValue(int nIndexValue)
        {
            throw new NotImplementedException();
        }

        public bool IsError()
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public bool CreateFields()
        {
            throw new NotImplementedException();
        }

        public object RunScript(string lpszFuncName, ref object pParams)
        {
            throw new NotImplementedException();
        }

        public string GetRoute(bool bRuntime)
        {
            throw new NotImplementedException();
        }

        public DCO FindRouteChild(string bszRoute)
        {
            throw new NotImplementedException();
        }

        public bool IsRoute(string lpszRoute)
        {
            throw new NotImplementedException();
        }

        int IDCO.Setup(string lpszDCOID)
        {
            throw new NotImplementedException();
        }

        string ICustomTMDco.Options
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}