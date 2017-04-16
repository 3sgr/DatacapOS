using dcrroLib;

namespace DCUnitTest
{
    class MyRRState:IRRState
    {
        public bool GetDCOBody(string bstrVictim, out string pbstrXMLBody)
        {
            throw new System.NotImplementedException();
        }

        public bool SetDCOBody(string bstrVictim, string bstrXMLBody)
        {
            throw new System.NotImplementedException();
        }

        public bool Push(string bstrDataID, object vData)
        {
            throw new System.NotImplementedException();
        }

        public bool Pop(string bstrDataID, out object pvData)
        {
            throw new System.NotImplementedException();
        }

        public void ClearData()
        {
            throw new System.NotImplementedException();
        }

        public string LoadString(string bstrRRXName, string bstrStringName, string bstrDefValue)
        {
            throw new System.NotImplementedException();
        }

        public bool RRXDLLID(long n64KeyPairLeft, long n64KeyPairRight)
        {
            throw new System.NotImplementedException();
        }

        public bool SecureCall(int n64Key1, int n64Key2, int n64Key3, int n64Key4, string bstrValIn, out object pbvarValOut)
        {
            throw new System.NotImplementedException();
        }

        public bool SecureCallAA(string bstrKeyPairLeft, string bstrKeyPairRight, string bstrValIn, out object pbvarValOut)
        {
            throw new System.NotImplementedException();
        }

        public IRRError Error { get; }
        public string Props { get; set; }
        public string XML { get; set; }
        public bool Interactive { get; }
        public bool DebugMode { get; }
        public string Victim { get; set; }
        public ELogicResult LogicResult { get; set; }
        public string OnRuleStart { get; set; }
        public string OnRuleFailure { get; set; }
        public string OnRuleSuccess { get; set; }
        public object get_Data(string bstrDataID)
        {
            throw new System.NotImplementedException();
        }

        public void set_Data(string bstrDataID, object pVal)
        {
            throw new System.NotImplementedException();
        }

        public string OnRulesetStart { get; set; }
        public string OnRulesetEnd { get; set; }
        public bool SkipChildren { get; set; }
        public bool SkipClosures { get; set; }
        public string OnProcessStart { get; set; }
        public string OnProcessEnd { get; set; }
        public string OnFunctionStart { get; set; }
        public string OnFunctionSuccess { get; set; }
        public string OnFunctionFailure { get; set; }
        public string OnActionStart { get; set; }
        public string OnActionSuccess { get; set; }
        public string OnActionFailure { get; set; }
        public string OnActionAbort { get; set; }
        public string OnActionStop { get; set; }
        public string CurrentDCOLoadList { get; }
        public int Flags { get; set; }
        public object Applications { get; }
        public object Application { get; }
        public string ApplicationName { get; }
        public string WorkflowName { get; }
        public object aTM { get; }
        public EExecMode ExecMode { get; set; }
    }
}