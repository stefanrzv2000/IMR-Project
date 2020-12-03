using UnityEngine;
using System.Collections;

public class HTManager : MonoBehaviour
{
    #region Singleton
    public static HTManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject HandOnSpace;
    public GameObject TriHand;
    public GameObject Triangle;
    public Geometry Geometry;
    //public RaycastOnPlane RaycastOnPlane;

    public Vector3[] GetHandLandmarks()
    {
        return GetComponent<ARHandProcessor>().CurrentHand.GetLandmarks();
    }

    public Vector3 GetHandLandmark(int landmark_index)
    {
        return GetComponent<ARHandProcessor>().CurrentHand.GetLandmark(landmark_index);
    }
}