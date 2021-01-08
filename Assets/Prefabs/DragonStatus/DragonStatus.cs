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
            text1 = transform.GetChild(1).Find("Text").gameObject.GetComponent<Text>();
            text2 = transform.GetChild(2).Find("Text").gameObject.GetComponent<Text>();
        }
        text1.text = stat.ToString();
        text2.text = stat.ToString();
    }
}
