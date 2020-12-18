using MediapipeHandTracking;
using UnityEngine;

public class ARHandProcessor : MonoBehaviour
{
    private GameObject Hand = default;
    private GameObject TriHand = default;
    private GameObject Triangle = default;
    private HandRect currentHandRect = default;
    private HandRect oldHandRect = default;
    private ARHand currentHand = default;
    private bool isHandRectChange = default;
    private Geometry geo;

    public float z_factor = 1;
    public bool display_points = false;

    void Start()
    {
        Hand = HTManager.instance.HandOnSpace;
        TriHand = HTManager.instance.TriHand;
        Triangle = HTManager.instance.Triangle;
        geo = HTManager.instance.Geometry;
        currentHand = new ARHand();
        currentHandRect = new HandRect();
        oldHandRect = new HandRect();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        //Debug.Log("Chiar ruleaza");
        if (GetComponent<ARFrameProcessor>().HandProcessor == null)
        {
            //Debug.Log("I'm outta here");
            //var x = (0, 0.1);
            if (TriHand.activeInHierarchy)
            {
                //Debug.Log("Am intrat in if");

                Vector3 r1 = TriHand.transform.GetChild(1).position - TriHand.transform.GetChild(0).position;
                Vector3 r2 = TriHand.transform.GetChild(2).position - TriHand.transform.GetChild(0).position;

                //geo.PlaceObject(Triangle, realPoints[0]);
                Triangle.transform.position = TriHand.transform.GetChild(0).position;
                if (Vector3.Magnitude(r1) > 0)
                {
                    geo.RotateObjectToMatch(Triangle, r1, r2);
                }

            }
            return;
        }
        float[] handRectData = GetComponent<ARFrameProcessor>().HandProcessor.getHandRectData();
        float[] handLandmarksData = GetComponent<ARFrameProcessor>().HandProcessor.getHandLandmarksData();

        if (null != handRectData)
        {
            currentHandRect = HandRect.ParseFrom(handRectData);
            if (!isHandStay())
            {
                oldHandRect = currentHandRect;
                isHandRectChange = true;
            }
            else
            {
                isHandRectChange = false;
            }
        }

        //if (null != handLandmarksData && !float.IsNegativeInfinity(GetComponent<ARFrameProcessor>().ImageRatio))
        //{
        //    currentHand.ParseFrom(handLandmarksData, GetComponent<ARFrameProcessor>().ImageRatio);
        //}

        //if (!Hand.activeInHierarchy) return;
        //for (int i = 0; i < Hand.transform.childCount - 1; i++)
        //{
        //    Hand.transform.GetChild(i).transform.position = currentHand.GetLandmark(i);
        //    Hand.transform.GetChild(i).transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        //}
        //Hand.transform.GetChild(Hand.transform.childCount - 1).transform.position = (currentHand.GetLandmark(0) + currentHand.GetLandmark(5) + currentHand.GetLandmark(17)) / 3.0f;
        //var dist = (currentHand.GetLandmark(0) - currentHand.GetLandmark(12)).magnitude;
        //Debug.Log("size = " + dist);

        if (Hand.activeInHierarchy && display_points)
        {
            for (int i = 0; i < Hand.transform.childCount - 1; i++)
            {
                geo.PlaceObject(Hand.transform.GetChild(i).gameObject, new Vector2(2 * handLandmarksData[i * 3] - 1, -2 * handLandmarksData[i * 3 + 1] + 1));
                //Hand.transform.GetChild(i).transform.position = currentHand.GetLandmark(i);
                Hand.transform.GetChild(i).transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            }
            Hand.transform.GetChild(Hand.transform.childCount - 1).transform.position = (Hand.transform.GetChild(0).transform.position + Hand.transform.GetChild(5).transform.position + Hand.transform.GetChild(17).transform.position) / 3.0f;
            //var dist = (currentHand.GetLandmark(0) - currentHand.GetLandmark(12)).magnitude;
            //Debug.Log("size = " + dist);

            Hand.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        }

        if (TriHand.activeInHierarchy)
        {
            Debug.Log("Am intrat in if");
            float[] dims = TriHand.GetComponent<HandDims>().GetDims();
            int i = 0;
            Vector2 p1 = new Vector2(handLandmarksData[i * 3] - 0.5f, -handLandmarksData[i * 3 + 1] + 0.5f);
            i = 5;
            Vector2 p2 = new Vector2(handLandmarksData[i * 3] - 0.5f, -handLandmarksData[i * 3 + 1] + 0.5f);
            i = 17;
            Vector2 p3 = new Vector2(handLandmarksData[i * 3] - 0.5f, -handLandmarksData[i * 3 + 1] + 0.5f);

            Vector2[] imPoints = { p1, p2, p3 };

            var realPoints = geo.GetRealPoints(imPoints,dims);

            //var center = (realPoints[0] + realPoints[1] + realPoints[2])/3;

            for(i = 0; i < 3; i++)
            {
                //realPoints[i] += center*(z_factor-1);
                Debug.Log($"realpos{i}: {realPoints[i].x} {realPoints[i].y} {realPoints[i].z}");
                geo.PlaceObject(TriHand.transform.GetChild(i).gameObject, realPoints[i]);
            }

            Vector3 r1 = TriHand.transform.GetChild(1).position - TriHand.transform.GetChild(0).position;
            Vector3 r2 = TriHand.transform.GetChild(2).position - TriHand.transform.GetChild(0).position;

            var TriCenterPos = (TriHand.transform.GetChild(0).position + TriHand.transform.GetChild(1).position + TriHand.transform.GetChild(2).position) / 3;

            var displacement = (TriCenterPos - geo.origin) * (z_factor - 1);

            // displacement = Vector3.Project(displacement, geo.zaxis);

            for (i = 0; i < 3; i++)
            {
                TriHand.transform.GetChild(i).position += displacement;
            }

            //geo.PlaceObject(Triangle, realPoints[0]);
            //Vector3 r1 = TriHand.transform.GetChild(1).position - TriHand.transform.GetChild(0).position;
            //Vector3 r2 = TriHand.transform.GetChild(2).position - TriHand.transform.GetChild(0).position;

            //geo.PlaceObject(Triangle, realPoints[0]);
            Triangle.transform.position = TriHand.transform.GetChild(0).position;
            if (Vector3.Magnitude(r1) > 0)
            {
                geo.RotateObjectToMatch(Triangle, r1, r2);
            }
        }


    }

    private bool isHandStay()
    {
        return currentHandRect.XCenter == oldHandRect.XCenter &&
            currentHandRect.YCenter == oldHandRect.YCenter &&
            currentHandRect.Width == oldHandRect.Width &&
            currentHandRect.Height == oldHandRect.Height &&
            currentHandRect.Rotaion == oldHandRect.Rotaion;
    }

    public ARHand CurrentHand { get => currentHand; }
    public bool IsHandRectChange { get => isHandRectChange; }
    public HandRect CurrentHandRect { get => currentHandRect; }
}