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

/*/ch*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Datacap.Math
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
