using System;
using System.IO;
using System.Reflection;
using Convertion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDCOLib;
using dclogXLib;
using dcrroLib;
using PILOTCTRLLib;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace DCUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IDCO _dco;
        private readonly IDCLog _dclogxl;
        private readonly IBPilotCtrl _pilot;
        private readonly IRRState _rrState;
        public UnitTest1()
        {
            _dco = new DCO();
            _dclogxl = new MyDCLogXLib();
            _rrState = new MyRRState();
            _pilot = new MyBPilotCtrl();
        }

        [TestMethod]
        public void ReadDCOs()
        {
           InitObjects();
            Assert.AreEqual(_dco.Variable["TYPE"], "APT");
            //Batch Profiler.xml
            //dco.Read()
        }

        private void InitObjects()
        {
            var runtimeDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var setupDCO = Path.Combine(runtimeDir.Parent.Parent.FullName, "Dependency","DCO", "Setup", "APT.xml");
            var runtimeDCO = Path.Combine(runtimeDir.Parent.Parent.FullName, "Dependency", "DCO", "Runtime", "Batch Profiler.xml");
            File.Copy(runtimeDCO, runtimeDCO+".bak");
            _dco.Read(runtimeDCO);
            _dco.ReadSetup(setupDCO);
            _pilot.BatchDir = Path.GetDirectoryName(runtimeDCO);
        }

        [TestMethod]
        public void RunAction()
        {
            InitObjects();
            var actions = new _3sgrMath.Actions
            {
                //DatacapRRDCO = _dco,
                CurrentDCO = _dco,
                RRLog = _dclogxl,
                DatacapRRState = _rrState,
                DatacapRRBatchPilot = _pilot,
                DateTimeFormats = "YYYY-MM-dd"
            };
            Assert.IsTrue(actions.ProcessFormula("test1"));
        }
        #region Conversion

        [TestMethod]
        public void ProcessExpression1()
        {
            var cv = new Convertor();
            const string formula = "exp(ln(2))      +  + (sin(3.14159265358/180*30)  +  0.25 +   +(100.0+-1.0*2.0)/10.0  +     ((abc)) -0.25     -     25^(-1)) ";
            var r = Math.Exp(Math.Log(2.0)) + Math.Sin(Math.PI / 180 * 30) + 0.25 + (100.0 - 1.0 * 2.0) / 10.0 + 2.0 - 0.25 - 1.0 / 25.0;
            var res = cv.Value(formula);
            Assert.AreEqual(res - r, 0, 0.00001);

        }

        [TestMethod]
        public void ProcessExpression2()
        {
            var cv = new Convertor();
            const string formula = "2*3";
            var r = 2*3;
            var res = cv.Value(formula);
            Assert.AreEqual(res - r, 0, 0.00001);

        }

        #endregion
    }
}
