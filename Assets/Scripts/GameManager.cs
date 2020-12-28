using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject neck = GameObject.Find("Neck");
        GameObject player = GameObject.Find("Player");

#if UNITY_EDITOR_WIN
        Debug.Log("WINDOWS!!");
        player.SetActive(false);
        neck.SetActive(true);
#else
        Debug.Log("ANDROID!!");
        player.SetActive(true);
        neck.SetActive(false);
#endif
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
