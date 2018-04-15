using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training_ControllerInstructions : MonoBehaviour {

	public SteamVR_TrackedController trackedController;
	public GameObject controller;
	public Training_Manager m;
	public GameObject manager;
	public GameObject questInstruc;
	public Text questText;
	public GameObject Pad;
	public GameObject Trigger;
	public float rotY;
	public float rotX;

	//Declaring questionnaire to add to the padPressInt
	[Header("Questionnaire settings")]
	public GameObject questArousal;
		private SpriteRenderer[] spriteArousal;
			public bool arousalBool;
	public GameObject questExcitement;
		private SpriteRenderer[] spriteExcitement;
			public bool excitementBool;
	public GameObject questDominance;
		private SpriteRenderer[] spriteDominance;
			public bool dominanceBool;
	

    public bool padPressInt;

	//Booleans
	public bool padTouchRun = false;
    public bool padPressRun = false;

	public bool keyA = false;
		public bool keyS = false;
			public bool keyD = false;

	public bool hasRun1 = false;
		private bool rotDone = false;
			public bool hasRun2 = false;
				public bool hasRun3 = false;
					public bool hasRun4 = false;


	// Use this for initialization
	void Start () {
		//controller = GameObject.Find("Controller  (right)");
		trackedController = controller.GetComponent<SteamVR_TrackedController>();
		m = GameObject.Find("Manager").GetComponent<Training_Manager>();

		questInstruc = GameObject.Find("questInstruc");
		questText = questInstruc.transform.GetChild(0).GetComponent<Text>();

		spriteArousal = questArousal.transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
		spriteExcitement = questExcitement.transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
		spriteDominance = questDominance.transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		rotY = gameObject.transform.eulerAngles.y;
		rotX = gameObject.transform.eulerAngles.x;
		//Booleans
		/*Must be redone to;
			trackedController.padTouched
			trackedController.padPressed
			trackedcontroller.triggerPressed
			*/

		if(trackedController.padTouched) keyA = !keyA;
		if(trackedController.padPressed) keyS = !keyS;
		if(trackedController.triggerPressed) keyD = !keyD;

		//Setting material
        Material black = Resources.Load("Black", typeof(Material)) as Material;
        Material red = Resources.Load("Red", typeof(Material)) as Material;
        Material green = Resources.Load("Green", typeof(Material)) as Material;

		questText.text = "Touch the pad." + System.Environment.NewLine + "This will activate your pointer.";

		//Sequences
		//Point of departure...
		if(!keyA && !padTouchRun && !hasRun1) {
			StartCoroutine(fadePad(red));
			hasRun1=true;
		}

        //trackedController.padTouched
        //When pad is touched...
        if (keyA && hasRun1) padTouchRun = true;
		if(padTouchRun && hasRun1) {
			questText.text = "Nice!" + System.Environment.NewLine + "Now point towards the questionnaire," + System.Environment.NewLine + "and fill in the questionnaire by clicking the pad.";
			padTouchRun = true;
			hasRun2 = true;
		}

        //!trackedController.padPressed && !hasRun3
        //When pad is pressed...
        if (keyS && hasRun1 && hasRun2)
        {
            padPressRun = true;
			if(m.arousalPicked && m.excitementPicked && m.dominancePicked) padPressInt = true;
        }
		if(padPressRun && hasRun1 && hasRun2 && padPressInt) {
			if(!hasRun3) StartCoroutine(rotate(-40f, 0f));
			questText.text = "Good job. Now you know how that works!" + System.Environment.NewLine + "Now, face the opening, and pull the trigger!";
		}
		if(rotDone && !hasRun3){
			StartCoroutine(fadeTrigger(red));
			StartCoroutine(fadePad(black));
			//hasRun3 = true;
			hasRun4 = true;
		} 

				//trackedController.triggerPressed && hasRun4
				//After trigger is pulled...
		if(keyD && hasRun4) {
			StartCoroutine(fadeTrigger(black));
			questInstruc.SetActive(false);
			gameObject.SetActive(false);
			hasRun2 = true;
		}

	}

	IEnumerator rotate(float f, float ff){
		Quaternion target = Quaternion.Euler(f,ff,0f);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
		if(gameObject.transform.eulerAngles.y > 320f) rotDone = true;
		if(gameObject.transform.eulerAngles.x <= (360-39f)) hasRun3 = true;
		yield return new WaitForSeconds(0.01f);
	}
	IEnumerator fadePad(Material m)
    {
			for (float f = 1; f > 0; f -= 0.05f)
			{
				Material black = Resources.Load("Black", typeof(Material)) as Material;
				Renderer Rend = Pad.GetComponent<Renderer>();
				Rend.material.Lerp(m, black, f);
				yield return new WaitForSeconds(0.05f);
			}
    }
	IEnumerator fadeTrigger(Material m)
    {
        for (float f = 1; f > 0; f -= 0.05f)
        {
            Material black = Resources.Load("Black", typeof(Material)) as Material;
            Renderer Rend = Trigger.GetComponent<Renderer>();
            Rend.material.Lerp(m, black, f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
