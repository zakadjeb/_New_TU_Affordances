using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;
using Assets.LSL4Unity.Scripts;

public class sendMarker : MonoBehaviour {
	//Creating marker
	public LSLMarkerStream marker;

    //Setting the names
    public string TrialNo; // Trial number we are at, at the moment
	public string Case; // Whether it is openings or flooring 
    public string isHigh; //Whether it's a transition to a higher space
	public string isDark; // Whether it is dark or not
	public string State; // Whether it is Go or No Go

    [Header("Event Code")]
	public string EventCode;

	// Use this for initialization
	void Start () {
        marker = FindObjectOfType<LSLMarkerStream>();
	}
	
	// Update is called once per frame
	void Update () {
		EventCode = "#" + TrialNo + ";" + Case + ";" + isHigh + ";" + isDark + ";" + State;
	}
} 