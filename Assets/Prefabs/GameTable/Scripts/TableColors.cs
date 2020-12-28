using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileColor
{
    TILE1,
    TILE2,
    HOVER1,
}

public class TableColors : MonoBehaviour
{
    public Material tile1;
    public Material tile2;
    public Material hover1;
    public float elevation = 0.01f;

    private int hoverIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 36; i++)
        {
            GameObject tile = transform.GetChild(i).GetChild(0).gameObject;
            tile.GetComponent<TileController>().myIndex = i;
            SetColor(i, GetTileColorForIndex(i));
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
        int i = index / 6;
        int j = index % 6;
        return (i + j) % 2 == 0 ? TileColor.TILE1 : TileColor.TILE2;
    }

    public void SetHover(int index)
    {
        if(0 <= hoverIndex && hoverIndex < 36)
        {
            SetColor(hoverIndex, GetTileColorForIndex(hoverIndex));    
        }
        if(0 <= index && index < 36)
        {
            SetColor(index, TileColor.HOVER1);
            hoverIndex = index;
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
            default:
                break;
        }
        if (mat != null)
        {
            transform.GetChild(index).GetChild(0).gameObject.GetComponent<MeshRenderer>().material = mat;
        }
    }
}
