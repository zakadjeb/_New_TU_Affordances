using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class VRUIInput : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private SteamVR_TrackedController trackedController;

    private GameObject active;
    private GameObject inactive;

    private void OnEnable()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;
        laserPointer.PointerOut += HandlePointerOut;

        trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = GetComponentInParent<SteamVR_TrackedController>();
        }
        trackedController.PadClicked-= HandlePadClicked;
        trackedController.PadClicked += HandlePadClicked;
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        var smiley = e.target.GetComponentInParent<QuestionnaireSettings>();
        //var smiley = e.target.GetComponent<SpriteRenderer>();
        if (smiley != null && GetComponent<SteamVR_TrackedController>().padTouched)
        {
            smiley.hashit = true;
            //smiley.enabled = true;
            //Debug.Log("HandlePointerIn", e.target.gameObject);
        }
        if (smiley != null && !GetComponent<SteamVR_TrackedController>().padTouched)
        {
            smiley.hashit = false;
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        var smiley = e.target.GetComponentInParent<QuestionnaireSettings>();
        //var smiley = e.target.GetComponent<SpriteRenderer>();
        if (smiley != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            smiley.hashit = false;
            //smiley.enabled = false;
            //Debug.Log("HandlePointerOut", e.target.gameObject);
        }
    }
}