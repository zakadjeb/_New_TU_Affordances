using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamControl : MonoBehaviour {
    private SteamVR_TrackedController controller;
    private SteamVR_LaserPointer laserPointer;
    public bool padPress;
    public bool padTouch;
    public bool hasrun;

    public Color ind = new Color();
    public Color ud = new Color();

    // Use this for initialization
    void Start () {
        controller = GetComponent<SteamVR_TrackedController>();
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        laserPointer.color = ind;
        laserPointer.thickness = 0.0f;
        laserPointer.addRigidBody = false;
    }
	
	// Update is called once per frame
	void Update () {
        padPress = controller.padPressed;
        padTouch = controller.padTouched;

        if (padTouch && !hasrun)
        {
            //SteamVR_LaserPointer laserPointer = gameObject.AddComponent(typeof(SteamVR_LaserPointer)) as SteamVR_LaserPointer;
            laserPointer.active = true;
            laserPointer.color = ind;
            laserPointer.thickness = 0.005f;
            laserPointer.addRigidBody = false;
            hasrun = true;
        }

        if (!padTouch && hasrun)
        {
            laserPointer.active = false;
            laserPointer.color = ud;
            laserPointer.thickness = 0.0f;
            laserPointer.addRigidBody = false;
            //Destroy(gameObject.GetComponent("SteamVR_LaserPointer"));
            hasrun = false;
        }
		
	}
}
