using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using VRTK;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent OnClickMenuButton;

    void Start()
    {
        
    }

    public VRTK_InteractableObject linkedObject;
    //public GameObject OtherMenu;
    //public float spinSpeed = 360f;


    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            //linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }

        //spinner = transform.Find("Capsule");
    }

    private void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        OnClickMenuButton.Invoke();
    }

    protected virtual void OnDisable()
    {
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
