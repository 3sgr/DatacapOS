using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDCOLib;
using dclogXLib;
using dcrroLib;
using PILOTCTRLLib;
using _3sgrMath;

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
            Assert.AreEqual(_dco.Variable["TYPE"], "APT");
            //Batch Profiler.xml
            //dco.Read()
        }

       

        [TestMethod]
        public void SimpleITTest()
        {
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
        [TestMethod]
        public void RunCustomKeyWord()
        {
            var actions = new _3sgrMath.Actions
            {
                //DatacapRRDCO = _dco,
                CurrentDCO = _dco,
                RRLog = _dclogxl,
                DatacapRRState = _rrState,
                DatacapRRBatchPilot = _pilot,
                DateTimeFormats = "YYYY-MM-dd"
            };
            Assert.IsTrue(actions.ProcessFormula("Count(3)"));
        }
        #region FormulaProcessor

        [TestMethod]
        public void ProcessExpression1()
        {
            var cv = new _3sgrMath.FormulaProcessor();
            const string formula = "exp(ln(2))      +  + (sin(3.14159265358/180*30)  +  0.25 +   +(100.0+-1.0*2.0)/10.0  +     ((abc)) -0.25     -     25^(-1)) ";
            var r = Math.Exp(Math.Log(2.0)) + Math.Sin(Math.PI / 180 * 30) + 0.25 + (100.0 - 1.0 * 2.0) / 10.0 + 2.0 - 0.25 - 1.0 / 25.0;
            var res = cv.Value(formula);
            Assert.AreEqual(res - r, 0, 0.00001);

        }

        [TestMethod]
        public void ProcessExpression2()
        {
            var cv = new FormulaProcessor();
            const string formula = "2*3";
            var r = 2*3;
            var res = cv.Value(formula);
            Assert.AreEqual(res - r, 0, 0.00001);

        }
        
        #endregion
    }
}
