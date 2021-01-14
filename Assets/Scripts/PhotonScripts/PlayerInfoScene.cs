using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoScene : MonoBehaviour
{
    public static PlayerInfoScene Instance;

    public int chosenElement = 0;
    public int playerId = 1;

    public int PhotonPresent = 0;

    public void Awake()
    {
        Debug.Log("PIS awaken");
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}
