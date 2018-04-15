using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Assets.LSL4Unity.Scripts;

public class LightsOn : MonoBehaviour {

//sphere business
	public GameObject sphere;
	public static bool myBool = false;
	private bool hasrun = false;

//controller
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;
	public bool triggerButtonDown = false;

    //Getting manager
    public GameObject Manager;
    private Manager m;

//Sending Marker
	private sendMarker sM;
	

	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        sphere.SetActive(false);
        sM = GameObject.Find("sendingMarker").GetComponent<sendMarker>();
        m = Manager.GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {
		triggerButtonDown = controller.GetPressDown(triggerButton);

		if(triggerButtonDown){
			myBool = !myBool;
		}
			
        //Day
		if(!m.isdark && !hasrun)
        {
            sphere.SetActive(false);
			hasrun=true;
		}

        //Night
        if (m.isdark && hasrun && (m.trialNo != m.trials))
        {
            hasrun = false;
            sphere.SetActive(true);
        }
	}
}
