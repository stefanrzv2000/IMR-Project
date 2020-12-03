using UnityEngine;
using MediapipeHandTracking;

public class ARHand
{
    private Vector3[] landmarks, landmarksCP = default;
    public float currentDepth = 0f;
    public Camera cam;
    public Camera cam1;
    public Camera cam2;

    public ARHand()
    {
        landmarks = new Vector3[21];
        cam = Camera.main;
        cam1 = GameObject.Find("CamPost")?.GetComponent<Camera>();
        cam2 = GameObject.Find("NoVRCamGroup")?.GetComponent<Camera>();
    }

    private ARHand(Vector3[] landmarks)
    {
        this.landmarks = landmarks;
    }

    public void ParseFrom(float[] arr, float c)
    {
        if (cam1 != null && cam1.isActiveAndEnabled)
        {
            cam = cam1;
        }
        else if(cam2 != null && cam2.isActiveAndEnabled)
        {
            cam = cam2;
        }
        else
        {
            cam = Camera.main;
        }

        Debug.Log("Trying to parse");
        Debug.Log($"Arr len = {arr.Length}");
        Debug.Log($"c = {c}");

        if (null == arr || arr.Length < 63) return;
        //độ sâu của điểm ở cổ tay

        if (c > 0)
        {
            for (int i = 0; i < 21; i++)
            {
                float xScreen = Screen.width * ((arr[i * 3 + 1] - 0.5f * (1 - c)) / c);
                // float xScreen = Screen.width * ((arr[i * 3 + 1] ) / c);
                float yScreen = Screen.height * (arr[i * 3]);
                //this.landmarks[i] = cam.ScreenToWorldPoint(new Vector3(xScreen, yScreen, arr[i * 3 + 2] / 60 + 0.4f));
                this.landmarks[i] = cam.ScreenToWorldPoint(new Vector3(xScreen, yScreen, 0));
            }
            Debug.Log("Calculated 1st");
        }
        else
        {
            for (int i = 0; i < 21; i++)
            {
                // float yScreen = Screen.width * ((arr[i * 3 + 1] - 0.5f * (1 - c)) / c);
                float yScreen = Screen.height * ((1 - arr[i * 3 + 1]) * (-c) + (1 + c) / 2);
                float xScreen = Screen.width * (arr[i * 3]);
                //this.landmarks[i] = cam.ScreenToWorldPoint(new Vector3(xScreen, yScreen, arr[i * 3 + 2] / 60 + 0.4f));
                this.landmarks[i] = cam.ScreenToWorldPoint(new Vector3(xScreen, yScreen, 0));
                //this.landmarks[i] = new Vector3((arr[i * 3 + 1]), arr[i * 3], this.landmarks[i].z);
                Debug.Log($"Hand {i} z = {landmarks[i].z}");

                if (i == 0)
                {
                    Debug.Log("Hand 0");
                    Debug.Log(arr[i * 3] + ", " + arr[i * 3 + 1] + ", " + arr[i * 3 + 2]);
                }
            }
            Debug.Log("Calculated 2nd");
        }

        if (landmarksCP == default)
        {
            landmarksCP = new Vector3[21];
            landmarksCP = (Vector3[])landmarks.Clone();
        }
        else
        {
            // nễu bị rung giữ nguyên landmark cũ
            if (isVibrate(0.02f))
            {
                landmarks = (Vector3[])landmarksCP.Clone();
            }
            else
            { // lưu lại landmark khi không bị rung
                landmarksCP = (Vector3[])landmarks.Clone();
            }
        }
    }

    public bool isVibrate(float deltaVibrate)
    {
        for (int i = 0; i < 21; i++)
        {
            if (Vector3.Distance(landmarksCP[i], landmarks[i]) > deltaVibrate) return false;
        }
        return true;
    }

    public Vector3 GetLandmark(int index) => this.landmarks[index];
    public Vector3[] GetLandmarks() => this.landmarks;
    public ARHand Clone()
    {
        return new ARHand((Vector3[])landmarks.Clone());
    }

}