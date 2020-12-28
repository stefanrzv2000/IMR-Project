using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheckX : MonoBehaviour
{
    PlayerController playerController;
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(true);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(true);
    }

    void OnColissionExit(Collision other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(false);
    }

    void OnColissionStay(Collision other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(true);
    }
}
