//
// Licensed Materials - Property of IBM
// 5725-C15
// © Copyright IBM Corp. 1994, 2014 All Rights Reserved
//
// US Government Users Restricted Rights - Use, duplication or
// disclosure restricted by GSA ADP Schedule Contract with IBM Corp.
//
/*/ch*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace _3sgrMath
{
    public class RRX : iRRX.iRRXnet // derive from expected interface
    {
        public RRX()
        {
        }

        #region iRRXnet Members

        public string Code
        {
            get
            {
                return Properties.Resources.TheRRX;
            }
        }

        public string Help
        {
            get
            {
                return "";// Properties.Resources.help;
            }
        }

        public string LoadString(string strStringName, string strDefValue)
        {
            string strRes = strDefValue;
            try
            {
                strRes = Properties.Resources.ResourceManager.GetString(strStringName);
            }
            catch (Exception e)
            {
                strRes = e.Message;
            }
            return strRes;
        }

        private long mKeyPairLeft = 0;
        private long mKeyPairRight = 0;
        public bool RRSID(object sender, long n64KeyPairLeft)
        {
            if (!(sender is dcrroLib.IRRState))
                return false;

            Random rnd = new Random(n64KeyPairLeft.GetHashCode());
            int n1 = rnd.Next();
            int n2 = rnd.Next();
            Int64 n64 = n1;
            n64 <<= 32;
            n64 += n2;

            dcrroLib.IRRState iRRSState = sender as dcrroLib.IRRState;
            //Int64 n64KeyPairRight = 0;
            mKeyPairLeft = n64KeyPairLeft;
            mKeyPairRight = n64;
            return iRRSState.RRXDLLID(n64KeyPairLeft, n64);
        }

        public bool SecureCall(long n64KeyPairLeft, long n64KeyPairRight, string strIn, out string strOut)
        {
            strOut = "";
            if (0 == mKeyPairLeft ||
                0 == mKeyPairRight ||
                mKeyPairLeft != n64KeyPairLeft ||
                mKeyPairRight != n64KeyPairRight)
                return false;

            return true;
        }

        #endregion
    }
}
