using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEngine.XR;
using System;
using Assets.LSL4Unity.Scripts;

public class Training_Manager : MonoBehaviour {
    [Header("Event Code")]
    public string EventCode;
    public bool eventMarkerRun = false;

    [Header("Game-settings")]
	public int trials = 300;
    public int trialNo = 1;
    public float tb4start = 5;
    public float SecB4Exit = 5;
    public float theStartTimer = 6.0f;
    public float dayTimer = 0;
    public float darkTimer = 3.0f;
    float walktime = 10.0f;
    public string potentialTotalTime;
    public bool starttimeRun = false;
    private bool trialRun = false;
    public bool isdark = true;
    private bool timeRunNight;
    private bool timeRunDay;
    private bool timeRunOnce;
    private bool earlyDebugRun;
    private bool timeMarkerRun = false;

    private static bool tButton;

    [Header("Managing questionnaire")]
    public string ChosenSAM;
    public float JumpingDistance = 3;
    public float Smoothness = 5;
    public  float timeFade = 5f;
    public bool SmileyPicked = false;
    public bool arousalPicked = false;
    public bool excitementPicked = false;
    public bool dominancePicked = false;
    public bool questRun = false;
    public bool threeQuestPicked = false;
    private GameObject questionnaire;
    private SpriteRenderer[] sprites;
    private GameObject descript;
    private GameObject minmax;


    [Header("Exiting settings")]
    public GameObject exitTxt;
    public Text exitingTxt;
    private string participantSize;
    public float participantSizefloat;
    private string RoomScale;

    [Header("Getting controller, sphere, spaces and wallMarker")]
    public GameObject cube;
	public GameObject txt;
	public GameObject touchCircle;
    private float tempVal;
	public float speed;
	public float amplitude;
	private Vector3 tempPos;
    public GameObject sphere;
    public GameObject opening;
    private Training_OpeningWidth oW;
    public GameObject wallMarker;
    public GameObject controller;
    private Training_LightsOn lO;
    public static bool triggerBool = false;

    //SphereCollider for customizing size
    private GameObject head;

    [Header("Generating list")]
    public List<int> doorCaseList;
    public List<int> tempDoorCase;
    public List<int> ceilingHeightList;
    public List<int> tempHeight;
    public List<int> goNoGoList;
    public List<int> tempGoNoGo;
    public string currentSituation;
    private string goCase;
    private string ceilingCase;

    [Header("Final lists")]
    public List<string> finalList;
    public List<float> points = new List<float>();
    public float totalPoints = 0;
    public List<string> quest = new List<string>();

    [Header("LSL settings")]
    public GameObject sending;
    private sendMarker sM;

    // Use this for initialization
    void Start () {
        //Tracking space type.
        if (UnityEngine.XR.XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale))
        {
            RoomScale = "Room-scale experience successfully employed.";
            Debug.Log(RoomScale);
        }
        else
        {
            RoomScale = "Room-scale experience failed!"; 
            Debug.Log(RoomScale);
        }

        //Getting other scripts.
        oW = opening.GetComponent<Training_OpeningWidth>();

        sM = sending.GetComponent<sendMarker>();
        sM.isDark = "Off";

        trialNo = 0;

        lO = controller.GetComponent<Training_LightsOn>();

        dayTimer = theStartTimer + UnityEngine.Random.Range(2, -2);

        questionnaire = GameObject.Find("Questionnaire");
        sprites = questionnaire.GetComponentsInChildren<SpriteRenderer>();
        descript = GameObject.Find("Description");
        minmax = GameObject.Find("Min/Max");

		cube = GameObject.Find("Cube");
		txt = GameObject.Find("TouchToStart");
		touchCircle = GameObject.Find("TouchCircle");
        tempVal = cube.transform.position.y;
        cube.SetActive(false);
		txt.SetActive(false);
		touchCircle.SetActive(true);

        //Calculating potential total time
        //potentialTotalTime = "+2: " + ((tb4start + ((trials/(100/chance)) * (theStartTimer+2)) + ((trials / (100 / chance)) * walktime) + ((trials / (100 / chance)) * (darkTimer+2))+SecB4Exit) / 60f).ToString("F2") + "min. +0: " + ((tb4start + ((trials / (100 / chance)) * (theStartTimer)) + ((trials / (100 / chance)) * walktime) + ((trials / (100 / chance)) * (darkTimer)) + SecB4Exit) / 60f).ToString("F2") + "min. -2: " + ((tb4start + ((trials / (100 / chance)) * (theStartTimer - 2)) + ((trials / (100 / chance)) * walktime) + ((trials / (100 / chance)) * (darkTimer - 2)) + SecB4Exit) / 60f).ToString("F2") + "min.";
        potentialTotalTime = "+2: " + ((tb4start + ((trials/2) * ((darkTimer + 2) + (theStartTimer+2) + walktime))) / 60).ToString("F2") + " min. -2: " + ((tb4start + ((trials/2) * ((darkTimer - 2) + (theStartTimer - 2) + walktime))) / 60).ToString("F2") + " min.";

