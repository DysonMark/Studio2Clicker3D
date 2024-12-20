using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour
{
    public UnlockableData unlock;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            unlock.UnlockPower(UnlockableData.UnlockablePower.LevelTwoSword);
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            unlock.UnlockPower(UnlockableData.UnlockablePower.Fire);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            unlock.UnlockPower(UnlockableData.UnlockablePower.Ultimate);
        }
    }
}
