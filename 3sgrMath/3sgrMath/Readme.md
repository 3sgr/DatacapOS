© SSS Group Ltd. 2017

Custom Actions Library for © IBM Datacap

Abstract
This library is designed to provide formula processing capabilities for © IBM Datacap.

Please note: You are responsible for providing access to all of the required dependencies, as they are not included with this project. 
It is recommended to Open/Compile the solution on a computer that has ©IBM Datacap sofwtare installed (version 9.x and higher)

Installation/Deployment
Content of this folder is a Visual Studio Solution that once built results in Custom Actions Library (.dll).Once Built the .dll needs to be place into Application-specific location (e.g. \Datacap\APT\dco_APT\Rules) or inside \Datacap\RRS\ folder.

1. Download(Pull) the solution to the PC that has ©IBM Datacap installed (version 9.x)
2. Remap references ('iRRX.dll' and 'dcSmart.dll') for the 3sgrMath project
3. Optional(Setting up Unit Test Project(see 'DCUnitTest' for more details) : <br/>
3.1. Create: '..\DCUnitTest\DCO\Setup' and '..\DCUnitTest\DCO\Runtime' folders <br/>
3.2 Copy APT Setup DCO ('C:\Datacap\APT\dco_APT\APT.xml') to '..\DCUnitTest\DCO\Setup'<br/>
3.3 Copy APT Batch content (at least at the Verify step, 'C:\Datacap\APT\batches\<batchID>\') to '..\DCUnitTest\DCO\Runtime'<br/>
4. Build the solution

For additional details, please see Wiki page.