        //Getting custom size of participants
        StreamReader customsizeString = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "/customsize.txt");
        participantSize = customsizeString.ReadLine();
        participantSizefloat = float.Parse(participantSize);

        //Setting custom SphereCollider
        head = GameObject.Find("Camera (eye)");
        SphereCollider sphereCol = (SphereCollider)head.gameObject.AddComponent(typeof(SphereCollider));
        sphereCol.isTrigger = true;
        sphereCol.radius = ((participantSizefloat)/2)/100;

        //Generating pseudo-randomized lists
        for (int i = 0; i < (trials / 3); i++)
        {
            tempDoorCase.Add(3);
            tempDoorCase.Add(2);
            tempDoorCase.Add(1);
            doorCaseList = tempDoorCase;
            // doorCaseList = tempDoorCase.OrderBy(x => UnityEngine.Random.value).ToList();
        }
        for (int i = 0; i < (trials / 2); i++)
        {
            tempHeight.Add(1);
            tempHeight.Add(2);
            ceilingHeightList = tempHeight.OrderBy(x => UnityEngine.Random.value).ToList();
        }
        for (int i = 0; i < (trials / 2); i++)
        {
            tempGoNoGo.Add(1);
            tempGoNoGo.Add(2);
            goNoGoList = tempGoNoGo;
            // goNoGoList = tempGoNoGo.OrderBy(x => UnityEngine.Random.value).ToList();
        }
    }

    // Update is called once per frame
    void Update() {
        //Training settings
        	//Rotating objects
		cube.transform.Rotate (Vector3.left + Vector3.down, 75 * Time.deltaTime);
		txt.transform.Rotate (Vector3.up, 75 * Time.deltaTime);	
		touchCircle.transform.Rotate (Vector3.up, 10 * Time.deltaTime);

			//Hovering the cube and text
		tempPos.y = tempVal + amplitude * Mathf.Sin (speed * Time.time);
		cube.transform.position = new Vector3 (cube.transform.position.x, tempPos.y, cube.transform.position.z);
		txt.transform.position = new Vector3 (txt.transform.position.x, tempPos.y + 0.3f, txt.transform.position.z);
        touchCircle.transform.position = new Vector3 (touchCircle.transform.position.x, tempPos.y, touchCircle.transform.position.z);

        //Current situation updated
        if (trialNo != trials)
        {
            if (goNoGoList[trialNo] == 1) goCase = "Go";
            if (goNoGoList[trialNo] == 2) goCase = "NoGo";
            if (ceilingHeightList[trialNo] == 1) ceilingCase = "Low";
            if (ceilingHeightList[trialNo] == 2) ceilingCase = "High";
            currentSituation = "Door: " + doorCaseList[trialNo] + ". Height: " + ceilingCase + ". Go/NoGo: " + goCase;
        }
        //Keep getting the event code
        sM.TrialNo = trialNo.ToString();
        EventCode = sM.EventCode;

        //Questionnaire settings: setting all smileys off when it's dark
        if (isdark && !questRun)
        {
            descript.SetActive(false);
            minmax.SetActive(false);
            for(int i = 0; i < questionnaire.transform.childCount; i++)
            {
                GameObject children;
                children = questionnaire.transform.GetChild(i).gameObject;
                for(int j = 0; j < 2; j++)
                {
                    children.transform.GetChild(j).gameObject.SetActive(true);
                }
            }
            for(int i = 0; i < questionnaire.transform.childCount * 2; i++)
            {
                sprites[i].enabled = false;
            }
            questRun = true;
        }

        //Trigger
        if (lO.triggerButtonDown) triggerBool = !triggerBool;
        if (triggerBool) isdark = true;

        //Counting trials
        if (isdark && !trialRun)
        {
            trialNo++;
            sM.isDark = "Off";
            tButton = !tButton;
            trialRun = true;
            eventMarkerRun = false;
            StartCoroutine(EventMarker());
        }
        if (!isdark && trialRun)
        {
            eventMarkerRun = false;
            StartCoroutine(EventMarker());
            sM.isDark = "On";
            trialRun = false;
        }

        //Point system
        totalPoints = points.Sum();

        //Setting material
        Material plaster = Resources.Load("Door", typeof(Material)) as Material;
        Material red = Resources.Load("Red", typeof(Material)) as Material;
        Material green = Resources.Load("Green", typeof(Material)) as Material;

        ///////////////////////////////////////////////////////////////////////////////////////////////////Timer settings/////////////////////////////////////////////////////////////

        //Gamestart settings
        if(Input.GetKeyDown(KeyCode.Escape)) ExitGame();

        if (!starttimeRun)
        {
            tb4start -= Time.deltaTime;
            sM.isDark = "On";
            //Debug.Log("Press Spacebar now to start the experiment"); 
            //&& Input.GetKeyDown(KeyCode.Space)
            if (tb4start <= 0 && isdark)
            {
                sM.isDark = "Off";
                sphere.SetActive(true);
                starttimeRun = true;
                triggerBool = true;
                isdark = false;
                StartCoroutine(EventMarker());
            }
        }

        if (tb4start <= tb4start && !earlyDebugRun)
        {
            StartCoroutine(EventMarker());
            
            earlyDebugRun = true;
        }

        //Case of day
        if (!timeRunDay && darkTimer < 0)
        {
            sM.isDark = "On";
            triggerBool = false;
            isdark = false;

            dayTimer -= Time.deltaTime;


            if (dayTimer < 0 && !(trialNo == trials))
            {
                //tempVal = UnityEngine.Random.Range(1, 100);
                if (goNoGoList[trialNo] == 1)
                {
                    StartCoroutine(fade(red));
                    sM.State = "NoGo";
                    if(sM.State == "NoGo")
                    {
                        eventMarkerRun = false;
                        StartCoroutine(EventMarker());
                        StopCoroutine(EventMarker());

                        descript.SetActive(true);
                        minmax.SetActive(true);
                        questRun = false;
                        for (int i = 0; i < questionnaire.transform.childCount; i++)
                        {
                            GameObject children;
                            children = questionnaire.transform.GetChild(i).gameObject;
                            for(int j = 0; j < children.transform.childCount; j++)
                            {
                                GameObject childrenschilderen;
                                childrenschilderen = children.transform.GetChild(j).gameObject;
                                for (int k = 0; k < childrenschilderen.transform.childCount; k++)
                                {
                                    GameObject childrenschildrenschild = childrenschilderen.transform.GetChild(k).gameObject;
                                    childrenschildrenschild.SetActive(true);
                                }
                            }
                        }

                        for (int i = 0; i < questionnaire.transform.childCount * 2; i++)
                        {
                            sprites[i].enabled = true;
                        }
                    }
                }
                else
                {
                    StartCoroutine(fade(green));
                    sM.State = "Go";
                    if (sM.State == "Go")
                    {
                        eventMarkerRun = false;
                        StartCoroutine(EventMarker());
                        StopCoroutine(EventMarker());

                    }
                }
                timeRunDay = true;
                timeRunNight = false;
                timeRunOnce = false;
            }
            if(trialNo> trials)
            {
                timeRunDay = true;
            }
        }

        //Case of night
        if(timeRunOnce && !timeRunNight && isdark && tb4start < 0)
        {
            timeMarkerRun = false;
            darkTimer -= Time.deltaTime;
            if (darkTimer < 0)
            {
                sM.isDark = "Off";

                isdark = false;
                timeRunNight = true;
                timeRunDay = false;
            }
        }

        //Night ONCE
        if(!timeRunOnce && isdark)
        { 
            darkTimer = theStartTimer + UnityEngine.Random.Range(2, -2);
            dayTimer = theStartTimer + UnityEngine.Random.Range(2, -2);

            sM.State = "";

            timeRunOnce = true;
        }

        //Exiting
        exitingTxt.alignment = TextAnchor.MiddleCenter;

        if (trialNo == trials)
        {
            exitTxt.SetActive(true);

            exitingTxt.text = trials.ToString() + " trials reached." + System.Environment.NewLine + "Training session is done! Touch the green diamond in the other room.";

            SecB4Exit -= Time.deltaTime;
            sM.marker.Write("TraningDone");

            cube.SetActive(true);
            txt.SetActive(true);


            // if (SecB4Exit <= 0)
            // {
            //     //sM.marker.Write("Exit");
            //     ExitGame();
            // }
        }
    }


    //Creating final output list and exiting the game.
    void ExitGame()
    {
        Debug.Log("Output_log file created.");



        string final = string.Join(System.Environment.NewLine, finalList.ToArray());
        System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "/Output_log.txt", "Output list" + System.Environment.NewLine + System.Environment.NewLine + "Participant width: " + participantSize + System.Environment.NewLine + System.Environment.NewLine + RoomScale + System.Environment.NewLine + final);


        Debug.Log("Game has been quit");
        Application.Quit();
    }

    //Sending marker.
    IEnumerator EventMarker()
    {
        yield return new WaitForEndOfFrame();

        if (!eventMarkerRun)
        {
            finalList.Add("#" + sM.TrialNo + ";" + sM.Case + ";" + sM.isHigh + ";" + sM.isDark + ";" + sM.State);
            sM.marker.Write(EventCode);
            eventMarkerRun = true;
        }
        eventMarkerRun = true;
    }


    //The fading of materials.
    IEnumerator fade(Material m)
    {
        for (float f = 1; f > 0; f -= 0.05f)
        {
            Material plaster = Resources.Load("Door", typeof(Material)) as Material;
            Renderer wallRend = wallMarker.GetComponent<Renderer>();
            wallRend.material.Lerp(plaster, m, f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
