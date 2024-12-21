using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Firestore;
using Google.MiniJSON;
using SAE.GPG214.Dyson.Data;

namespace SAE.GPG214.Dyson.Firebase
{
    public class MyFirebaseDatabase : MonoBehaviour
    {

        public PlayerData myCurrentData;

        public PlayerData dataFromTheServer;

        private DatabaseReference _databaseReference;

        public string userID;

        private string saveFilePath;

        private FirebaseFirestore db;

        private string savePlayerData;
        private Task<QuerySnapshot> _storageSnapshot;

        // Start is called before the first frame update
        void Start()
        {
            myCurrentData = new PlayerData();
            myCurrentData.playerName = "Yulis Kane";
            myCurrentData.numberOfEnemiesKilled = 0;
            myCurrentData.playerPosition = Vector3.zero;
            saveFilePath = Application.streamingAssetsPath + "/PlayerData.json";

            //Firestore

            db = FirebaseFirestore.DefaultInstance;
            _storageSnapshot = FirebaseFirestore.DefaultInstance.Collection("users").GetSnapshotAsync();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveGame();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
            }
        }

        public void SaveGame()
        {
            string jsonData = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "PlayerData.json"));
            Dictionary<string, object> savePlayerData =
                JsonUtility.FromJson<Dictionary<string, object>>(jsonData);
            //File.WriteAllText(saveFilePath, savePlayerData);
            db.Collection("users").AddAsync(savePlayerData).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("The json file has been sent");
                }
                else
                {
                    Debug.LogError("Failed to send json file");
                }
            });


            Debug.Log("Game has been saved");
        }

        public void LoadGame()
        {
            if (File.Exists(saveFilePath))
            {
                string loadPlayerData = File.ReadAllText(saveFilePath);
                myCurrentData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
                Debug.Log("Game has been loaded");
            }
        }
    }
}

