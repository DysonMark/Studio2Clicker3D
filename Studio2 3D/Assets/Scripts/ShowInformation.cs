using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInformation : MonoBehaviour
{
    #region Variables
    
    public GameObject redInformationPanel;
    public GameObject blueInformationPanel;
    public GameObject yellowInformationPanel;
    #endregion

    private void OnMouseOver()
    {
        if (this.GameObject().name == "potion_red")
        {
            redInformationPanel.SetActive(true);
            blueInformationPanel.SetActive(false);
            yellowInformationPanel.SetActive(false);
        }
        if (this.GameObject().name == "potion_blue")
        {
            blueInformationPanel.SetActive(true);
            redInformationPanel.SetActive(false);
            yellowInformationPanel.SetActive(false);
        }
        if (this.GameObject().name == "potion_yellow")
        {
            yellowInformationPanel.SetActive(true);
            blueInformationPanel.SetActive(false);
            redInformationPanel.SetActive(false);
        }
    }

    private void OnMouseExit()
    { 
        redInformationPanel.SetActive(false);
        blueInformationPanel.SetActive(false);
        yellowInformationPanel.SetActive(false);
    }
}
