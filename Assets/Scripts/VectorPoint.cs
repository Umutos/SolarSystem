using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorPoint : MonoBehaviour
{
    
    public Vector3 vectorCoordinnate ;
    public List<CelestialObject> planets;
    public float arrowLength;
    public float force;
    public float dist = 9999;
    public int range ;
    public int step;
    public LineRenderer arrowTip;
    private LineRenderer vectorDir;
    void Start()
    {
       
        arrowTip = transform.GetChild(0).GetComponent<LineRenderer>();
        vectorDir = GetComponent<LineRenderer>();
      
        StartCoroutine(LoadPlanet());
        GameObject[] vectorField;
        vectorField = GameObject.FindGameObjectsWithTag("VectorField");
        if (vectorField.Length != 0)
        {
            range = vectorField[0].GetComponent<VectorField>().range;
            step = vectorField[0].GetComponent<VectorField>().step;
        }

    }

    void MoveVectorArrow()
    {
        vectorCoordinnate = new Vector3(0, 0, 0);
        vectorDir.SetPosition(1, transform.localPosition);
        arrowLength = Vector3.Distance(transform.position, transform.localPosition + vectorCoordinnate);
        //if arroww too long dont draw it
        if (arrowLength < 10)
        {
            arrowTip.SetPosition(0, transform.localPosition + vectorCoordinnate);
            vectorDir.SetPosition(0, transform.localPosition + vectorCoordinnate);
            arrowTip.SetPosition(1, transform.localPosition + vectorCoordinnate + vectorCoordinnate / 10);
        }
        else
        {
            arrowTip.SetPosition(0, transform.position);
            arrowTip.SetPosition(1, transform.position);
            vectorDir.SetPosition(0, transform.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        calculatevectorDir();
        MoveVectorArrow();
    }

    private void calculatevectorDir()
    {
         dist = 9999;
        foreach (CelestialObject planet in planets)
        {
            float distance = Vector3.Distance(transform.position, planet.transform.position);
            if (dist > distance)
            {
                dist = distance;
            }
            force = 0.050f * (planet.mass) / Mathf.Pow(distance, 2);
            Vector3 direction = (planet.transform.position - transform.position);

            vectorCoordinnate += direction * force;
        }

        if (dist != 9999 && dist > step * range && step!=0) 
        {
            GameObject[] vectorField;
            vectorField = GameObject.FindGameObjectsWithTag("VectorField");
            if (vectorField.Length != 0)
            { 
                vectorField[0].GetComponent<VectorField>().RecreateVectorField();
            }

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

