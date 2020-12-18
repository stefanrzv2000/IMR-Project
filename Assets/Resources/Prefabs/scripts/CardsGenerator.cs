using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGenerator
{
    private GameObject cards;
    public Material[][] cards_material = new Material[4][];
    public float width = 0.11f;
    public float height = 0.07f;

    public Transform cardHolder;

    private int FIRE = 0;
    private int WATER = 1;
    private int EARTH = 2;
    private int AIR = 3;

    private int TANK = 0;
    private int MELEE = 1;
    private int RANGER = 2;
    private int QUEEN = 3;
    private int PAWN = 4;

    private bool done = false;

    public CardsGenerator(GameObject cardPrefab)
    {
        cards = cardPrefab;
        cardHolder = GameObject.Find("CardHolder").transform;

        string element = "";
        for (int i = 0; i < 4; i++)
        {
            cards_material[i] = new Material[4];
        }
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
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
                var path = "Materials/cards/" + element + "/" + index;
                Debug.Log(path);
                var material = Resources.Load(path, typeof(Material)) as Material;
                //var texture = (Texture2D)material.mainTexture;

                cards_material[i][j] = material;
            }
        }

        //for(int i = 0; i < 10; i++)
        //{
        //    CreateCard(i, 0.1f, 270, Random.Range(0, 4), Random.Range(0, 4));
        //}

        //CreateCard(1, 0.1f, 270, FIRE, TANK);
        //CreateCard(0, 0.1f, 270, AIR, TANK);

        //CreateCard(new Vector3(0.8f, 2.6f, 0), 0.1f, 270, FIRE, TANK);


        done = true;
    }

    public GameObject CreateCard(int index, int element, int type, bool flame = false, float scale = 0.1f, float angle = 270)
    {
        if (!done)
        {
            Debug.Log("Am apelat inainte de done");
            return null;
        }
        var hh = 1;
        if (index >= 5)
        {
            index -= 5;
            hh = -1;
        }
        var pos = new Vector3(0 + (index - 2) * width, hh * height, -0.1f);

        GameObject virtual_card = GameObject.Instantiate(cards) as GameObject;
        virtual_card.transform.parent = cardHolder;
        virtual_card.transform.localPosition = pos;
        virtual_card.transform.localScale = new Vector3(scale, scale, scale);
        virtual_card.transform.Rotate(Vector3.up, angle);

        virtual_card.transform.GetChild(0).Find("Front").gameObject.GetComponent<Renderer>().material = cards_material[element][type];

        return virtual_card;

    }
}
