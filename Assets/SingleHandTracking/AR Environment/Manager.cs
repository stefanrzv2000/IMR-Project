using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    #region Singleton
    public static Manager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject HandOnSpace;
    public RaycastOnPlane RaycastOnPlane;
}