using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VRHandController;

public class ControllerTracker : MonoBehaviour
{
    public GameObject tracked = null;
    private Material m0, m1;
    int curr = -1;
    float[] limx, limz;
    private TableColors tableColors;

    public HandType handType = HandType.RIGHT;
    private string handName;
    // Start is called before the first frame update
    void Start()
    {
        handName = handType == HandType.RIGHT ? "RightHand" : "LeftHand";

        tableColors = GetComponent<TableColors>();
        //transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = selected;

        limx = new float[2];
        limz = new float[2];

        limx[0] = transform.position.x - 1.04f / 2;
        limx[1] = transform.position.x + 1.04f / 2;
        limz[0] = transform.position.z - 1.04f / 2;
        limz[1] = transform.position.z + 1.04f / 2;
        Debug.Log($"limx {limx[0]} {limx[1]}");
        Debug.Log($"limz {limz[0]} {limz[1]}");
    }

    int GetSelectedChild()
    {
        float tx = tracked.transform.position.x;
        float tz = tracked.transform.position.z;

        if (limx[0] < tx && tx < limx[1] && limx[0] < tz && tz < limz[1])
        {
            int j = tableColors.HEIGHT - 1 - (int)(tableColors.HEIGHT * (limx[1] - tx) / (limx[1] - limx[0]));
            int i = (int)(tableColors.HEIGHT * (limz[1] - tz) / (limz[1] - limz[0]));

            return i * tableColors.HEIGHT + j;
        }

        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (tracked == null)
        {
            tracked = GameObject.Find(handName);
        }
        if (tracked != null)
        {
            tableColors.SetHover(GetSelectedChild());
        }
    }
}
