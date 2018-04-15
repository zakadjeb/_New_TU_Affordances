using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class OpeningWidth : MonoBehaviour {
    [Header("Current Case")]
    public int HeightCase;
    public int WidthCase;

    //roof settings
    public GameObject goalRoof;
    public GameObject goalWalls;
    private GameObject roofWall;
    public float caseNoRoof;
    private bool roofRun = false;

	//width settings
	public GameObject wallL;
	public GameObject wallR;
	public float caseNo;
	public bool wallRun = false;
    public float case1;
    public float case2;
    public float case3;

	//Movements for wall
	private Vector3 vecCase1 = new Vector3 (.10f, 0, 0);
    private Vector3 vecCase2;
	private Vector3 vecCase3 = new Vector3 (0.6f, 0, 0);

    //Movements for roof
    private Vector3 roofCase1 = new Vector3(0, 0, 0);
    private Vector3 roofCase2 = new Vector3(0, 0, 3f);
    private Vector3 originWallR;
    private Vector3 originWallL;
    private Vector3 originRoof;

    //Getting triggerbool
    public GameObject controllerTrigger;
	private LightsOn lightScript;
	public bool buttonBoolean;

	//Writing marker
	public GameObject EventMarker;
	private sendMarker sM;
    

	//Getting Scripts
	public GameObject Manager;
	private Manager m;

	
	//Bools
	public bool caseRun = true;

	// Use this for initialization
	void Start () {
		lightScript = controllerTrigger.GetComponent<LightsOn>();
		sM = EventMarker.GetComponent<sendMarker>();
		m = Manager.GetComponent<Manager>();

        roofWall = GameObject.Find("roofWall");

        //Setting custom size
        vecCase2 = new Vector3(((m.participantSizefloat+10)/100)/2, 0, 0);

        //Origin positions
        originWallR = wallR.transform.position;
        originWallL = wallL.transform.position;
        originRoof = goalRoof.transform.position;

        //Displaying sizes
        case1 = vecCase1.x * 2;
        case2 = (vecCase2.x+10) * 2;
        case3 = vecCase3.x * 2;
	}

	// Update is called once per frame
	void Update () {

        //HeightCase = m.ceilingHeightList[m.trialNo];
        //WidthCase = m.doorCaseList[m.trialNo];

        if (lightScript.triggerButtonDown){
			buttonBoolean = !buttonBoolean;
        }

        //Generate case
        if(caseRun && (m.trialNo != m.trials))
        {
            switch (m.doorCaseList[m.trialNo])
            {
                case 1:
                    sM.Case = "Op1";
                    break;
                case 2:
                    sM.Case = "Op2";
                    break;
                case 3:
                    sM.Case = "Op3";
                    break;
            }
            switch (m.ceilingHeightList[m.trialNo])
            {
                case 1:
                    sM.isHigh = "Low";
                    break;
                case 2:
                    sM.isHigh = "High";
                    break;
            }

            caseRun = false;
        }

        //Moving walls
		if(!m.isdark && !wallRun && !(m.trialNo == m.trials))
        {
			if(m.doorCaseList[m.trialNo] == 1){
                //Opening
				wallR.transform.Translate(-vecCase1);
				wallL.transform.Translate(vecCase1);

                //Cases
                caseNo = 1;
			}

			if(m.doorCaseList[m.trialNo] == 2){
                //Opening
                wallR.transform.Translate(-vecCase2);
				wallL.transform.Translate(vecCase2);

                //Cases
                caseNo = 2;
			}

			if(m.doorCaseList[m.trialNo] == 3){
                //Opening
				wallR.transform.Translate(-vecCase3);
				wallL.transform.Translate(vecCase3);
                //Roof

                //Cases
				caseNo = 3;
			}

			wallRun = true;
        }

        //Moving roof
        if (!m.isdark && !roofRun && !(m.trialNo == m.trials))
        {
            if (m.ceilingHeightList[m.trialNo] == 1)
            {
                //Roof
                goalWalls.transform.localScale = new Vector3(1, 1, 1);
                roofWall.SetActive(false);
                //Cases
                caseNoRoof = 1;
            }

            if (m.ceilingHeightList[m.trialNo] == 2)
            {
                //Roof
                goalRoof.transform.Translate(roofCase2);
                roofWall.SetActive(true);
                goalWalls.transform.localScale = new Vector3(1, 1, 2);
                //Cases
                caseNoRoof = 2;

                roofRun = true;
            }
        }

        //Putting back to origin
        if (m.isdark && wallRun)
		{
			wallR.transform.position = originWallR;
			wallL.transform.position = originWallL;
					
			wallRun = false;
            caseRun = true;
            buttonBoolean = true;
		}

        if(m.isdark && roofRun)
        {
            goalRoof.transform.position = originRoof;
            goalWalls.transform.localScale = new Vector3(1, 1, 1);
            
            roofRun = false;
        }
	}
		
}
