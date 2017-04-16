﻿//
// Licensed Materials - Property of IBM
//
// 5725-C15
// © Copyright IBM Corp. 1994, 2014 All Rights Reserved
// US Government Users Restricted Rights - Use, duplication or
// disclosure restricted by GSA ADP Schedule Contract with IBM Corp.
//
// This is an example of a .NET action for IBM Datacap using .NET 4.0.
// The compliled DLL needs to be placed into the RRS directory.
// The DLL does not need to be registered.  
// Datacap studio will find the RRX file that is embedded in the DLL, you do not need to place the RRX in the RRS directory.
// If you add references to other DLLs, such as 3rd party, you may need to place those DLLs in C:\RRS so they are found at runtime.
// If Datacap references are not found at compile time, add a reference path of C:\Datacap\DCShared\NET to the project to locate the DLLs while building.
// This template has been tested with IBM Datacap 9.0.  

using System;
using System.Runtime.InteropServices;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace _3sgrMath
{
    public class Actions // This class must be a base class for .NET 4.0 Actions.
    {
        #region ExpectedByRRS
        /// <summary/>
        ~Actions()
        {
            DatacapRRCleanupTime = true;
        }

        /// <summary>
        /// Cleanup: This property is set right before the object is released by RRS
        /// </summary>
        public bool DatacapRRCleanupTime
        {
            set
            {
                if (value)
                {
                    CurrentDCO = null;
                    DCO = null;
                    RRLog = null;
                    RRState = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        protected PILOTCTRLLib.IBPilotCtrl BatchPilot = null;
        public PILOTCTRLLib.IBPilotCtrl DatacapRRBatchPilot { set { this.BatchPilot = value; GC.Collect(); GC.WaitForPendingFinalizers(); } get { return this.BatchPilot; } }

        protected TDCOLib.IDCO DCO = null;
        /// <summary/>
        public TDCOLib.IDCO DatacapRRDCO
        {
            get { return this.DCO; }
            set
            {
                DCO = value;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        protected dcrroLib.IRRState RRState = null;
        /// <summary/>
        public dcrroLib.IRRState DatacapRRState
        {
            get { return this.RRState; }
            set
            {
                RRState = value;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public TDCOLib.IDCO CurrentDCO = null;
        /// <summary/>
        public TDCOLib.IDCO DatacapRRCurrentDCO
        {
            get { return this.CurrentDCO; }
            set
            {
                CurrentDCO = value;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public dclogXLib.IDCLog RRLog = null;
        /// <summary/>
        public dclogXLib.IDCLog DatacapRRLog
        {
            get { return this.RRLog; }
            set
            {
                RRLog = value;
                LogAssemblyVersion();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #endregion

        #region CommonActions

        void OutputToLog(int nLevel, string strMessage)
        {
            if (null == RRLog)
                return;
            RRLog.WriteEx(nLevel, strMessage);
        }

        public void WriteLog(string sMessage)
        {
            OutputToLog(5, sMessage);
        }

        private bool versionWasLogged = false;

        // Log the version of the library that was running to help with diagnosis.
        // Hooked this method to be called after the log object is assigned.  Also put in
        // a check that this action runs only once, just in case it gets called multiple times.
        protected bool LogAssemblyVersion()
        {
            try
            {
                if (versionWasLogged == false)
                {
                    FileVersionInfo fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                    WriteLog(Assembly.GetExecutingAssembly().Location +
                             ". AssemblyVersion: " + Assembly.GetExecutingAssembly().GetName().Version.ToString() +
                             ". AssemblyFileVersion: " + fv.FileVersion.ToString() + ".");
                    versionWasLogged = true;
                }
            }
            catch (Exception ex)
            {
                WriteLog("Version logging exception: " + ex.Message);
            }

            // We can always return true.  If getting the version fails, we can try to continue anyway.
            return true;
        }

        #endregion


        //implementation of the Dispose method to release managed resources
        public void Dispose()
        {
        }

        struct Level
        {
            internal const int Batch = 0;
            internal const int Document = 1;
            internal const int Page = 2;
            internal const int Field = 3;
        }

        /// <summary/>
        /// This is an example custom .NET action that takes multiple parameters with multiple types.
        /// The parameter order and types must match the definition in the RRX file.
        public bool ProcessFormula(string formula)
        {
            var bRes = true;
            dcSmart.SmartNav localSmartObj = null;

            try
            {
               
            }
            catch (Exception ex)
            {
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog("There was an exception: " + ex.Message);
                bRes = false;
            }

            localSmartObj = null;
            return bRes;
        }


        public string dateTimeFormat;
        /// <summary/>
        /// This is an example of an action that sets a C# DateTime Formats that needs to be used.  
        /// An action that sets a property always returns true.
        /// The parameter type must match the type defined in the RRX file for this action.
        public string DateTimeFormats
        {
            get { return dateTimeFormat; }
            set
            {
                dateTimeFormat = value;
                OutputToLog(5, dateTimeFormat);
            }
        }
    }
}