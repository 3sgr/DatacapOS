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


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using dcSmart;
using SSSGroup.Datacap.CustomActions.FormulaProcessor;
using SSSGroup.Datacap.CustomActions._3sgrMath.Resources;

namespace SSSGroup.Datacap.CustomActions._3sgrMath
{
    /// <summary>
    /// Class difinition is split into multiple .cs files based on logical purpose of the code
    /// This file contains public functions and properties visible from Datacap
    /// </summary>
    public partial class Actions // This class must be a base class for .NET 4.0 Actions.
    {
        public Dictionary<string, Func<string, string>> CustomFunctions;

        public Actions()
        {
            CustomFunctions = new Dictionary<string, Func<string, string>> { { "count", CountXmlNodes }, { "sum", SumXmlNodes }, { "sumASCII", SumASCII }, { "smartParameter", SmartParameter } };
        }

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

        #region CustomProperties

        public bool Changed = true;
        private XmlDocument _masterDoc;
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
        #region PublicFunctions
        /// <summary>
        /// This function will take and process formula.
        /// </summary>
        /// <param name="formula"></param>
        /// Supports smart parameters as part of the formula or target
        /// <returns></returns>
        public bool ProcessFormula(string formula)
        {
            try
            {
                CurrentDCO.Variable[Const.DCOResultVar] = Const.True;
                var localSmartObj = new SmartNav(this);
                WriteLog(Messages.PFStart);
                WriteLog(string.Format(Messages.PFProcessing,formula));
                string eval;
                string target;
//                var ps = new Parser(CustomFunctions);
//                if (TestForAssignement(formula, out target, out eval))
//                {//we have an assignement to be made
//                    localSmartObj.DCONavSetValue(target, ps.SubstringPush(eval));
//                }
//                else
//                {
//                    return Convert.ToBoolean(ps.SubstringPush(formula));
//                }
            }
            catch (Exception ex)
            {
                CurrentDCO.Variable[Const.DCOResultVar] = Const.False;
                //CallFormulaProcessor(ReadSmartParameter(formula));
                // It is a best practice to have a try catch in every action to prevent any unexpected errors
                // from being thrown back to RRS.
                WriteLog(string.Format(Messages.Exception,ex.Message));
                return false;
            }
            finally
            {
                WriteLog(Messages.PFEnd);
            }
            return true;
        }
        
        /// <summary/>
        /// This is an example custom .NET action that takes multiple parameters with multiple types.
        /// The parameter order and types must match the definition in the RRX file.

        #endregion
        #region PublicProperties
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
        #endregion
        
    }
}
