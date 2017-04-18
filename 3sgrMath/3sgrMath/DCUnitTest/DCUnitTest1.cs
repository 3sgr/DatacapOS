using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDCOLib;
using dclogXLib;
using dcrroLib;
using PILOTCTRLLib;
using FormulaProcessor;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace DCUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public FormulaProcessor.FormulaProcessor CV = new FormulaProcessor.FormulaProcessor();
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
            //File.Copy(runtimeDCO, runtimeDCO+".bak");
            _dco.Read(runtimeDCO);
            _dco.ReadSetup(setupDCO);
            _pilot.BatchDir = Path.GetDirectoryName(runtimeDCO);
            var  i = 0;
            var t = 0;
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

        #region FormulaProcessor

        [TestMethod]
        public void DigitTerm()
        {
            string Formula = "2*3.3";// "exp(ln(2))      +  + (sin(3.14159265358/180*30)  +  0.25 +   +(100.0+-1.0*2.0)/10.0  +     ((abc)) -0.25     -     25^(-1)) "; 
            double r = 2 * 3.3// Math.Exp(Math.Log(2.0))+ Math.Sin( Math.PI / 180 * 30)  +  0.25   +  (100.0 -1.0*2.0)/10.0  +     2.0     -0.25     -     1.0/25.0;
          FillTable();

            var DoubleValue = CV.Value(Formula);
            Assert.AreEqual(DoubleValue - r, 0, 0.00001);
        }

        public void FillTable()
        {   // the identifiers of variables
            CV.InitID("abc", 2.0);
            // the identifiers of functiones
            // CV.InitID("Func", 1.0);
        }
        #endregion
    }
}
