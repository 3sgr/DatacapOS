using PILOTCTRLLib;

namespace DCUnitTest
{
    public class MyBPilotCtrl : IBPilotCtrl
    {
        public void BatchContinue()
        {
            throw new System.NotImplementedException();
        }

        public void BatchStop(short eStatus)
        {
            throw new System.NotImplementedException();
        }

        public int LoadObject(object pObj)
        {
            throw new System.NotImplementedException();
        }

        public void ChildrenChanged(object pParent)
        {
            throw new System.NotImplementedException();
        }

        public void SetupStop(short eState)
        {
            throw new System.NotImplementedException();
        }

        public short MsgLog(string Message, object SevLevel = null, object MsgID = null)
        {
            throw new System.NotImplementedException();
        }

        public int SetProfileString(string Section, string Key, string Value, string File)
        {
            throw new System.NotImplementedException();
        }

        public string GetProfileString(string Section, string Key, string Default, string File)
        {
            throw new System.NotImplementedException();
        }

        public int MsgStatus(string Message, object PaneID = null)
        {
            throw new System.NotImplementedException();
        }

        public object GetBoundObject(object Control)
        {
            throw new System.NotImplementedException();
        }

        public int IsObjectEmpty(object varObject)
        {
            throw new System.NotImplementedException();
        }

        public void MsgStatistic(string StatString)
        {
            throw new System.NotImplementedException();
        }

        public void SetStatSQL(string SQLString)
        {
            throw new System.NotImplementedException();
        }

        public void SetTreeColumn(short nIndex, short eObjType, string sVarName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTreeColumn(short nIdx)
        {
            throw new System.NotImplementedException();
        }

        public void NewCtrlFocus()
        {
            throw new System.NotImplementedException();
        }

        public void SetProgress(int nMin, int nMax, int nStep)
        {
            throw new System.NotImplementedException();
        }

        public void SetProgressPos(int nPos)
        {
            throw new System.NotImplementedException();
        }

        public void SetProgressUnitName(string sName)
        {
            throw new System.NotImplementedException();
        }

        public void StepProgress()
        {
            throw new System.NotImplementedException();
        }

        public int SaveData(object pObj)
        {
            throw new System.NotImplementedException();
        }

        public object LoadDCOSetupTree(string lpszSetupPath)
        {
            throw new System.NotImplementedException();
        }

        public int GetKeyState(short KeyCode)
        {
            throw new System.NotImplementedException();
        }

        public object GetTMClient()
        {
            throw new System.NotImplementedException();
        }

        public void RepaintPause(int mSec)
        {
            throw new System.NotImplementedException();
        }

        public void AllowBatchTreeMenu(int bAllow)
        {
            throw new System.NotImplementedException();
        }

        public void ModifyTreeStyle(int dwRemove, int dwAdd)
        {
            throw new System.NotImplementedException();
        }

        public void ModifyTreeStyleEx(int dwRemoveEx, int dwAddEx)
        {
            throw new System.NotImplementedException();
        }

        public void GetNextObject(object pCurObj, int dwTreeProperty, ref object pObj)
        {
            throw new System.NotImplementedException();
        }

        public string ExportXML()
        {
            throw new System.NotImplementedException();
        }

        public void ImportXML(string strContent)
        {
            throw new System.NotImplementedException();
        }

        public int SetupMode { get; }
        public string BatchID { get; set; }
        public string BatchDir { get; set; }
        public string Operator { get; set; }
        public string Station { get; set; }
        public int ChildrenQuantity { get; set; }
        public short Priority { get; set; }
        public object ThumbSource { get; set; }
        public object ImageSource { get; set; }
        public object ActiveObject { get; set; }
        public string Caption { get; set; }
        public string ProjectPath { get; set; }
        public int PagesInBatch { get; set; }
        public int DocsInBatch { get; set; }
        public int ExpectedPages { get; set; }
        public int ExpectedDocs { get; set; }
        public int AdjustedPages { get; set; }
        public int AdjustedDocs { get; set; }
        public string JobName { get; set; }
        public string TaskName { get; set; }
        public string FormPath { get; set; }
        public string DCOFile { get; set; }
        public string JobID { get; set; }
        public string TaskID { get; set; }
        public string get_ChildImageDir(int nChild)
        {
            throw new System.NotImplementedException();
        }

        public void set_ChildImageDir(int nChild, string pVal)
        {
            throw new System.NotImplementedException();
        }

        public int get_ChildCondition(int nChild)
        {
            throw new System.NotImplementedException();
        }

        public void set_ChildCondition(int nChild, int pVal)
        {
            throw new System.NotImplementedException();
        }

        public string get_ChildPageFile(int nChild)
        {
            throw new System.NotImplementedException();
        }

        public void set_ChildPageFile(int nChild, string pVal)
        {
            throw new System.NotImplementedException();
        }

        public string get_XtraBatchFieldValue(string strName)
        {
            throw new System.NotImplementedException();
        }

        public void set_XtraBatchFieldValue(string strName, string pVal)
        {
            throw new System.NotImplementedException();
        }

        public string get_XtraChildFieldValue(string strName, short nIndex)
        {
            throw new System.NotImplementedException();
        }

        public void set_XtraChildFieldValue(string strName, short nIndex, string pVal)
        {
            throw new System.NotImplementedException();
        }

        public string get_TaskVar(string strName)
        {
            throw new System.NotImplementedException();
        }

        public void set_TaskVar(string strName, string pVal)
        {
            throw new System.NotImplementedException();
        }

        public string get_Path(string strName)
        {
            throw new System.NotImplementedException();
        }

        public string AppName { get; set; }
        public string Workflow { get; set; }
    }
}