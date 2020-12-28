using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTrack : MonoBehaviour
{
    public Material selected;
    public GameObject tracked;
    private Material m0, m1;
    int curr = -1;
    float[] limx, limz;

    // Start is called before the first frame update
    void Start()
    {
        m0 = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        m1 = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material;
        //transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = selected;

        limx = new float[2];
        limz = new float[2];

        limx[0] = transform.position.x - transform.localScale.x / 2;
        limx[1] = transform.position.x + transform.localScale.x / 2;
        limz[0] = transform.position.z - transform.localScale.z / 2;
        limz[1] = transform.position.z + transform.localScale.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"x {limx}, z {limz}");
        ChangeColor(GetSelectedChild());
    }

    int GetSelectedChild()
    {
        float tx = tracked.transform.position.x;
        float tz = tracked.transform.position.z;

        if (limx[0] < tx && tx < limx[1] && limx[0] < tz && tz < limz[1])
        {
            int i = (int)(5*(limx[1] - tx) / (limx[1] - limx[0]));
            int j = (int)(5*(limz[1] - tz) / (limz[1] - limz[0]));

            return j * 5 + i;
        }

        return -1;
    }

    void ChangeColor(int index)
    {
        if (index!=curr)
        {
            if(curr >= 0)  transform.GetChild(curr).gameObject.GetComponent<MeshRenderer>().material = curr % 2 == 1 ? m1 : m0;
            if(index >= 0) transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material = selected;
            curr = index;
        }
    }
}
