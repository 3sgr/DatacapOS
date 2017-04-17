© SSS Group Ltd.

In order to successfully run unit tests you need to set up the environemnt.
It is recommended to use a computer that already has © IBM Datacap software installed.
If you do not have © IBM Datacap software installed on your Development station you will need to register certain dlls.
You can copy required dlls to the folders listed below and use supplied bat files to register: <br/>
<b> \\Dependencies\\RegisterCOM </b>
<br/>
- dclogX.dll<br/>
- dcrro.dll<br/>
- PilotCtrl.dll<br/>
- TDCO.dll<br/>
<b> \\Dependencies\\ </b> <br/>
- dcsmart.dll  <br/>
- iRRX.dll <br/>
<br/>
- To Register COM use : Regsvr.bat<br/>
- To Unregister COM: UnRegsvr.bat<br/>
After registering the dlls add them via COM reference in Visual Studio for COM dlls and add dcsmart.dll and iRRX.dll as .dll <br/>

In addition you need to supply a valid format DCO files: <br/>
-Setup DCO to:
\Dependencies\DCO\Setup
-Runtime DCO to:
\Dependencies\DCO\Runtime - Copy content of the APT Demo batch


For more details see DCUnitTest project.
