using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class FloorWidth : MonoBehaviour {

	//width settings
	public GameObject floor;
	public GameObject floorWall;
	public float caseNo;
	public bool floorRun = false;
	private int tempVal;

	//Movements
	private Vector3 vecCase1 = new Vector3 (0,0,0);
	private Vector3 vecCase2 = new Vector3 (4.65f,0,0);
	private Vector3 vecCase3 = new Vector3 (5.15f,0,0); 

	//Getting triggerbool
	public GameObject controllerTrigger;
	private LightsOn lightScript;
	public static bool buttonBoolean;

	//Writing marker
	public GameObject EventMarker;
	private sendMarker sM;

	//Getting Scripts
	private FloorWidth fW;
	public GameObject Manager;
	private Manager m;

	//public GameObject exitTxt;
	public Text exitingTxt;
	
	//Bools
	private bool caseRun = true;

	// Use this for initialization
	void Start () {
		// lightScript = controllerTrigger.GetComponent<LightsOn>();
		// sM = EventMarker.GetComponent<sendMarker>();
		// m = Manager.GetComponent<Manager>();
	}

	// Update is called once per frame
	// void Update () {

	// 	if(lightScript.triggerButtonDown){
	// 		buttonBoolean = !buttonBoolean;
			
	// 	}

    //     if (caseRun)
    //     {
    //         tempVal = Random.Range(1, 4);
    //         switch (tempVal)
    //         {
    //             case 1:
    //                 sM.Case = "Fl1";
    //                 break;
    //             case 2:
    //                 sM.Case = "Fl2";
    //                 break;
    //             case 3:
    //                 sM.Case = "Fl3";
    //                 break;
    //         }
    //         caseRun = false;
    //     }

    //     if (!m.isdark && !floorRun && !(m.trialNo >= m.trials))
    //     {
	// 		//tempVal = Random.Range(1,4);
	// 		if(tempVal == 1){
	// 			floor.transform.Translate(vecCase1);
	// 			floorWall.transform.Translate(vecCase1);
	// 			caseNo = tempVal;			
	// 		}

	// 		if(tempVal == 2){
	// 			floor.transform.Translate(vecCase2);
	// 			floorWall.transform.Translate(vecCase2);
	// 			caseNo = tempVal;					
	// 		}

	// 		if(tempVal == 3){
	// 			floor.transform.Translate(vecCase3);
	// 			floorWall.transform.Translate(vecCase3);
	// 			caseNo = tempVal;
	// 		}

	// 		floorRun = true;
    //     }

	// 	if(m.isdark && floorRun)
	// 	{
	// 		if(tempVal == 1){
	// 			floor.transform.Translate(-vecCase1);
	// 			floorWall.transform.Translate(-vecCase1);
	// 			caseNo = tempVal;
	// 		}

	// 		if(tempVal == 2){
	// 			floor.transform.Translate(-vecCase2);
	// 			floorWall.transform.Translate(-vecCase2);
	// 			caseNo = tempVal;
	// 		}

	// 		if(tempVal == 3){
	// 			floor.transform.Translate(-vecCase3);
	// 			floorWall.transform.Translate(-vecCase3);
	// 			caseNo = tempVal;
	// 		}
			
	// 		floorRun = false;
    //         caseRun = true;
	// 	}
	// }
}
