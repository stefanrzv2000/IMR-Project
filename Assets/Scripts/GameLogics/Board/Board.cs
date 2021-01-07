using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board 
{
    public int Width = 8;
    public int Height = 8;

    public OnBoardDestructible[,] Destructables;
    public OnBoardBuilding[] Buildings;

    public GameObject PhysicBoard;

    protected const int DESTRUCTIBLE = 0;
    protected const int DRAGON = 1;
    protected const int OCCUPIED = 2;
    protected const int BUILDING = 3;
    protected const int NEST = 4;
    protected const int BARRACK = 5;
    protected const int MAGE_TOWER = 6;

    public GameReferee GameReferee;

    public List<OnBoardDragon> GetAllDragons()
    {
        List<OnBoardDragon> result = new List<OnBoardDragon>();

        for (int y = 1; y < Height - 1; y++)
        {
            for (int x = 1; x < Width - 1; x++)
            {
                if (Destructables[y, x] == null)
                    continue;
                if (Destructables[y, x].DestructibleType != DRAGON)
                    continue;
                result.Add((OnBoardDragon)Destructables[y, x]);
            }
        }

        return result;
    }

    public List<OnBoardDestructible> GetAllDestructibles()
    {
        List<OnBoardDestructible> result = new List<OnBoardDestructible>();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Destructables[y, x] == null)
                    continue;
                result.Add(Destructables[y, x]);
            }
        }

        return result;
    }

    public List<OnBoardDragon> GetAllDragonsOfOwner(int owner)
    {
        List<OnBoardDragon> result = new List<OnBoardDragon>();

        var allDragons = GetAllDragons();
        foreach (var dragon in allDragons)
        {
            if (dragon.Owner == owner)
                result.Add(dragon);
        }

        return result;
    }

    public List<OnBoardDestructible> GetAllDestructiblesOfOwner(int owner)
    {
        List<OnBoardDestructible> result = new List<OnBoardDestructible>();

        var allDestructibles = GetAllDestructibles();
        foreach (var destructible in allDestructibles)
        {
            if (destructible.Owner == owner)
                result.Add(destructible);
        }

        return result;
    }

    public List<Vector2Int> GetEmptyPositions()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        for (int y = 1; y < Height - 1; y++)
        {
            for (int x = 1; x < Width - 1; x++)
            {
                if (Destructables[y, x] == null)
                {
                    result.Add(new Vector2Int(x, y));
                }
            }
        }

        return result;
    }

    public List<Vector2Int> GetAllInnerPositions()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        for (int y = 1; y < Height - 1; y++)
        {
            for (int x = 1; x < Width - 1; x++)
            {
                result.Add(new Vector2Int(x, y));
            }
        }

        return result;
    }

    public Board(GameReferee gameReferee)
    {
        GameReferee = gameReferee;

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

    public void DebugDestructibles()
    {
        for(int i = 0; i < Height; i++)
        {
            string s = "";
            for(int j = 0; j < Width; j++)
            {
                s += Destructables[i, j].DestructibleType + " ";
            }
            Debug.Log($"Board {i} {s}");
        }
    }
}