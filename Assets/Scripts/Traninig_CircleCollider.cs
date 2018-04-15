using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traninig_CircleCollider : MonoBehaviour {

	public GameObject cube;
	public GameObject txt;
	void OnTriggerEnter (Collider c) {
		if(c.name == "Controller (right)"){
			cube.SetActive(true);
			txt.SetActive(true);
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Start () {

	}
}
