using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardDestructible : MonoBehaviour
{
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

    public void ReceiveDamage(int attack)
    {
        Health -= attack;
        if (Health > 0)
            return;

        Board.Destructables[BoardY, BoardX] = null;
        Alive = false;
        Destroy(this);
    }

    public Vector2 GetProjection2D()
    {
        return new Vector2(ProjectX, ProjectY);
    }
}
