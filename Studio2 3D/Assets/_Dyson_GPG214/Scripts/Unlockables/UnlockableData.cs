using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Google.MiniJSON;

namespace SAE.GPG214.Dyson.Unlockable
{

    public class UnlockableData : MonoBehaviour
    {
        private static Dictionary<UnlockablePower, bool> unlockedPowers = new Dictionary<UnlockablePower, bool>();

        public void UnlockPowerManager()
        {
            foreach (UnlockablePower power in System.Enum.GetValues(typeof(UnlockablePower)))
            {
                unlockedPowers[power] = false;
            }
        }

        public void UnlockPower(UnlockablePower power)
        {
            unlockedPowers[power] = true;
            SaveUnlockables();
        }

        public enum UnlockablePower
        {
            LevelTwoSword,
            Ultimate,
            Fire
        }

        public static bool IsUnlocked(UnlockablePower power)
        {
            return unlockedPowers[power];
        }

        public static void SaveUnlockables()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "unlockables.json");
            string json = JsonUtility.ToJson(unlockedPowers);
            File.WriteAllText(filePath, json);
        }

        public static void LoadUnlockables()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "unlockables.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Dictionary<UnlockablePower, bool> savedUnlockables =
                    JsonUtility.FromJson<Dictionary<UnlockablePower, bool>>(json);
                if (savedUnlockables != null)
                {
                    unlockedPowers = savedUnlockables;
                }
            }
        }
    }
}
