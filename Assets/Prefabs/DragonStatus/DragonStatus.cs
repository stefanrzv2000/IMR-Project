using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonStatus : MonoBehaviour
{
    Text text1;
    Text text2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void UpdateStatus(int stat)
    {
        if(text1 == null)
        {
            text1 = transform.Find("Canvas1").GetChild(0).gameObject.GetComponent<Text>();
            text2 = transform.Find("Canvas2").GetChild(0).gameObject.GetComponent<Text>();
        }
        text1.text = stat.ToString();
        text2.text = stat.ToString();
    }

    public void UpdateStatus(string stat)
    {
        if (text1 == null)
        {
            text1 = transform.Find("Canvas1").GetChild(0).gameObject.GetComponent<Text>();
            text2 = transform.Find("Canvas2").GetChild(0).gameObject.GetComponent<Text>();
        }
        text1.text = stat;
        text2.text = stat;
    }

    public void UpdateStatus(Color color)
    {
        if (text1 == null)
        {
            text1 = transform.Find("Canvas1").GetChild(0).gameObject.GetComponent<Text>();
            text2 = transform.Find("Canvas2").GetChild(0).gameObject.GetComponent<Text>();
        }
        text1.color = color;
        text2.color = color;
    }
}
