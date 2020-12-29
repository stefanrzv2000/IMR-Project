﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoScene : MonoBehaviour
{
    public static PlayerInfoScene Instance;

    public int chosenElement = 0;
    public int playerId = 1;

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
