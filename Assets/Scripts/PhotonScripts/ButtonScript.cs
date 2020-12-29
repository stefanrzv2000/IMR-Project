using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent OnClickMenuButton;
    private Color lastColor = Color.black;

    void Start()
    {
        
    }

    public VRTK_InteractableObject linkedObject;
    //public GameObject OtherMenu;
    //public float spinSpeed = 360f;


    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);
        if (lastColor == Color.black) lastColor = transform.GetChild(0).Find("Text").GetComponent<Text>().color;

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched += InteractableObjectUntouched;
            //linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }

        //spinner = transform.Find("Capsule");
    }

    private void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        transform.GetChild(0).Find("Text").GetComponent<Text>().color = lastColor;
    }

    private void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        if (lastColor == Color.black) lastColor = transform.GetChild(0).Find("Text").GetComponent<Text>().color;
        transform.GetChild(0).Find("Text").GetComponent<Text>().color = Color.red;
    }

    private void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        OnClickMenuButton.Invoke();
    }

    protected virtual void OnDisable()
    {
        transform.GetChild(0).Find("Text").GetComponent<Text>().color = lastColor;
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            //linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
