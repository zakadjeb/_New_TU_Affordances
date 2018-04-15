using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_DoorScript : MonoBehaviour {

    public Vector3 origin;
    public Vector3 target;
    public Vector3 currentPosition;
    public bool doorRun;

    private Training_Manager m;
    private OpeningWidth o;
    public GameObject initInstruc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Camera (eye)" && !m.isdark)
        {
            StartCoroutine(moveAway());
            initInstruc.SetActive(false);
        }

    }

    // Use this for initialization
    void Start () {
        origin = transform.position;
        target = new Vector3(0, 0, 3);
        m = GameObject.Find("Manager").GetComponent<Training_Manager>();
        o = GameObject.Find("Openings").GetComponent<OpeningWidth>();
    }
	
	// Update is called once per frame
	void Update () {
        currentPosition = transform.position;

        if (m.isdark && !doorRun)
        {
            StartCoroutine(moveBack());
            doorRun = true;
        }

        if (!m.isdark) doorRun = false;
	}

    IEnumerator moveAway()
    {
        for (float f = 0; f < 1; f += 0.01f)
        {
            transform.position = Vector3.Lerp(origin, target, f);
            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator moveBack()
    {
        for (float f = 0; f < 1; f += 0.01f)
        {
            transform.position = origin;
            yield return new WaitForSeconds(.01f);
        }
    }
}
