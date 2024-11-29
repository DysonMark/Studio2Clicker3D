using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInteractions : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject canvasCamera;
    [SerializeField] private GameObject originalSword;
    [SerializeField] private Material purpleMaterial;
    [SerializeField] private Material redMaterial;
    public TextMeshProUGUI coinsText;

    int coins = 1000;
    private int click = 0;
    private int otherClick = 0;
    private int newCoins;

    public int maximum;

    public int current;

    public Image mask;
    
    #endregion
    void Start()
    {
        StringToInt();
    }

    // Update is called once per frame
    void Update()
    {
        EnableCanvasCamera();
    }

    void StringToInt()
    {
        coinsText.text = "1000";
        int.TryParse(coinsText.text, out coins);
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
        click += 1;
        originalSword.GetComponent<Renderer>().material = purpleMaterial;
        if (click == 1)
            coins -= 200;
        else
        {
         Debug.Log("Sword already equipped");   
        }

        coinsText.text = coins.ToString();
    }

    public void RedSword()
    {
        otherClick += 1;
        originalSword.GetComponent<Renderer>().material = redMaterial;
        if (otherClick == 1)
            coins -= 200;
        else
        {
            Debug.Log("Sword already equipped");
        }
        coinsText.text = coins.ToString();
    }

    public void FillGauge()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = 1;
    }   
}
