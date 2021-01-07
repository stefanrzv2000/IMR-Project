using System;
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

    private int tileBeingUsed = -1;
    OnBoardDestructible myDestructible = null;
    // Start is called before the first frame update

    private void LinkEvents()
    {
        UseEventsController.Instance.BoardObjectUsed.AddListener(OnTileUsed);
        UseEventsController.Instance.BoardObjectUnused.AddListener(OnTileUnused);
    }

    private void OnTileUnused()
    {
        int currentTile = GetSelectedChild();
        if (myDestructible != null && myDestructible.DestructibleType == OnBoardDestructible.DRAGON)
        {
            int i1 = tileBeingUsed / tableColors.HEIGHT;
            int j1 = tileBeingUsed % tableColors.HEIGHT;

            int i2 = currentTile / tableColors.HEIGHT;
            int j2 = currentTile % tableColors.HEIGHT;

            GameReferee.Instance.CallRPCMethod("MoveOnBoardDragon", new int[] { i1, j1 }, new int[] { i2, j2 });
        }


        tileBeingUsed = -1;
        myDestructible = null;
        tableColors.ResetAll();
    }

    private void OnTileUsed()
    {
        Debug.Log("used used");
        if (tileBeingUsed != -1) return;
        tileBeingUsed = GetSelectedChild();
        if (tileBeingUsed == -1) return;
        Debug.Log($"used {tileBeingUsed}");

        int i = tileBeingUsed / tableColors.HEIGHT;
        int j = tileBeingUsed % tableColors.HEIGHT;
        Debug.Log($"used {i} {j}");

        myDestructible = GameReferee.Instance.Board.Destructables[i, j];

        if (myDestructible != null)
        {
            Debug.Log($"There is something here {myDestructible.DestructibleType}");
        }

        if (myDestructible != null && myDestructible.DestructibleType == OnBoardDestructible.DRAGON)
        {
            Debug.Log("I am indeed a dragon");
            var movePositions = ((OnBoardDragon)myDestructible).GetMovingPositions();
            Debug.Log($"move positions: {movePositions.Count}");
            foreach (var pos in movePositions)
            {
                int index = pos.x + 8 * pos.y;
                Debug.Log($"Some position: {pos} index {index}");
                tableColors.SetCurrentColor(index, TileColor.MOVE);
            }

            var attackPositions = ((OnBoardDragon)myDestructible).GetAttackingPositions();
            Debug.Log($"move positions: {attackPositions.Count}");
            foreach (var pos in attackPositions)
            {
                int index = pos.x + 8 * pos.y;
                Debug.Log($"Some position: {pos} index {index}");
                tableColors.SetCurrentColor(index, TileColor.ATTACK);
            }
        }
    }

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

        LinkEvents();
    }

    int GetSelectedChild()
    {
        float tx = tracked.transform.position.x;
        float tz = tracked.transform.position.z;

        if (limx[0] < tx && tx < limx[1] && limz[0] < tz && tz < limz[1])
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
