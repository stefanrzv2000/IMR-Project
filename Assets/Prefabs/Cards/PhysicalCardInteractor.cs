using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PhysicalCardInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    public VRTK_InteractableObject linkedObject;

    private Vector3 startingPoint;
    private Quaternion startingRotation;
    public Card virtualCard;
    private TableColors GameTable;

    private bool grabbed = false;

    void Start()
    {
        startingPoint = transform.position;
        startingRotation = transform.rotation;
        GameTable = GameObject.Find("GameTable").GetComponent<TableColors>();
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
        GameTable.cardGrabbed = null;
        GameTable.ResetAll();
        grabbed = false;
        var pos = GetBoardPosition();
        Debug.Log($"hover pos: {pos}");
        if (IsValidPosition(pos) && virtualCard.CanBePlayed())
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
        GameTable.cardGrabbed = virtualCard;
        GameTable.ShowAvailablePlayPositions();
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
        return GameTable.GetHoverPosition();
    }

    bool IsValidPosition(Vector2Int pos)
    {
        return virtualCard.GetAvailableTargets().Contains(pos);
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
