using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorPoint : MonoBehaviour
{
    
     public Vector3 vectorCoordinnate ;
    private LineRenderer vectorDir;
    public List<CelestialObject> planets;
    public float test;
    public float force;
    private bool tooFar;
    void Start()
    {
        vectorDir = GetComponent<LineRenderer>();
        StartCoroutine(LoadPlanet());
       
       
    }

    // Update is called once per frame
    void Update()
    {
        vectorCoordinnate = new Vector3(0, 0, 0);
      
            calculatevectorDir();
            vectorDir.SetPosition(1, transform.localPosition);
            test = Vector3.Distance(transform.position, transform.localPosition + vectorCoordinnate);
            if (test < 4)
            {
                vectorDir.SetPosition(0, transform.localPosition + vectorCoordinnate);
            }
            else
            {
                vectorDir.SetPosition(0, transform.localPosition);
            }
        
      
        
    }

    private void calculatevectorDir()
    {
         foreach (CelestialObject planet in planets)
            {
                

                float distance = Vector3.Distance(transform.position, planet.transform.position);
                force = 0.050f * (planet.mass) / Mathf.Pow(distance, 2);
                Vector3 direction = (planet.transform.position - transform.position);
                
                    vectorCoordinnate +=direction* force ;
            }
       

    }

    private IEnumerator LoadPlanet()
    {
        yield return new WaitForSeconds(1);
        GameObject[] planetsTemp = GameObject.FindGameObjectsWithTag("Planet");
        for (int i = 0; i < planetsTemp.Length; i++)
        {
            planets.Add(planetsTemp[i].GetComponent<CelestialObject>());
        }
    }


}

