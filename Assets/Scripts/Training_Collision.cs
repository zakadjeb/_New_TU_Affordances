using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Collision : MonoBehaviour {

	private Training_Manager m;
    public GameObject txt;

	// Use this for initialization
	void Start () {
        txt = GameObject.Find("TouchToStart");
	}

	void OnTriggerEnter (Collider c){
		if(c.name == "Controller (right)"){

			UnityEngine.SceneManagement.SceneManager.LoadScene("Openings");
		}
	}

	// Update is called once per frame
	void Update () {

		
	}
}
