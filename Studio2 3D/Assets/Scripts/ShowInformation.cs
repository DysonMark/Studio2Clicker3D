using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInformation : MonoBehaviour
{
    public GameObject informationPanel;
    private void OnMouseOver()
    {
        informationPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        informationPanel.SetActive(false);
    }
}
