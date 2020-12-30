﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileColor
{
    TILE1,
    TILE2,
    HOVER1,
    INVISIBLE,
    ATTACK,
    MOVE,
}

public class TableColors : MonoBehaviour
{
    public Material tile1;
    public Material tile2;
    public Material hover1;
    public Material invisible;
    public Material availableAttack;
    public Material availableMove;

    public float elevation = 0.01f;

    private int hoverIndex = -1;
    public int HEIGHT = 8;
    public int SIZE = 64;

    private TileColor[] currentColors;

    // Start is called before the first frame update
    void Start()
    {
        currentColors = new TileColor[SIZE];
        for(int i = 0; i < SIZE; i++)
        {
            GameObject tile = transform.GetChild(i).GetChild(0).gameObject;
            tile.GetComponent<TileController>().myIndex = i;
            currentColors[i] = GetTileColorForIndex(i);
            SetColor(i, currentColors[i]);
            if (GetTileColorForIndex(i) == TileColor.TILE1)
            {
                tile.transform.localPosition += new Vector3(0,elevation,0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    TileColor GetTileColorForIndex(int index)
    {
        int i = index / HEIGHT;
        int j = index % HEIGHT;

        if (i == 0 || j == 0 || i == HEIGHT - 1 || j == HEIGHT - 1)
        {
            return TileColor.INVISIBLE;
        }

        return (i + j) % 2 == 0 ? TileColor.TILE1 : TileColor.TILE2;
    }

    public void SetCurrentColor(int index, TileColor color)
    {
        currentColors[index] = color;
        SetToCurrentColor(index);
    }

    public void SetHover(int index)
    {
        if(0 <= hoverIndex && hoverIndex < SIZE)
        {
            SetColor(hoverIndex, currentColors[hoverIndex]);    
        }
        if(0 <= index && index < SIZE)
        {
            SetColor(index, TileColor.HOVER1);
            hoverIndex = index;
        }
        //Debug.Log($"Hover: {GetHoverPosition()}");
    }

    public void ResetColor(int index)
    {
        if (0 <= index && index < SIZE)
        {
            currentColors[index] = GetTileColorForIndex(index);
            SetToCurrentColor(index);
        }
    }

    public void SetToCurrentColor(int index)
    {
        if (0 <= index && index < SIZE)
        {
            SetColor(index, currentColors[index]);
        }
    }

    public void ResetAll()
    {
        for(int i = 0; i < SIZE; i++)
        {
            ResetColor(i);
        }
    }

    void SetColor(int index, TileColor color)
    {
        Material mat = null;
        switch (color)
        {
            case TileColor.TILE1:
                mat = tile1;
                break;
            case TileColor.TILE2:
                mat = tile2;
                break;
            case TileColor.HOVER1:
                mat = hover1;
                break;
            case TileColor.INVISIBLE:
                mat = invisible;
                break;
            case TileColor.ATTACK:
                mat = availableAttack;
                break;
            case TileColor.MOVE:
                mat = availableMove;
                break;
            default:
                break;
        }
        if (mat != null)
        {
            transform.GetChild(index).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = mat;
        }
    }

    public Vector2Int GetHoverPosition()
    {
        if(0 <= hoverIndex && hoverIndex < SIZE)
        {
            return new Vector2Int(hoverIndex / HEIGHT, hoverIndex % HEIGHT);
        }
        return new Vector2Int(-1, -1);
    }
}
