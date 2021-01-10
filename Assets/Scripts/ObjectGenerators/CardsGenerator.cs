using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsGenerator
{
    private GameObject dragonCard;
    private GameObject spellCard;
    private GameObject LastPlayedCard;
    public Material[,] card_materials = new Material[4,9];
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

    private int TIER1_SPELL = 5;
    private int TIER2_SPELL = 6;
    private int TIER3_SPELL = 7;
    private int TIER4_SPELL = 8;
    private int TIER5_SPELL = 9;

    private bool done = false;

    public CardsGenerator(GameObject dragonCardPrefab, GameObject spellCardPrefab)
    {
        dragonCard = dragonCardPrefab;
        spellCard = spellCardPrefab;

        cardHolder = GameObject.Find("CardHolder").transform;

        string element = "";
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 9; j++)
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

                card_materials[i,j] = material;
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

    public GameObject CreateCard(int index, CardSpell card, bool flame = false, float scale = 0.1f, float angle = 270)
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

        int element = card.Race;
        int type = card.ID + 4;

        GameObject virtual_card = GameObject.Instantiate(spellCard) as GameObject;
        virtual_card.transform.parent = cardHolder;
        virtual_card.transform.localPosition = pos;
        virtual_card.transform.localScale = new Vector3(scale, scale, scale);
        virtual_card.transform.Rotate(Vector3.up, angle);

        virtual_card.transform.GetChild(0).Find("Front").gameObject.GetComponent<Renderer>().material = card_materials[element, type];

        Transform canvas = virtual_card.transform.GetChild(0).Find("Canvas");
        canvas.Find("CostText").gameObject.GetComponent<Text>().text = card.ManaCost.ToString();
        canvas.Find("Description").gameObject.GetComponent<Text>().text = card.Description;
        
        return virtual_card;
    }

    public GameObject CreateCard(int index, CardDragon card, bool flame = false, float scale = 0.1f, float angle = 270)
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

        int element = card.Race;
        int type = card.Type;

        GameObject virtual_card = GameObject.Instantiate(dragonCard) as GameObject;
        virtual_card.transform.parent = cardHolder;
        virtual_card.transform.localPosition = pos;
        virtual_card.transform.localScale = new Vector3(scale, scale, scale);
        virtual_card.transform.Rotate(Vector3.up, angle);

        virtual_card.transform.GetChild(0).Find("Front").gameObject.GetComponent<Renderer>().material = card_materials[element,type];

        Transform canvas = virtual_card.transform.GetChild(0).Find("Canvas");
        canvas.Find("HealthText").gameObject.GetComponent<Text>().text = card.MaxHealth.ToString();
        canvas.Find("AttackText").gameObject.GetComponent<Text>().text = card.Attack.ToString();
        canvas.Find("CostText")  .gameObject.GetComponent<Text>().text = card.GoldCost.ToString();
        canvas.Find("RangeText") .gameObject.GetComponent<Text>().text = card.Range.ToString();
        canvas.Find("SpeedText") .gameObject.GetComponent<Text>().text = card.Speed.ToString();

        return virtual_card;
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

        GameObject virtual_card = GameObject.Instantiate(dragonCard) as GameObject;
        virtual_card.transform.parent = cardHolder;
        virtual_card.transform.localPosition = pos;
        virtual_card.transform.localScale = new Vector3(scale, scale, scale);
        virtual_card.transform.Rotate(Vector3.up, angle);

        virtual_card.transform.GetChild(0).Find("Front").gameObject.GetComponent<Renderer>().material = card_materials[element,type];

        return virtual_card;

    }

    public void UpdateLastCard(int element, int type)
    {
        if(LastPlayedCard == null)
        {
            LastPlayedCard = GameObject.Find("LastPlayedCard");
            LastPlayedCard.transform.GetChild(0).gameObject.SetActive(true);
        }
        if(LastPlayedCard != null)
        {
            LastPlayedCard.transform.GetChild(0).Find("Front").gameObject.GetComponent<MeshRenderer>().material = card_materials[element, type];
            if(type < 4) // it's a dragon card
            {
                CardDragon card = new CardDragon(type, element, 1);
                Transform canvas = LastPlayedCard.transform.GetChild(0).Find("Canvas");
                canvas.Find("HealthText").gameObject.GetComponent<Text>().text = card.MaxHealth.ToString();
                canvas.Find("HealthText").gameObject.SetActive(true);
                canvas.Find("AttackText").gameObject.GetComponent<Text>().text = card.Attack.ToString();
                canvas.Find("AttackText").gameObject.SetActive(true);
                canvas.Find("RangeText").gameObject.GetComponent<Text>().text = card.Range.ToString();
                canvas.Find("RangeText").gameObject.SetActive(true);
                canvas.Find("SpeedText").gameObject.GetComponent<Text>().text = card.Speed.ToString();
                canvas.Find("SpeedText").gameObject.SetActive(true);
                canvas.Find("CostText").gameObject.GetComponent<Text>().text = card.GoldCost.ToString();
                canvas.Find("Description").gameObject.SetActive(false);
            }
            else // it's a spell
            {
                CardSpell card = CardSpellCreator.GenerateCardSpell(type - 4, 1, element);
                Transform canvas = LastPlayedCard.transform.GetChild(0).Find("Canvas");
                canvas.Find("HealthText").gameObject.SetActive(false);
                canvas.Find("AttackText").gameObject.SetActive(false);
                canvas.Find("RangeText").gameObject.SetActive(false);
                canvas.Find("SpeedText").gameObject.SetActive(false);
                canvas.Find("Description").gameObject.SetActive(true);
                canvas.Find("CostText").gameObject.GetComponent<Text>().text = card.ManaCost.ToString();
                canvas.Find("Description").gameObject.GetComponent<Text>().text = card.Description;
            }
        }
    }
}
