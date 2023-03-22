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
    [SerializeField] private TMP_Text planetName;
    [SerializeField] private TMP_Text planetMass;
    [SerializeField] private TMP_Text planetVelocity;
    [SerializeField] private TMP_Text planetPosition;

    [SerializeField] private RectTransform creationPanel;
    [SerializeField] private RectTransform generalInfoPanel;

    [SerializeField] private CameraOrbite cam;
    private CelestialObject target = null;

    static public bool OnPause = true;
    private bool InCreation = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.isOrbiting && !InCreation)
        {
            if (!infoPanel.gameObject.activeInHierarchy)
            {
                infoPanel.gameObject.SetActive(true);
            }

            if (target == null || target.gameObject != cam.target.gameObject)
            {
                target = cam.target.parent.gameObject.GetComponent<CelestialObject>();
            }

            planetName.text = "Name : " + target.planetName;
            planetMass.text = "Mass : " + target.mass.ToString();
            planetVelocity.text = "Velocity : " + target.velocity.ToString();
            planetPosition.text = "Position : " + target.transform.position.ToString();
        }
        else if (!cam.isOrbiting && infoPanel.gameObject.activeInHierarchy)
        {
            infoPanel.gameObject.SetActive(false);
        }
    }

    public void AddCelestialObject()
    {
        if (infoPanel.gameObject.activeInHierarchy)
        {
            infoPanel.gameObject.SetActive(false);   
        }
        generalInfoPanel.gameObject.SetActive(false);
        creationPanel.gameObject.SetActive(true);
        InCreation = true;
    }

    public void CancelCreation()
    {
        creationPanel.gameObject.SetActive(false);
        generalInfoPanel.gameObject.SetActive(true);
        InCreation = false;
    }

    public void StartSimulation()
    {
        if (startButton_StartTitle.IsActive())
        {
            startButton_StartTitle.gameObject.SetActive(false);
            startButton_StopTitle.gameObject.SetActive(true);
            OnPause = false;
        }
        else
        {
            startButton_StartTitle.gameObject.SetActive(true);
            startButton_StopTitle.gameObject.SetActive(false);
            OnPause = true;
        }
    }

    public void TargetFieldManagement(bool isOn)
    {
        target.vectorFieldOn = isOn;
    }
}
