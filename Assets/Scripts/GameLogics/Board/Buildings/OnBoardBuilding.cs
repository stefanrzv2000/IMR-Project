using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnBoardBuilding : OnBoardDestructible
{
    public int Tier;

    public void Repair(int repairAmount)
    {
        Health = Math.Min(Health + repairAmount, MaxHealth);
    }

    public override void UpdateStatus()
    {
        if(PhysicInstance != null)
        {
            PhysicInstance.transform.Find("HealthStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Health);
            if(Tier >= 0) PhysicInstance.transform.Find("TierStatus").gameObject.GetComponent<DragonStatus>().UpdateStatus(Tier + 1);
        }
    }
}