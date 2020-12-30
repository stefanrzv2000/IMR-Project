using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

public class UseEventsController : MonoBehaviour
{
    public static UseEventsController Instance;

    public UnityEvent BoardObjectUsed;
    public UnityEvent BoardObjectUnused;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBoardObjectUsed()
    {
        Debug.Log("On Board Object Used Invoked");
        BoardObjectUsed.Invoke();
    }

    public void OnBoardObjectUnused()
    {
        Debug.Log("On Board Object Unused Invoked");
        BoardObjectUnused.Invoke();
    }
    
}
