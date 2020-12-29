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

        PlayerInfoScene infos = PlayerInfoScene.Instance;
        if(infos.playerId == 2)
        {
            Transform playerTransform = GameObject.Find("PlayerObject").transform;
            playerTransform.position = new Vector3(0, 0, 1.4f);
            playerTransform.Rotate(new Vector3(0, 180, 0));
        }

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
