using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TileController : MonoBehaviour
{
    public VRTK_InteractableObject linkedObject;
    public int myIndex;
    private TableColors Table;

    // Start is called before the first frame update
    void Start()
    {
        Table = transform.parent.parent.gameObject.GetComponent<TableColors>();
    }

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched += InteractableObjectUntouched;
        }

    }

    private void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        
    }

    private void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log($"touched {myIndex}");
        Table.SetHover(myIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
