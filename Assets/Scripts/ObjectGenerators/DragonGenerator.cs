using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dragonPrefab;
    public Transform island;
    
    private Material[][] role_material = new Material[4][];
    private Transform Board;

    // public GameObject horns;
    // private GameObject[] flames=new GameObject[5];

    private int DRAGON_BODY = 0;
    private int DRAGON_TAIL = 1;
    private int DRAGON_WING1 = 2;
    private int DRAGON_WING2 = 3;
    private int DRAGON_CROWN = 4;
    private int DRAGON_HORN1 = 5;
    private int DRAGON_HORN2 = 6;


    private int FIRE = 0;
    private int WATER = 1;
    private int EARTH = 2;
    private int AIR = 3;

    private int TANK = 0;
    private int MELEE = 1;
    private int RANGER = 2;
    private int QUEEN = 3;
    private int PAWN = 4;

    void Start()
    {
        Board = GameObject.Find("GameTable").transform;
        // create matrix
        string element = "";
        for (int i = 0; i < 4; i++)
        {
            role_material[i] = new Material[7];

        }


        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 7; j++)
            {
                if (i == 0)
                    element = "fire";
                else if (i == 1)
                    element = "water";
                else if (i == 2)
                    element = "earth";
                else if (i == 3)
                    element = "air";


                //element = "air";
                var index = j;
                var path = "Materials/" + element + "/" + index;
                
                var material = Resources.Load(path, typeof(Material)) as Material;
                //var texture = (Texture2D)material.mainTexture;

                role_material[i][j] = material;
            }

        //for(int i = 0; i < 4; i++)
        //{
        //    var path = "Prefabs/flames/" + i;
        //    Debug.Log(path);
        //    flames[i] = Resources.Load(path, typeof(GameObject)) as GameObject;
        //}

        /*
        CreateDragon(new Vector3(0, 0, 0), 3, 1);
        CreateDragon(new Vector3(0, 0, 10), 3, 4);
        CreateDragon(new Vector3(10, 0, 0), 3, 3);*/
        //CreateDragon(new Vector3(0, 1.11f, 0.2f), 0.01f, 90, FIRE, RANGER);
        //CreateDragon(new Vector3(0, 1.11f, 0.5f), 0.01f, 90, WATER, RANGER);
        //CreateDragon(new Vector3(0, 1.11f, 0.8f), 0.01f, 90, EARTH, RANGER);
        //CreateDragon(new Vector3(0, 1.11f, 1.1f), 0.01f, 90, AIR, RANGER);

        //GenerateAll();
        //CreateDragon(new Vector3(0,4,0), 0.1f, 90,AIR,PAWN);

    }

    void GenerateAll()
    {
        //Transform island = GameObject.Find("FloatingIsland").transform;
        //Transform island = GameObject.Find("board_island").transform;
        for(int elem = 0; elem < 4; elem++)
        {
            for(int type = 0; type < 5; type++)
            {
                //Debug.Log($"blabla {elem} {type}");
                CreateDragon(new Vector3((type-2)*0.2f, 1.11f, 0.4f + 0.2f*elem), 0.01f, 90, elem, type,parent:island);
            }
        }
    }

    public GameObject CreateDragon(int pos, int element, int type, int owner)
    {
        var parent = Board.GetChild(pos);

        var dragon = CreateDragon(new Vector3(0,0,0), 0.01f, -90 + 180 * owner, element, type, parent:parent);
        float multiplier = owner == 1 ? -1 : 1;
        dragon.transform.localPosition = new Vector3(multiplier * 0.24f, 0.018f, multiplier * 0.43f);

        return dragon;
    }

    public GameObject CreateDragon(Vector3 pos, float scale, float angle, int element, int type, bool flame = false, Transform parent = null)
    {
        var dragon = GameObject.Instantiate(dragonPrefab);
        dragon.transform.position = pos;

        var body = dragon.transform.GetChild(DRAGON_BODY).gameObject;
        var tail = dragon.transform.GetChild(DRAGON_TAIL).gameObject;
        var wing1 = dragon.transform.GetChild(DRAGON_WING1).GetChild(0).gameObject;
        var wing2 = dragon.transform.GetChild(DRAGON_WING2).GetChild(0).gameObject;
        var crown = dragon.transform.GetChild(DRAGON_CROWN).gameObject;
        var horn1 = dragon.transform.GetChild(DRAGON_HORN1).gameObject;
        var horn2 = dragon.transform.GetChild(DRAGON_HORN2).gameObject;


        // TODO: add textures

        var identity = (element, type);

        var texture = role_material[identity.Item1][identity.Item2];
        var tail_wings_texture = role_material[identity.Item1][5];
        var horns_texture = role_material[identity.Item1][6];

        for(int i = 0; i < body.transform.childCount; i++)
        {
            body.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = texture;
        }
        //body.GetComponent<Renderer>().material = texture;

        
        wing1.GetComponent<Renderer>().material = tail_wings_texture;

        
        wing2.GetComponent<Renderer>().material = tail_wings_texture;

        
        tail.GetComponent<Renderer>().material = tail_wings_texture;

        /*
        GameObject horn1 = Instantiate(horns) as GameObject;
        horn1.transform.parent = dragon.transform;
        horn1.transform.localPosition = new Vector3(-3.02f, 7.19f, -1.75f);


        GameObject horn2 = Instantiate(horns) as GameObject;
        horn2.transform.parent = dragon.transform;
        horn2.transform.localPosition = new Vector3(-3.03f, 7.2f, -8.4f);
        horn2.transform.localScale = new Vector3(1, 1, -1);
        */
       

        horn1.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = horns_texture;
        horn2.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = horns_texture;

        if ((identity.Item2 == 0) || (identity.Item2 == 4))
        {
            wing1.GetComponent<Renderer>().enabled = false;
            wing2.GetComponent<Renderer>().enabled = false;

        }


        crown.SetActive(false);
        if (identity.Item2 == 3)
        {
            crown.SetActive(true);
        }

        //if (flame)
        //{
        //    GameObject virtual_flame = Instantiate(flames[identity.Item1]) as GameObject;
        //    virtual_flame.transform.parent = dragon.transform;
        //    virtual_flame.transform.localPosition = new Vector3(0, -2.22f, -5.34f);
        //    //var psmain = virtual_flame.GetComponent<ParticleSystem>().main;
        //    //psmain.startSizeMultiplier = scale;
        //}

        dragon.transform.localScale = new Vector3(scale, scale, scale);
        dragon.transform.Rotate(Vector3.up, angle);

        if (parent)
        {
            dragon.transform.parent = parent;
        }
        return dragon;
    }
}
