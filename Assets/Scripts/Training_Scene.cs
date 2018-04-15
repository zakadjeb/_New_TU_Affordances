using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.LSL4Unity.Scripts;

public class Training_Scene : MonoBehaviour {
		//Getting objects
	[Header("Getting objects")]
	public GameObject circle;
	public GameObject cube;
	public GameObject txt;
	public GameObject turnAround;
		//Settings
	[Header("Settings")]
	private float tempVal;
	public float speed;
	public float amplitude;
	private Vector3 tempPos;

		//origin values
	private float originX;
	private float originY;
	private float originZ;

		//Colliders
	private BoxCollider cubeCol;
	private SphereCollider circleCol;

		//sending traning marker
	private LSLMarkerStream marker;
	public bool EventMarker = false;

	void Start () {
			//LSL marker
		marker = FindObjectOfType<LSLMarkerStream>();

			//finding objects
		circle = GameObject.Find("Circle");
		cube = GameObject.Find("Cube");
		txt = GameObject.Find("TouchToStart");
		turnAround = GameObject.Find("TurnAround");

			//Origin values
		originX = cube.transform.position.x;
		originY = cube.transform.position.y;
		originZ = cube.transform.position.z;

			//temp value
		tempVal = cube.transform.position.y;

			//Getting colliders
		cubeCol = cube.GetComponent<BoxCollider>();
		circleCol = circle.GetComponent<SphereCollider>();

			//Start settings
		cube.SetActive(false);
		txt.SetActive(false);
		turnAround.SetActive(false);
	}
	

	void Update () {
			//On pressing spacebar the participant is taken to next level/spacebar
		if(Input.GetKeyDown(KeyCode.Space)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene("Openings");
		}

			//Rotating objects
		circle.transform.Rotate (Vector3.up, 50 * Time.deltaTime);
		cube.transform.Rotate (Vector3.left + Vector3.down, 75 * Time.deltaTime);
		txt.transform.Rotate (Vector3.up, 75 * Time.deltaTime);	
		turnAround.transform.Rotate (Vector3.up, 75 * Time.deltaTime);

			//Hovering the cube and text
		tempPos.y = tempVal + amplitude * Mathf.Sin (speed * Time.time);
		cube.transform.position = new Vector3 (originX, tempPos.y, originZ);
		txt.transform.position = new Vector3 (txt.transform.position.x, tempPos.y + 0.3f, txt.transform.position.z);

			//Send marker on pressing A
		if(EventMarker){
			marker.Write("Training done!");
			Debug.Log("Training done!");
		} 
	}
}
