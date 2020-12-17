using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGenerator : MonoBehaviour
{
    public static GameObject cards;
    public static Material[][] cards_material = new Material[4][];

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
        string element = "";
        for (int i = 0; i < 4; i++)
        {
            cards_material[i] = new Material[4];

        }


        for (int i = 0; i < 4; i++)
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

        //    CreateCard(1, 0.1f, 270, FIRE, TANK);
      //  CreateCard(2, 0.1f, 270, AIR, TANK);
        //CreateCard(new Vector3(0.8f, 2.6f, 0), 0.1f, 270, FIRE, TANK);



    }
     public static GameObject CreateCard(int index, float scale, float angle, int element, int type, bool flame = false, Transform parent = null)
    {

        var width = 0.2f;
        var gap = 0.05f;
        var pos = new Vector3(0.8f , 2.6f, 0 + index * (width + gap));

        GameObject virtual_card = Instantiate(cards) as GameObject;
        //virtual_card.transform.parent = this.transform;
        virtual_card.transform.localPosition = pos;
        virtual_card.transform.localScale = new Vector3(scale, scale, scale);
        virtual_card.transform.Rotate(Vector3.up, angle);

        virtual_card.transform.GetChild(0).Find("Front").gameObject.GetComponent<Renderer>().material = cards_material[element][type];

        return virtual_card;

    }
    void Update()
    {
        
    }
}
