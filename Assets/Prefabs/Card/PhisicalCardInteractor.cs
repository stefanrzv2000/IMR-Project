using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PhisicalCardInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    public VRTK_InteractableObject linkedObject;

    private Vector3 startingPoint;
    private Quaternion startingRotation;
    private Card virtualCard;

    private bool grabbed = false;

    void Start()
    {
        startingPoint = transform.position;
        startingRotation = transform.rotation;
    }

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched += InteractableObjectUntouched;
            linkedObject.InteractableObjectGrabbed += InteractableObjectGrabbed;
            linkedObject.InteractableObjectUngrabbed += InteractableObjectUngrabbed;
        }
        
    }

    private void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        grabbed = false;
        var pos = GetBoardPosition();
        if (IsValidPosition(pos))
        {
            virtualCard.GoPlay(pos);
        }
        else
        {
            transform.position = startingPoint;
            transform.rotation = startingRotation;
        }
    }

    private void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        grabbed = true;
        //startingPoint = transform.position;
        //startingRotation = transform.rotation;
    }

    private void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        transform.Find("Highlight").gameObject.SetActive(false);
    }

    private void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        transform.Find("Highlight").gameObject.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched -= InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched -= InteractableObjectUntouched;
            linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
            linkedObject.InteractableObjectUngrabbed -= InteractableObjectUngrabbed;
        }
    }

    Vector2Int GetBoardPosition()
    {
        //TODO: Implement this
        return new Vector2Int(0, 0);
    }

    bool IsValidPosition(Vector2Int pos)
    {
        //TODO: Implement this
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabbed)
        {
            transform.position = startingPoint;
            transform.rotation = startingRotation;
        }
    }
}
