using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRHandController : MonoBehaviour
{
    public enum HandType
    {
        RIGHT,
        LEFT,
    }
    public HandType handType = HandType.RIGHT;
    private GameObject handController = null;
    private VRTK_ControllerEvents controllerEvents = null;
    private string handName;

    // Start is called before the first frame update
    void Start()
    {
        handName = handType == HandType.RIGHT ? "RightHand" : "LeftHand";
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR_WIN
#else
        if (handController == null)
        {
            handController = GameObject.Find(handName);
            
        }
        
        if (handController != null)
        {
            if (controllerEvents == null)
            {
                controllerEvents = handController.transform.GetChild(2).GetComponent<VRTK_ControllerEvents>();
            }
            Debug.Log($"ControllerEvents {controllerEvents}");
            handController.transform.position = transform.position;
            handController.transform.rotation = transform.rotation;
        }
#endif
    }

    public void GripPressed()
    {
        controllerEvents?.OnGripPressed(new ControllerInteractionEventArgs());
    }

    public void GripReleased()
    {
        controllerEvents?.OnGripReleased(new ControllerInteractionEventArgs());
    }

    public void TriggerPressed()
    {
        controllerEvents?.OnTriggerPressed(new ControllerInteractionEventArgs());
    }

    public void TriggerReleased()
    {
        controllerEvents?.OnTriggerReleased(new ControllerInteractionEventArgs());
    }
}
