using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
     public List<CelestialObject> planets;
   


    void Start()
    {
        GetPlanets();


    }

    // Update is called once per frame
    void Update()
    {
        calculateForce();
    }

    private void GetPlanets()
    {
        GameObject[] planetsTemp = GameObject.FindGameObjectsWithTag("Planet");
        for (int i = 0; i < planetsTemp.Length; i++)
        {
            planets.Add(planetsTemp[i].GetComponent<CelestialObject>());
        }


    }


    private void calculateForce()
    {
        foreach(CelestialObject curObject in planets)
        {
            foreach (CelestialObject prevObject in planets)
            {
                if (curObject == prevObject) continue;

                float distance = Vector3.Distance(curObject.transform.position, prevObject.transform.position);
                float force = 0.10f * (curObject.mass * prevObject.mass) / Mathf.Pow(distance, 2);
                Vector3 direction = (prevObject.transform.position - curObject.transform.position);
                curObject.AddForce(direction * force);
            }
        }

        /*for (int i = 0; i < planets.Count; i++)
        {
            for (int j = 0; j < planets.Count; j++)
            {
                if(j!=i)
                {
                    float distance = Vector3.Distance(planets[i].transform.position, planets[j].transform.position);
                    float force = 0.10f * (planets[i].mass * planets[j].mass) / Mathf.Pow(distance, 2);
                    Vector3 direction = (planets[j].transform.position - planets[i].transform.position);
                    planets[i].AddForce(direction*force);
                }

           
            }
         
        }*/

    }


}