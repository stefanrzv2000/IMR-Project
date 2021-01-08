using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
            //linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            //linkedObject.InteractableObjectUntouched += InteractableObjectUntouched;
            //linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            //linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            UseEventsController.Instance.BoardObjectUnused.AddListener(ObjectUnused);
            //linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }

    }

    private void ObjectUnused()
    {
        Table.ResetAll();
    }

    protected virtual void OnDisable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            //linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            //linkedObject.InteractableObjectUntouched += InteractableObjectUntouched;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
        }

    }

    private void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        Table.ResetAll();
    }

    private void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log($"used {myIndex}");
        int i = myIndex / Table.HEIGHT;
        int j = myIndex % Table.HEIGHT;
        Debug.Log($"used {i} {j}");

        OnBoardDestructible myDestructible = GameReferee.Instance.Board.Destructables[j, i];

        if(myDestructible != null)
        {
            Debug.Log($"There is something here {myDestructible.DestructibleType}");
        }

        if(myDestructible != null && 
            myDestructible.DestructibleType == OnBoardDestructible.DRAGON && 
            myDestructible.Owner == PlayerInfoScene.Instance.playerId
            )
        {
            Debug.Log("I am indeed a dragon");
            var movePositions = ((OnBoardDragon)myDestructible).GetMovingPositions();
            Debug.Log($"move positions: {movePositions.Count}");
            foreach (var pos in movePositions)
            {
                int index = pos.x * 8 + pos.y;
                Debug.Log($"Some position: {pos} index {index}");
                Table.SetCurrentColor(index, TileColor.MOVE);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
