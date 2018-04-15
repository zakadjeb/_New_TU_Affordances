using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadWallCollision : MonoBehaviour {
    private Training_Manager m;
    private OpeningWidth o;
    private sendMarker sM;

    //Sending current Event
    private string failed;

    //questionnaire
    public bool questRun = false;
    private GameObject questionnaire;
    private SpriteRenderer[] sprites;
    private GameObject descript;
    private GameObject minmax;

    private void OnTriggerEnter(Collider other)
    {
        if (!m.isdark)
        {
            Material red = Resources.Load("Red", typeof(Material)) as Material;
            StartCoroutine(fadingIn(red));

            AudioSource alert = GetComponent<AudioSource>();
            alert.Play();

            failed = "#" + sM.TrialNo + ";" + sM.Case + ";" + sM.isHigh + ";" + sM.isDark + ";" + sM.State + ";FAILED";

            if(!m.finalList[m.finalList.Count - 1].Equals(failed)){
                activatingQuestionnaire();
                m.finalList.Add(failed);
                sM.marker.Write(failed);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Material red = Resources.Load("Red", typeof(Material)) as Material;
        StartCoroutine(fadingOut(red));
    }

    // Use this for initialization
    void Start () {
        m = GameObject.Find("Manager").GetComponent<Training_Manager>();
        o = GameObject.Find("Openings").GetComponent<OpeningWidth>();
        sM = GameObject.Find("sendingMarker").GetComponent<sendMarker>();

        //questionnaire
        questionnaire = GameObject.Find("Questionnaire");
        sprites = questionnaire.GetComponentsInChildren<SpriteRenderer>();
        descript = GameObject.Find("Description");
        minmax = GameObject.Find("Min/Max");
    }
	
	// Update is called once per frame
	void Update () {
        //Setting material
        Material plaster = Resources.Load("Plasterwhite", typeof(Material)) as Material;
        Material green = Resources.Load("Green", typeof(Material)) as Material;
    }

    //The fading of materials.
    IEnumerator fadingIn(Material m)
    {
        for (float f = 1; f > 0; f -= 0.5f)
        {
            Material plaster = Resources.Load("Plasterwhite", typeof(Material)) as Material;
            Renderer wallRend = GetComponent<Renderer>();
            wallRend.material.Lerp(plaster, m, f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    //The fading of materials.
    IEnumerator fadingOut(Material m)
    {
        Material plaster = Resources.Load("Plasterwhite", typeof(Material)) as Material;
        for (float f = 1; f > 0; f -= 0.5f)
        {
            Renderer wallRend = GetComponent<Renderer>();
            wallRend.material.Lerp(m, plaster, f);
            yield return new WaitForSeconds(0.5f);
        }
        GetComponent<Renderer>().material = plaster;
    }

    void activatingQuestionnaire(){
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
