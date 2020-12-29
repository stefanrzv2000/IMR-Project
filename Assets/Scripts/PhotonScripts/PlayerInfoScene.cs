using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoScene : MonoBehaviour
{
    public static PlayerInfoScene Instance;

    public int chosenElement;
    public int playerId;

    public void Awake()
    {
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
