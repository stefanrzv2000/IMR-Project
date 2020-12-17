using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adaugare_textura : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);

        // set the pixel values
        texture.SetPixel(0, 0, Color.blue);
        texture.SetPixel(1, 0, Color.blue);
        texture.SetPixel(0, 1, Color.blue);
        texture.SetPixel(1, 1, Color.green);

        // Apply all SetPixel calls
        texture.Apply();

        // connect texture to material of GameObject this script is attached to
        GameObject gura = GameObject.Find("base_gura");
        var def = gura.transform.GetChild(0).gameObject;
        def.GetComponent<Renderer>().material.mainTexture = texture;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///CORP
        
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        var texture2 = new Texture2D(3, 3, TextureFormat.ARGB32, false);

        // set the pixel values
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                texture2.SetPixel(i, j, Color.red);

        texture2.Apply();

        GameObject corp = GameObject.Find("corp");
        corp.GetComponent<Renderer>().material.mainTexture = texture2;




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
