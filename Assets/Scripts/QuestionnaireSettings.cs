using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestionnaireSettings : MonoBehaviour {
    public GameObject controllerHand;
    private LaserBeamControl LBC;

    private SteamVR_LaserPointer laser;

    public GameObject quest;
    public SpriteRenderer active;
    public SpriteRenderer inactive;
    public bool hashit;
    public float tid;

    private Vector3 origin;

    public GameObject Manager;
    private Manager m;
    private sendMarker sM;
    private GameObject descript;
    private GameObject minmax;

    private bool hasPressed = false;
    private bool fadeRunArousal = false;
    private bool fadeRunExcitement = false;
    private bool fadeRunDominance = false;

    Color originActive;
    Color originInactive;

    // Use this for initialization
    void Start () {

        LBC = controllerHand.GetComponent<LaserBeamControl>();

        quest = GameObject.Find("Questionnaire");

        laser = LBC.GetComponent<SteamVR_LaserPointer>();
        m = Manager.GetComponent<Manager>();
        sM = GameObject.Find("sendingMarker").GetComponent<sendMarker>();

        tid = m.timeFade;

        active = transform.Find("Active").GetComponent<SpriteRenderer>();
        inactive = transform.Find("Inactive").GetComponent<SpriteRenderer>();
        descript = GameObject.Find("Description");
        minmax = GameObject.Find("Min/Max");

        origin = gameObject.transform.position;

        originActive = active.color;
        originInactive = inactive.color;
    }

    // Update is called once per frame
    void Update () {
        //Pointing on smileys
        if(hashit)
        {
            active.enabled = true;
            if (LBC.padPress && LBC.padTouch && !hasPressed)
            {
                m.ChosenSAM = transform.parent.name;
                StartCoroutine(fade());
                StartCoroutine(goUp());
                m.quest.Add(gameObject.name.ToString());

                m.SmileyPicked = true;
                hasPressed = true;

                m.finalList.Add("#" + sM.TrialNo + ";" + sM.Case + ";" + sM.isHigh + ";" + sM.isDark + ";" + sM.State + ";" + name);
                sM.marker.Write("#" + sM.TrialNo + ";" + sM.Case + ";" + sM.isHigh + ";" + sM.isDark + ";" + sM.State + ";" + name);
            }
        }

        //NOT pointing on smileys
        if(!hashit)
        {
            active.enabled = false;
            inactive.enabled = true;
        }

        //If one smiley has been picked IN CHILD'S ROW
        if (m.SmileyPicked && m.ChosenSAM == "Arousal" && !fadeRunArousal)
        {
            tid -= Time.deltaTime;
            if (tid < 0)
            {
                StartCoroutine(fadeInactive());
                fadeRunArousal = true;
            }
        }

        if (m.SmileyPicked && m.ChosenSAM == "Excitement" && !fadeRunExcitement)
        {
            tid -= Time.deltaTime;
            if (tid < 0)
            {
                StartCoroutine(fadeInactive());
                fadeRunExcitement = true;
            }
        }

        if (m.SmileyPicked && m.ChosenSAM == "Dominance" && !fadeRunDominance)
        {
            tid -= Time.deltaTime;
            if (tid < 0)
            {
                StartCoroutine(fadeInactive());
                fadeRunDominance = true;
                descript.SetActive(false);
                minmax.SetActive(false);
            }
        }

        //Todo in the dark
        if (m.isdark)
        {
            gameObject.transform.position = origin;
            StartCoroutine(colorReset());

            active.enabled = false;
            m.SmileyPicked = false;

            tid = m.timeFade;

            fadeRunArousal = false;
            fadeRunExcitement = false;
            fadeRunDominance = false;
            hasPressed = false;

            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    IEnumerator fade()
    {
        active.enabled = true;
        inactive.enabled = false;

        Color tmpColor = active.color;
        tmpColor.a = 0f;

        for (float f = 1; f >= 0; f -= 0.05f)
        {
            active.material.SetColor("_Color", Color.Lerp(tmpColor, active.color, f));
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }


    IEnumerator fadeInactive()
    {
        if(transform.parent.name == m.ChosenSAM)
        {
            for (float f = 1; f >= 0; f -= 0.05f)
            {
                Color tmpColor2 = inactive.color;
                tmpColor2.a = 0f;

                inactive.material.SetColor("_Color", Color.Lerp(tmpColor2, inactive.color, f));
                yield return new WaitForSeconds(0.05f);
            }
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    IEnumerator colorReset()
    {
        
        originActive.a = 1.0f;
        originInactive.a = 1.0f;
        active.material.SetColor("_Color", originActive);
        inactive.material.SetColor("_Color", originInactive);
        yield return null;

        //for (float f = 1; f >= 0; f -= 0.05f)
        //{
        //    active.material.SetColor("_Color", Color.Lerp(originActive, active.color, f));
        //    inactive.material.SetColor("_Color", Color.Lerp(originInactive, inactive.color, f));
        //    yield return new WaitForSeconds(0.05f);
        //}
    }

    IEnumerator goUp()
    {
        Vector3 target = gameObject.transform.position + new Vector3(0, m.JumpingDistance, 0);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * m.Smoothness);
        yield return null;

    }
}
