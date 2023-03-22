using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorPoint : MonoBehaviour
{
    
    public Vector3 vectorCoordinnate ;
    public float test;
    public float force;
    public float dist = 9999;
    public LineRenderer arrowTip;
    private LineRenderer vectorDir;
    public VectorField vectorField;
    public float maxRadius = 0;
    private bool tooFar;
    void Start()
    {
       
        arrowTip = transform.GetChild(0).GetComponent<LineRenderer>();
        vectorDir = GetComponent<LineRenderer>();

    }
    void FixedUpdate()
    {
        calculatevectorDir();
        MoveVectorArrow();
    }

    void MoveVectorArrow()
    {
        vectorDir.SetPosition(1, transform.localPosition);
        test = Vector3.Distance(transform.position, transform.localPosition + vectorCoordinnate);
        if (test < 8)
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


    private void calculatevectorDir()
    {
        vectorCoordinnate = new Vector3(0, 0, 0);
        dist = 9999;
        foreach (CelestialObject planet in vectorField.solarSystem.planets)
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

        if (dist != 9999 && dist > vectorField.step * vectorField.range && vectorField.step !=0) 
        {
            GameObject[] vectorField;
            vectorField = GameObject.FindGameObjectsWithTag("VectorField");
            if (vectorField.Length != 0)
            { 
                vectorField[0].GetComponent<VectorField>().RecreateVectorField();
            }

        }
    }


}

