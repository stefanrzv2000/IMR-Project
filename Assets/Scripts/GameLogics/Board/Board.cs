using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board 
{
    public int Width = 6;
    public int Height = 6;

    public OnBoardDestructible[,] Destructables;
    public OnBoardBuilding[] Buildings;

    public GameObject PhysicBoard;

    public Board()
    {
        PhysicBoard = GameObject.Find("PhysicBoard");

        var onBoardOccupied = new OnBoardOccupied(this);

        Destructables = new OnBoardDestructible[Height, Width];
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

        var playerOneNest = new OnBoardNest(1,this);
        var playerTwoNest = new OnBoardNest(2, this);

        var playerOneMageTower = new OnBoardMageTower(1, this);
        var playerTwoMageTower = new OnBoardMageTower(2, this);

        var playerOneBarrack = new OnBoardBarrack(1, this);
        var playerTwoBarrack = new OnBoardBarrack(2, this);

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

    public void ResetTurn(int owner)
    {
        for (int y = 1; y < Height-1; y++)
        {
            for (int x = 1; x < Width-1; x++)
            {
                if (Destructables[y, x] == null)
                    continue;

                if (Destructables[y, x].Owner == owner)
                {
                    OnBoardDragon dragon = (OnBoardDragon) Destructables[y, x];
                    dragon.ResetTurn();
                }

            }
        }
    }
}
