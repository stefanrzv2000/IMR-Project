using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class EndTurn : MonoBehaviour
{
    public VRTK_InteractableObject linkedObject;

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched += InteractableObjectUntouched;
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }

    }

    protected virtual void OnDisable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched -= InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched -= InteractableObjectUntouched;
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
        }

    }

    private void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("END TURN");
        int this_player = PlayerInfoScene.Instance.playerId - 1;
        if (GameReferee.Instance.Players[this_player].HisTurn)
        {
            GameReferee.Instance.CallRPCMethod("SwitchTurns",this_player);
        }
    }

    private void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        transform.GetChild(1).GetChild(0).GetComponent<Text>().color = Color.black;
    }

    private void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        transform.GetChild(1).GetChild(0).GetComponent<Text>().color = new Color((float)0xff / 255, (float)0xea / 255, (float)0x00 / 255);
    }
}
