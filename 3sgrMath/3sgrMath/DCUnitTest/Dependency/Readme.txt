You need to register dlls listed in this folder using the following bat files:
\Dependencies\RegisterCOM
	dclogX.dll
	dcrro.dll
	PilotCtrl.dll
	TDCO.dll
\Dependencies\
	dcsmart.dll
	iRRX.dll
- To Register COM use : Regsvr.bat
- To Unregister COM: UnRegsvr.bat
After registering the dlls add them via COM reference in Visual Studio

In addition you need to supply a valid format DCO files:
-Setup DCO to:
\Dependencies\DCO\Setup
-Runtime DCO to:
\Dependencies\DCO\Runtime - Copy content of the APT Demo batch

