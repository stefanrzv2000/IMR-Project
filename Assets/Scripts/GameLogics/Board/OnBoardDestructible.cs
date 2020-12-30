using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardDestructible 
{
    public static int DESTRUCTIBLE = 0;
    public static int DRAGON       = 1;
    public static int OCCUPIED     = 2;
    public static int BUILDING     = 3;
    public static int NEST         = 4;
    public static int BARRACK      = 5;
    public static int MAGE_TOWER   = 6;

    public int DestructibleType = DESTRUCTIBLE;

    public int BoardX;
    public int BoardY;

    public float ProjectX;
    public float ProjectY;

    public int MaxHealth;
    public int Health;

    public bool Alive;

    public Board Board;

    public int Owner;

    public string Name;

    public GameObject PhysicInstance;

    public void ReceiveDamage(int attack)
    {
        Health -= attack;
        if (Health > 0)
            return;

        Board.Destructables[BoardY, BoardX] = null;
        Alive = false;
        GameObject.Destroy(PhysicInstance);
        //Destroy(this);
    }

    public Vector2 GetProjection2D()
    {
        return new Vector2(ProjectX, ProjectY);
    }
}
