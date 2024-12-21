using UnityEngine;

namespace SAE.GPG214.Dyson.Data
{
    [System.Serializable]
    public struct PlayerData
    {
        public string playerName;
        public int numberOfEnemiesKilled;
        public Vector3 playerPosition;
    }
}
