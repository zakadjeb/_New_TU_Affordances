using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeSpace : MonoBehaviour {
	public bool hasRun = false;
	private static bool myBool = false;
	public List<GameObject> openings, holes;
    private Manager m;

	public bool isOpening = true;


	int count = 0;
	int count2 = 0;

	// Use this for initialization
	void Start () {
		// foreach(GameObject x in holes){
		// 	x.SetActive(false);
		// }

        // m = this.GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {
        //if(m.trialNo > m.trials / 2)
        //{
        //    foreach (GameObject x in openings)
        //    {
        //        x.SetActive(false);
        //    }

        //    foreach (GameObject x in holes)
        //    {
        //        x.SetActive(true);
        //    }
        //    isOpening = false;
        //}

        //if(m.trialNo < m.trials / 2)
        //{
        //    foreach (GameObject x in openings)
        //    {
        //        x.SetActive(true);
        //    }

        //    foreach (GameObject x in holes)
        //    {
        //        x.SetActive(false);
        //    }
            
        //}
	}
}
