using System.IO;
using System.Reflection;
using dclogXLib;
using dcrroLib;
using PILOTCTRLLib;
using TDCOLib;
using _3sgrMath;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
#pragma warning disable 649

namespace DCUnitTest
{
    public class BaseTestClass
    {
        private  IDCO _dco;
        private readonly IDCLog _dclogxl;
        private readonly IBPilotCtrl _pilot;
        private readonly IRRState _rrState;
        public Actions ActionsInst;

    public BaseTestClass(DirectoryInfo runtimeDir = null, string setupDCO = "", string runtimeDCO = "")
        {
            if (null == runtimeDir)
                runtimeDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            if (string.IsNullOrEmpty(setupDCO))
                setupDCO = Path.Combine(runtimeDir.Parent.Parent.FullName, "Dependency", "DCO", "Setup", "APT.xml");
            if (string.IsNullOrEmpty(runtimeDCO))
                runtimeDCO = Path.Combine(runtimeDir.Parent.Parent.FullName, "Dependency", "DCO", "Runtime", "Batch Profiler.xml");

            _dco = new DCO();
            _dclogxl = new MyDCLogXLib();
            _rrState = new MyRRState();
            _pilot = new MyBPilotCtrl();

            //File.Copy(runtimeDCO, runtimeDCO+DateTime.Now.ToString("HH:mm:ss tt zz")+".bak");
            _dco.Read(runtimeDCO);
            _dco.ReadSetup(setupDCO);
            _pilot.BatchDir = Path.GetDirectoryName(runtimeDCO);
            ActionsInst = new Actions
             {
                 //DatacapRRDCO = _dco,
                 CurrentDCO = _dco,
                 RRLog = _dclogxl,
                 DatacapRRState = _rrState,
                 DatacapRRBatchPilot = _pilot,
                 DateTimeFormats = "YYYY-MM-dd"
             };

        }
    }
}
