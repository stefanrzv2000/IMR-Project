﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Width = 6;
    public int Height = 6;

    public OnBoardDestructible[,] Destructables;
    public OnBoardBuilding[] Buildings;

    public Board()
    {
        var onBoardOccupied = new OnBoardOccupied();

        Destructables = new OnBoardDestructible[Width, Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                    Destructables[y, x] = onBoardOccupied;
                else 
                    Destructables[y, x] = null;
            }
        }

        var playerOneNest = new OnBoardNest(1);
        var playerTwoNest = new OnBoardNest(2);

        var playerOneMageTower = new OnBoardMageTower(1);
        var playerTwoMageTower = new OnBoardMageTower(2);

        var playerOneBarrack = new OnBoardBarrack(1);
        var playerTwoBarrack = new OnBoardBarrack(2);

        Destructables[0, 3] = playerTwoNest;
        Destructables[0, 4] = playerTwoNest;

        Destructables[Height - 1, 3] = playerOneNest;
        Destructables[Height - 1, 4] = playerOneNest;


        Destructables[1, 0] = playerTwoMageTower;
        Destructables[2, 0] = playerTwoMageTower;

        Destructables[Height-2, Width-1] = playerOneMageTower;
        Destructables[Height-3, Width-1] = playerOneMageTower;


        Destructables[1, Width-1] = playerTwoBarrack;
        Destructables[2, Width-1] = playerTwoBarrack;

        Destructables[Height-2, 0] = playerOneBarrack;
        Destructables[Height-3, 0] = playerOneBarrack;

        Buildings = new OnBoardBuilding[6];
        Buildings[0] = playerOneNest;
        Buildings[1] = playerOneBarrack;
        Buildings[2] = playerOneMageTower;

        Buildings[3] = playerTwoNest;
        Buildings[4] = playerTwoBarrack;
        Buildings[5] = playerTwoMageTower;
    }
}
