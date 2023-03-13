using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CreationMenu : MonoBehaviour
{

    [SerializeField] private TMP_InputField spawnName;
    [SerializeField] private TMP_InputField spawnPositionX;
    [SerializeField] private TMP_InputField spawnPositionY;
    [SerializeField] private TMP_InputField spawnPositionZ;
    [SerializeField] private TMP_InputField spawnVelocityX;
    [SerializeField] private TMP_InputField spawnVelocityY;
    [SerializeField] private TMP_InputField spawnVelocityZ;
    [SerializeField] private TMP_InputField spawnMass;

    [SerializeField] private GameObject spawnable;
    [SerializeField] private SolarSystem solarSystem;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}