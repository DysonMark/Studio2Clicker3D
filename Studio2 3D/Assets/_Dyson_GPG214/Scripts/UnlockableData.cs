using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class UnlockableData
{
    public string unlockableFolder;
    public string unlockableID;
    public bool isUnlocked;
    public bool hasPreviouslyBeenDisplayed;

    public void Unlock()
    {
        isUnlocked = true;
    }

    public void UpdateHasBeenDisplayed()
    {
        hasPreviouslyBeenDisplayed = true;
    }

    public IEnumerator LoadData(DatabaseReference databaseReference, string userId)
    {
        yield return null;
    }
}
