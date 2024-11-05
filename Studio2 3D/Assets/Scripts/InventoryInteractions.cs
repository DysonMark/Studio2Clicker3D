using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class InventoryInteractions : MonoBehaviour
{
    [SerializeField] private GameObject canvasCamera;
    //[SerializeField] private GameObject purpleSword;
    //[SerializeField] private GameObject redSword;
    [SerializeField] private GameObject originalSword;
    [SerializeField] private Material purpleMaterial;
    [SerializeField] private Material redMaterial;

    public TextMeshProUGUI coinsText;

    int coins = 1000;

    private int newCoins;
    //[SerializeField] private Material originalMaterial;
    void Start()
    {
     //   coinsText = GetComponent<TextMeshProUGUI>();
        StringToInt();
        //coinsText.text = "Coins : 1000";
        //  purpleSword = GameObject.Find("PurpleSword");
        // redSword = GameObject.Find("RedSword");
        //originalSword = GameObject.Find("OriginalSword");

    }

    // Update is called once per frame
    void Update()
    {
        EnableCanvasCamera();
    }

    void StringToInt()
    {
        //coinsText.text = "1000";
        int.TryParse(coinsText.text, out coins);
        //coinsText.text = string.Format("{1000}", coins);
        Debug.Log("coins: " + coins);
    }
    private void EnableCanvasCamera()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvasCamera.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasCamera.SetActive(false);
        }
    }

    public void PurpleSword()
    {
        originalSword.GetComponent<Renderer>().material = purpleMaterial;
        coins -= 200;
        coinsText.text = coins.ToString();
        
        Debug.Log("value of coins after purple buy: " + coins);
        //originalMaterial = purpleMaterial;
        // purpleSword.transform.position = originalSword.transform.position;
        //Debug.Log("Purple sword");
    }

    public void RedSword()
    {
        originalSword.GetComponent<Renderer>().material = redMaterial;
        //originalMaterial = redMaterial;
    }
}
