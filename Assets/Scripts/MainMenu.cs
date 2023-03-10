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
    [SerializeField] private TMP_InputField spawnName;
    [SerializeField] private TMP_InputField spawnPositionX;
    [SerializeField] private TMP_InputField spawnPositionY;
    [SerializeField] private TMP_InputField spawnPositionZ;
    [SerializeField] private TMP_InputField spawnVelocityX;
    [SerializeField] private TMP_InputField spawnVelocityY;
    [SerializeField] private TMP_InputField spawnVelocityZ;
    [SerializeField] private TMP_InputField spawnMass;

    [SerializeField] private CameraOrbite cam;
    [SerializeField] private SolarSystem solarSystem;

    [SerializeField] private GameObject spawnable;
    private CelestialObject target = null;

    static public bool OnPause = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.isOrbiting && !infoPanel.gameObject.activeInHierarchy)
        {
            if (creationPanel.gameObject.activeInHierarchy)
            {
                creationPanel.gameObject.SetActive(false);
            }
            infoPanel.gameObject.SetActive(true);
        }
        else if (!cam.isOrbiting && infoPanel.gameObject.activeInHierarchy)
        {
            infoPanel.gameObject.SetActive(false);
        }

        if (cam.target != null)
        {
            if (target == null || target.gameObject != cam.target.gameObject)
            {
                target = cam.target.parent.gameObject.GetComponent<CelestialObject>();
            }

            planetName.text = "Name : " + target.planetName;
            planetMass.text = "Mass : " + target.mass.ToString();
            planetVelocity.text = "Velocity : " + target.velocity.ToString();
            planetPosition.text = "Position : " + target.transform.position.ToString();
        }
    }

    public void AddCelestialObject()
    {
        if (infoPanel.gameObject.activeInHierarchy)
        {
            infoPanel.gameObject.SetActive(false);
        }

        creationPanel.gameObject.SetActive(true);
    }

    public void CreateCelestialObject()
    {
        CelestialObject parameter = spawnable.GetComponent<CelestialObject>();

        parameter.planetName = spawnName.text;
        parameter.mass = float.Parse(spawnMass.text);

        Vector3 pos = new Vector3(float.Parse(spawnPositionX.text), float.Parse(spawnPositionY.text), float.Parse(spawnPositionZ.text));
        parameter.gameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);

        parameter.velocity.x = float.Parse(spawnVelocityX.text);
        parameter.velocity.y = float.Parse(spawnVelocityY.text);
        parameter.velocity.z = float.Parse(spawnVelocityZ.text);

        solarSystem.planets.Add(parameter);
        Instantiate(spawnable);
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
            OnPause = false;
        }
        else
        {
            startButton_StartTitle.gameObject.SetActive(true);
            startButton_StopTitle.gameObject.SetActive(false);
            OnPause = true;
        }
    }
}
