using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVCam : MonoBehaviour
{
    public RawImage dest;
    WebCamTexture webCamTexture;
    RawImage img;
    RenderTexture rt;
    bool resized = false;
    //bool resized = false;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        img = GetComponent<RawImage>();
        //img.material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        //img.texture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        webCamTexture.Play();
    }

    Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
    {
        if (!resized)
        {
            rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            resized = true;
        }

        //var old_rt = RenderTexture.active;
        //Destroy(old_rt);
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        Destroy(texture2D);
        return result;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void NotUpdate()
    {
        var old_tex = img.texture;
        var w = webCamTexture.width;
        var h = webCamTexture.height;
        //if (!resized) resized = ((Texture2D)img.texture).Resize(w/2,h/2);

        var pix = webCamTexture.GetPixels();
        //System.Array.Reverse(pix);
        Texture2D tex = new Texture2D(w, h);
        tex.SetPixels(pix);
        tex.Apply();
        //tex.Resize(w / 2, h / 2);
        //tex.Apply();
        tex = Resize(tex, w / 2, h / 2);
        dest.texture = tex;
        dest.material.mainTexture = tex;
        //Destroy(old_tex);

        //Debug.Log("Image size");
        //Debug.Log(h);
        //Debug.Log(w);
        img.rectTransform.sizeDelta = new Vector2(tex.width, tex.height);
        dest.rectTransform.sizeDelta = new Vector2(tex.width, tex.height);
        UpdatePic();
    }

    void UpdatePic()
    {
        //yield return new WaitForEndOfFrame();
        //GetComponent<Renderer>().material.mainTexture = webCamTexture;
        //webCamTexture.Play();
    }
}
