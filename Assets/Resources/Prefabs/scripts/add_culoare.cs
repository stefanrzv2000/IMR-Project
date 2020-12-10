using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_culoare : MonoBehaviour
{
    public GameObject horns;
    public GameObject tail;
    public Material[][] role_material=new Material[4][];

    //role[i][j] for each dragon
    //i in range(4) with value in order: fire,water,earth,air
    //j in range(5) with value in order: r,k,b,q,p

    
    void role_matrix_build()
    { string element="";
        for(int i = 0; i < 4; i++)
        {
            role_material[i] = new Material[7];
            
        }


        for (int i = 0; i < 4; i++)
            for (int j =0;j<7;j++)
        {
            if (i ==0)
                element = "fire";
            else if (i ==1)
                element = "water";
            else if (i ==2)
                 element = "earth";
            else if (i ==3)
                element = "air";


                //element = "air";
                var index = j;
            var path = "Materials/" + element + "/" + index;
            Debug.Log(path);
                var material = Resources.Load(path, typeof(Material)) as Material;
            //var texture = (Texture2D)material.mainTexture;
            
            role_material[i][j] = material;
        }
        
    }


    void Start()
    {
        var identity = (2,1);
        role_matrix_build();


        
        var texture = role_material[identity.Item1][identity.Item2];
        var tail_wings_texture = role_material[identity.Item1][5];
        var horns_texture = role_material[identity.Item1][6];

        GameObject body = GameObject.Find("dragon_body");
        body.GetComponent<Renderer>().material = texture;

        GameObject wing1 = GameObject.Find("wing1_body");
        wing1.GetComponent<Renderer>().material = tail_wings_texture;

        GameObject wing2 = GameObject.Find("wing2_body");
        wing2.GetComponent<Renderer>().material = tail_wings_texture;

        GameObject tail = GameObject.Find("tail_body");
        tail.GetComponent<Renderer>().material = tail_wings_texture;


        GameObject virtual_horn1 = Instantiate(horns) as GameObject;
        virtual_horn1.transform.parent = this.transform;
        virtual_horn1.transform.localPosition = new Vector3(-3.02f, 7.19f, -1.75f);


        GameObject virtual_horn2 = Instantiate(horns) as GameObject;
        virtual_horn2.transform.parent = this.transform;
        virtual_horn2.transform.localPosition = new Vector3(-3.03f, 7.2f, -8.4f);
        virtual_horn2.transform.localScale=new Vector3(1, 1, -1);

       // virtual_horn1.transform.GetChild(0).gameObject.AddComponent<MeshRenderer>(); 
       // virtual_horn2.AddComponent<MeshRenderer>();

        virtual_horn1.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = horns_texture;
        virtual_horn2.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = horns_texture;

        if((identity.Item1 == 0) || (identity.Item2== 4)){
            wing1.GetComponent<Renderer>().enabled = false;
            wing2.GetComponent<Renderer>().enabled = false;

        }

        GameObject crown = GameObject.Find("crown");
        crown.active = false;
        if (identity.Item2 == 3)
        {
            crown.active = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
