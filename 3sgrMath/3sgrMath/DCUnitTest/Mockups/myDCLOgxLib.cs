using System;
using System.Diagnostics;
using dclogXLib;

namespace DCUnitTest
{
    class MyDCLogXLib : IDCLog
    {
        public void Init(string bstrTargetDir, string bstrFNCore, string bstrFNCritCore, string bstrExt)
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void Write(string bstrMsg)
        {
            Debug.WriteLine($"/Write/ message:'{bstrMsg}'");
        }

        public void Try()
        {
            throw new System.NotImplementedException();
        }

        public void Finally()
        {
            throw new System.NotImplementedException();
        }

        public void Sleep()
        {
            throw new System.NotImplementedException();
        }

        public void WakeUp()
        {
            throw new System.NotImplementedException();
        }

        public void InitEx(string bstrTargetDir, string bstrFNCore, string bstrFNCritCore, string bstrExt, bool bAppendIfExists,
            bool bUinqIdFileName)
        {
            throw new System.NotImplementedException();
        }

        public uint GetLastError(out string pbstrMsg, out string pbstrAction, out string pbstrWhere)
        {
            throw new System.NotImplementedException();
        }

        public void WriteEx(int nLevel, string bstrMsg)
        {
            Debug.WriteLine($"/WriteEx/ level:'{nLevel}' and message '{bstrMsg}'");
        }

        public bool Reflush { get; set; }
        public bool Time { get; set; }
        public string FileName { get; }
        public bool Enabled { get; set; }
        public int MessageLevelThreshold { get; set; }
        public bool ThreadProcessID { get; set; }
        public bool TimeDiff { get; set; }
        public string CriticalFileName { get; }
        public bool Milliseconds { get; set; }
    }
}