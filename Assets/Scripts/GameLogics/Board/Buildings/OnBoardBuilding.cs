using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnBoardBuilding : OnBoardDestructible
{
    public void Repair(int repairAmount)
    {
        Health = Math.Min(Health + repairAmount, MaxHealth);
    }
}
