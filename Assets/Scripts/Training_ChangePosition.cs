using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Training_ChangePosition : MonoBehaviour {
    public int tempVal;
    public List<Vector3> coordinatesOpen = new List<Vector3>();
    //public List<Vector3> coordinatesHole = new List<Vector3>();
    private Vector3 origin;

    public GameObject Manager;
    private Training_Manager m;

    private GameObject quest;

    private bool hasrun = true;

	// Use this for initialization
	void Start () {
        m = Manager.GetComponent<Training_Manager>();

        origin = transform.position;

        quest = GameObject.Find("Questionnaire");

        //List for openings
        for(float i = -4.5f; i <= -.5f; i++)
        {
            for(float j = 3.5f;j <= 7.5f; j++)
            {
                Vector3 vect = new Vector3(-j, 1f, -i);
                coordinatesOpen.Add(vect);
            }
        }
        coordinatesOpen.RemoveAt(10);
        coordinatesOpen.RemoveAt(10);
        // for(int k = 0; k<coordinatesOpen.Count();k++){
        //     GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //     Cube.transform.position = coordinatesOpen[k];
        //     Cube.transform.localScale = new Vector3(.2f,.2f,.2f);
        // }
        

        //List for holes
        // for (float i = -4.5f; i <= -.5f; i++)
        // {
        //     for (float j = 6.5f; j <= 7.5f; j++)
        //     {
        //         Vector3 vect = new Vector3(-j, 1f, -i);
        //         coordinatesHole.Add(vect);
        //     }
        // }
    }
	
	// Update is called once per frame
	void Update () {

        //Openings 

        if (!m.isdark && hasrun)
        {
            tempVal = Random.Range(1, 23);
            transform.position = coordinatesOpen[tempVal];
            hasrun = false;
        }
        
        if(m.isdark && !hasrun)
        {
            transform.position = origin;
            hasrun = true;
        }

        //Hole in ground

        // if (!m.isdark && hasrun && !cS.isOpening)
        // {
        //     tempVal = Random.Range(1, 10);
        //     transform.position = coordinatesHole[tempVal];
        //     hasrun = false;
        // }

        // if (m.isdark && !hasrun && !cS.isOpening)
        // {
        //     transform.position = origin;
        //     hasrun = true;
        // }
    }
}
