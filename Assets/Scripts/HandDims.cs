using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDims : MonoBehaviour
{
    public float dim01 = 0.070f;
    public float dim02 = 0.078f;
    public float dim12 = 0.063f;

    public float[] GetDims()
    {
        float[] dims = { dim01, dim02, dim12 };
        return dims;
    }
}
