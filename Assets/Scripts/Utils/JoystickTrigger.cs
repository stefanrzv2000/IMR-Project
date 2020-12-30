using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class JoystickTrigger : MonoBehaviour
{
    VRTK_ControllerEvents controller;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        //controller = GameObject.Find("RightHand").transform.GetChild(2).GetComponent<VRTK_ControllerEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("TRIGGER PRESENT");
        if(controller == null)
        {
            controller = GameObject.Find("RightHand").transform.GetChild(2).GetComponent<VRTK_ControllerEvents>();
            if(controller != null)
            {
                controller.GripPressed += BoardObjectUsed;
                controller.GripReleased += BoardObjectUnused;
            }
        }
        if(controller != null)
        {
            //Debug.Log($"Trigger Axis {Input.GetAxis("RightTrigger")}");
            if(!triggered && Input.GetAxisRaw("RightTrigger") > 0.5)
            {
                triggered = true;
                controller.OnGripPressed(new ControllerInteractionEventArgs());
            }
            if (triggered && Input.GetAxisRaw("RightTrigger") < 0.5)
            {
                triggered = false;
                controller.OnGripReleased(new ControllerInteractionEventArgs());
            }

        }
    }

    private void BoardObjectUsed(object sender, ControllerInteractionEventArgs e)
    {
        UseEventsController.Instance.OnBoardObjectUsed();
    }

    private void BoardObjectUnused(object sender, ControllerInteractionEventArgs e)
    {
        UseEventsController.Instance.OnBoardObjectUnused();
    }
}
