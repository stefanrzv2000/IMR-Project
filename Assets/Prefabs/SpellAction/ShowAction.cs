using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShowActionType
{
    LIGHTNING,
    FIRE,

}

public class ShowAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Show(ShowActionType.LIGHTNING);
        }
        else if(Input.GetKeyUp(KeyCode.L))
        {
            Hide(ShowActionType.LIGHTNING);
        }
    }

    public void Show(ShowActionType action)
    {
        GetObjectForAction(action).SetActive(true);
    }

    public void Hide(ShowActionType action)
    {
        GetObjectForAction(action).SetActive(false);
    }

    public GameObject GetObjectForAction(ShowActionType _action)
    {
        switch (_action)
        {
            case ShowActionType.FIRE:
                return transform.Find("fire").gameObject;
            case ShowActionType.LIGHTNING:
                return transform.Find("lightning").gameObject;
        }
        return null;
    }
}
