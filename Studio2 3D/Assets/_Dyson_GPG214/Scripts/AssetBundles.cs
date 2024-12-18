using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class AssetBundles : MonoBehaviour
{
    private string folderPath = "AssetBundles";

    private string fileName = "gpg214";

    private string combinedPath;

    private AssetBundle gpgBundle;

    public SpriteRenderer playerSprite;

    private GameObject playerInstance;
    // Start is called before the first frame update
    void Start()
    {
        LoadAssetBundle();
        LoadCube();
    }

    void LoadCube()
    {
        if (gpgBundle == null)
        {
            return;
        }

        GameObject dog = GameObject.Find("Dog");
        dog = gpgBundle.LoadAsset<GameObject>("Dog");
        if (dog != null)
        {
            dog.transform.position = new Vector3(-16, 5, 0);
            playerInstance = Instantiate(dog);
            Debug.Log("instantiate this");            
        }
    }

    void LoadPlayerSprite()
    {
        if (gpgBundle == null)
        {
            return;
        }
        
        Texture2D doggo = gpgBundle.LoadAsset<Texture2D>("dog");

        if (playerInstance != null)
        {
            playerInstance.GetComponent<MeshRenderer>().material.mainTexture = doggo;
        }
        if (doggo != null)
        {
            playerSprite.sprite = Sprite.Create(doggo, new Rect(0, 0, doggo.width, doggo.height), Vector2.zero);
        }
    }
    private void LoadAssetBundle()
    {
        combinedPath = Path.Combine(Application.streamingAssetsPath, folderPath, fileName);

        if (File.Exists(combinedPath))
        {
            gpgBundle = AssetBundle.LoadFromFile(combinedPath);
            Debug.Log("Asset Bundle Loaded");
        }
        else
        {
            Debug.Log("File does not exists: " + combinedPath);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
