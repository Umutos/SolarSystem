using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text startButton_StartTitle;
    [SerializeField] private TMP_Text startButton_StopTitle;

    [SerializeField] private RectTransform infoPanel;

    [SerializeField] private RectTransform creationPanel;


    bool planetSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (planetSelected)
        {
            infoPanel.gameObject.SetActive(true);
        }
        else
        {
            infoPanel.gameObject.SetActive(false);
        }
    }

    public void AddCelestialObject()
    {
        creationPanel.gameObject.SetActive(true);
    }

    public void CancelCreation()
    {
        creationPanel.gameObject.SetActive(false);
    }

    public void StartSimulation()
    {
        if (startButton_StartTitle.IsActive())
        {
            startButton_StartTitle.gameObject.SetActive(false);
            startButton_StopTitle.gameObject.SetActive(true);
        }
        else
        {
            startButton_StartTitle.gameObject.SetActive(true);
            startButton_StopTitle.gameObject.SetActive(false);
        }
    }
}
