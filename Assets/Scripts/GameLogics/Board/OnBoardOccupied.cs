using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardOccupied : OnBoardDestructible
{
    public OnBoardOccupied(Board board)
    {
        Board = board;
        DestructibleType = OCCUPIED;
    }
}