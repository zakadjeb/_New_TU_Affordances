//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO.Ports;

//public class writeTTL : MonoBehaviour {

////Getting trigger
//public GameObject controller;
//private LightsOn lightsOn;

////Getting OpeningWidth
//public GameObject oW;
//private OpeningWidth openWidth;
//private timer tOpen;

////Getting FloorWidth
//public GameObject fW;
//private FloorWidth floorWidth;
//private timer tWidth;

////Getting ChangeSpace
//public GameObject chSpace;
//private changeSpace chS;

////TTL-signal
//public string portName = "COM8";
//public int baudRate = 115200;
//SerialPort sp = new SerialPort ();

////run once for case!
//public bool hasRunCase1 = true;
//public bool hasRunCase2 = true;
//public bool hasRunCase3 = true;

////run once for timer!
//public bool hasRunTimerGO = true;
//public bool hasRunTimerNoGo = true;

////has run once for changing space!
//public bool hasRunHoleSpace = true;
//public bool hasRunOpenSpace = true;

//	// Use this for initialization
//	void Start () {
//		//Port-settings
//		sp.PortName = portName;
//		sp.BaudRate = baudRate;
//		sp.DtrEnable = true;
//		sp.RtsEnable = true;
//		sp.Parity = Parity.None;
//		sp.DataBits = 8;
//		sp.StopBits = StopBits.One;
//		sp.Open ();
//		Debug.Log ("Port is open? " + sp.IsOpen);

//		//OpenWidth settings
//		openWidth = oW.GetComponent<OpeningWidth>();

//		//FloorWidth settings
//		floorWidth = fW.GetComponent<FloorWidth>();

//		//Timer settings
//		tOpen = openWidth.GetComponent<timer>();
//		tWidth = floorWidth.GetComponent<timer>();

//		//ChangeSpace settings
//		chS = chSpace.GetComponent<changeSpace>();
//	}
	
//	// Update is called once per frame
//	void Update () {
//		//TTL signal for changing space...
//		if(chS.isOpening && hasRunOpenSpace){
//			byte[] bytetosend = {0x42};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Spaces of OPENINGS. Code #42");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunOpenSpace = false;			
//		}
		
//		if(!chS.isOpening && hasRunHoleSpace){
//			byte[] bytetosend = {0x44};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Spaces of HOLES. Code #44");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunHoleSpace = false;			
//		}
//		//TTL signal timer hitting 0.00...

//		if(tOpen.Go && hasRunTimerGO){
//			byte[] bytetosend = {0x34};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Go! Code #34");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunTimerGO = false;
//		 }
		
//		if(tOpen.NoGo && hasRunTimerNoGo){
//			byte[] bytetosend = {0x32};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("No go! Code #32");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunTimerNoGo = false;
//		 }
		 
//		 if(tWidth.Go && hasRunTimerGO){
//			byte[] bytetosend = {0x34};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Go! Code #34");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunTimerGO = false;
//		 }

//		if(tWidth.NoGo && hasRunTimerNoGo){
//			byte[] bytetosend = {0x32};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("No go! Code #32");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunTimerNoGo = false;
//		 }

//		//TTL signal for opening width case number...

//		if(openWidth.caseNo == 1 && hasRunCase1){
//			byte[] bytetosend = {0x36};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Case #" + openWidth.caseNo + ". Code #36");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunCase1 = false;
//		 }

//		 if(openWidth.caseNo == 2 && hasRunCase2){
//			byte[] bytetosend = {0x38};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Case #" + openWidth.caseNo + ". Code #38");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunCase2 = false;
//		 }

//		 if(openWidth.caseNo == 3 && hasRunCase3){
//			byte[] bytetosend = {0x40};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Case #" + openWidth.caseNo + ". Code #40");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunCase3 = false;
//		 }

//		 //TTl signal for floor width case numer...
//		if(floorWidth.caseNo == 1 && hasRunCase1){
//			byte[] bytetosend = {0x36};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Case #" + floorWidth.caseNo + ". Code #36");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunCase1 = false;
//		 }

//		 if(floorWidth.caseNo == 2 && hasRunCase2){
//			byte[] bytetosend = {0x38};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Case #" + floorWidth.caseNo + ". Code #38");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunCase2 = false;
//		 }

//		 if(floorWidth.caseNo == 3 && hasRunCase3){
//			byte[] bytetosend = {0x40};
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			sp.Write (bytetosend, 0, bytetosend.Length);
//			Debug.Log ("Case #" + floorWidth.caseNo + ". Code #40");
//			byte[] btosend = {0x00};
//			sp.Write (btosend, 0, btosend.Length);
//			sp.Write (btosend, 0, btosend.Length);
//			hasRunCase3 = false;
//		 }
		
//	}
//}
