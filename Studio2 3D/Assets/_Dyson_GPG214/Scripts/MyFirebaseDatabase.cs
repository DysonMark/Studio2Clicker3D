using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Google.MiniJSON;

public class MyFirebaseDatabase : MonoBehaviour
{

    public PlayerData myCurrentData;

    public PlayerData dataFromTheServer;

    private DatabaseReference _databaseReference;
    

    public string userID;

    private string saveFilePath;
    
    // Start is called before the first frame update
    void Start()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
       //Firebase.Firestore
        
        myCurrentData = new PlayerData();
        myCurrentData.playerName = "Yulis Kane";
        myCurrentData.numberOfEnemiesKilled = 0;
        myCurrentData.playerPosition = Vector3.zero;

        saveFilePath = Application.streamingAssetsPath + "/PlayerData.json";
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
        string savePlayerData = JsonUtility.ToJson(myCurrentData);
        File.WriteAllText(saveFilePath, savePlayerData);
        Task sendJSon = _databaseReference.Child("users").Child(dataFromTheServer.playerName).Child("PlayerData").SetRawJsonValueAsync(savePlayerData);

        /*while (!sendJSon.IsCompleted && !(sendJSon.IsFaulted || sendJSon.IsCanceled))
        {
            yield return null;
        }*/
        
        
        
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

/*public class User
{
    public string username;
    public string email;

    public User()
    {
        
    }

    public User(string username, string email)
    {
        this.username = email;
        this.email = email;
    }

}*/
