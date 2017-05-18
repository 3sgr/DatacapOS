//
//Copyright(c) 2017 SSS Group Ltd.
//3sgr.com
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
using System.IO;
using System.Reflection;
using dclogXLib;
using dcrroLib;
using PILOTCTRLLib;
using TDCOLib;
using Datacap.Math;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
#pragma warning disable 649

namespace DCUnitTest
{
    public class BaseTestClass
    {
        private IDCO _dco;
        private readonly IDCLog _dclogxl;
        private readonly IBPilotCtrl _pilot;
        private readonly IRRState _rrState;
        public Actions oActions;

        public BaseTestClass(DirectoryInfo runtimeDir = null, string setupDCO = "", string runtimeDCO = "")
        {
            if (null == runtimeDir)
                runtimeDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            if (string.IsNullOrEmpty(setupDCO))
                setupDCO = Path.Combine(runtimeDir.Parent.Parent.FullName, "Dependency", "TestDCO", "Setup", "APT.xml");
            if (string.IsNullOrEmpty(runtimeDCO))
                runtimeDCO = Path.Combine(runtimeDir.Parent.Parent.FullName, "Dependency", "TestDCO", "Runtime", "Batch Profiler.xml");

            _dco = new DCO();
            _dclogxl = new MyDCLogXLib();
            _rrState = new MyRRState();
            _pilot = new MyBPilotCtrl();

            //File.Copy(runtimeDCO, runtimeDCO+DateTime.Now.ToString("HH:mm:ss tt zz")+".bak");
            _dco.Read(runtimeDCO);
            _dco.ReadSetup(setupDCO);
            _pilot.BatchDir = Path.GetDirectoryName(runtimeDCO);
            oActions = new Actions
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
