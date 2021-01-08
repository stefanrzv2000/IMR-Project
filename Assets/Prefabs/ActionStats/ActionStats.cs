using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusUpdateType
{
    HEALTH,
    ATTACK,
    STAR,
}

public class ActionStats : MonoBehaviour
{
    public Transform toMove;
    public float duration = 3.0f;
    //public string text = "";
    //public Color color = Color.white;
    private float time = -1;
    private bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        if(toMove == null)
        {
            toMove = transform.GetChild(0);
        }
    }

    private void OnEnable()
    {
        if (toMove == null)
        {
            toMove = transform.GetChild(0);
        }

        ShowUpdate(StatusUpdateType.STAR, "+2", Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0)
        {
            time += Time.deltaTime;
            float x = 2 * time / duration;

            toMove.localPosition = new Vector3(0, 1 - (x - 1) * (x - 1) / 4, 0);

            if (x > 2)
            {
                toMove.gameObject.SetActive(false);
                time = -1;
                if (firstTime)
                {
                    firstTime = false;
                    ShowUpdate(StatusUpdateType.ATTACK, "-2", Color.red);
                }
            }
        }
    }

    public void ShowUpdate(StatusUpdateType type ,string stat, Color color)
    {
        toMove.gameObject.SetActive(true);

        time = 0;

        DragonStatus ds = toMove.gameObject.GetComponent<DragonStatus>();
        if (ds != null)
        {
            ds.UpdateStatus(stat);
            ds.UpdateStatus(color);
        }

        if(type == StatusUpdateType.HEALTH)
        {
            toMove.Find("HeartUpdate")?.gameObject.SetActive(true);
            toMove.Find("StarUpdate")?.gameObject.SetActive(false);
            toMove.Find("SwordUpdate")?.gameObject.SetActive(false);
        }
        else if(type == StatusUpdateType.STAR)
        {
            toMove.Find("HeartUpdate")?.gameObject.SetActive(false);
            toMove.Find("StarUpdate")?.gameObject.SetActive(true);
            toMove.Find("SwordUpdate")?.gameObject.SetActive(false);
        }
        else if (type == StatusUpdateType.ATTACK)
        {
            toMove.Find("HeartUpdate")?.gameObject.SetActive(false);
            toMove.Find("StarUpdate")?.gameObject.SetActive(false);
            toMove.Find("SwordUpdate")?.gameObject.SetActive(true);
        }
    }
}
