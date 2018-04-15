using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTrigger : MonoBehaviour {
	public GameObject timer;
    public GameObject PointText;
    public Text pointTxt;
    public bool exited = false;
    private bool hasrun = false;

    private ChangePosition cp;
    private sendMarker sM;

    private Vector3 startPos;
    private Vector3 endPos;
    public float timeToMove = 5f;
    public int dist = 10;

    //Questionnaire
    private GameObject quest;
    public List<GameObject> questList;
    private SpriteRenderer[] sprites;
    private GameObject descript;
    private GameObject minmax;

    //Manager Point System
    public GameObject Manager;
    private Manager m;

    // Use this for initialization
    void Start()
    {
        pointTxt.text = "";
        cp = GetComponent<ChangePosition>();
        m = Manager.GetComponent<Manager>();
        sM = GameObject.Find("sendingMarker").GetComponent<sendMarker>();

        quest = GameObject.Find("Questionnaire");
        sprites = quest.GetComponentsInChildren<SpriteRenderer>();
        descript = GameObject.Find("Description");
        minmax = GameObject.Find("Min/Max");

        questList.Add(quest);
    }


    void OnTriggerEnter (Collider c) {
        if (c.gameObject.name == "Controller (right)")
        {
            AudioSource gameCoin = GetComponent<AudioSource>();
            gameCoin.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            exited = false;

            //Sending eventmarker
            m.finalList.Add("#" + sM.TrialNo + ";" + sM.Case + ";" + sM.isHigh + ";" + sM.isDark + ";" + sM.State + ";Coin");
            sM.marker.Write("#" + sM.TrialNo + ";" + sM.Case + ";" + sM.isHigh + ";" + sM.isDark + ";" + sM.State + ";Coin");

            //€ sign moving upwards and fading out
            if (!hasrun)
            {
                float pointValue = 0.1f;
                pointTxt.text = "€" + pointValue.ToString("F2") + "!";

                m.points.Add(float.Parse(pointValue.ToString("f2"), System.Globalization.CultureInfo.InvariantCulture));

                hasrun = true;
            }
        
            StartCoroutine(move(timeToMove));

            //Activating questionnaire
            descript.SetActive(true);
            minmax.SetActive(true);
            m.questRun = false;
            for (int i = 0; i < quest.transform.childCount; i++)
            {
                GameObject children;
                children = quest.transform.GetChild(i).gameObject;
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

            for (int i = 0; i < quest.transform.childCount * 2; i++)
            {
                sprites[i].enabled = true;
                
                //quest.transform.GetChild(i).gameObject.SetActive(true);
                //Debug.Log(i);
            }

        }         
    }

    void OnTriggerExit (Collider c){
        exited = true;

	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 50 * Time.deltaTime);
        PointText.transform.Rotate(new Vector3(0,1,0), 100 * Time.deltaTime);

        if (exited)
        {
            PointText.transform.position = gameObject.transform.position;
        }

        if (m.questRun)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    IEnumerator move(float time)
    {
        float elapsedTime = 0;
        startPos = gameObject.transform.position;
        endPos = Vector3.Scale(gameObject.transform.position, new Vector3(1, dist, 1));
        while (elapsedTime < time)
        {
            PointText.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            if (elapsedTime > time)
            {
                hasrun = false;
                pointTxt.text = "";
                
            }
            yield return null;
        }
    }
}
