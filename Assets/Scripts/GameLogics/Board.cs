using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Width = 6;
    public int Height = 6;

    public OnBoardDragon[,] dragons;

    public Board()
    {
        dragons = new OnBoardDragon[Width, Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                dragons[y, x] = null;
            }
        }
    }
}
