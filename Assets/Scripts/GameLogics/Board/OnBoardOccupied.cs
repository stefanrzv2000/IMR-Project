using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardOccupied : OnBoardDestructible
{
    public OnBoardOccupied()
    {
        DestructibleType = OCCUPIED;
    }
}
