using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorPoint : MonoBehaviour
{
    
    public Vector3 vectorCoordinnate ;
    public float dist;
    public float force;

    public LineRenderer arrowTip;
    private LineRenderer vectorDir;
    public VectorField vectorField;
    private Renderer rendererComponent;
    float VectorLength = 1;
    public float maxRadius = 0;
    void Start()
    {
       
        arrowTip = transform.GetChild(0).GetComponent<LineRenderer>();
        vectorDir = GetComponent<LineRenderer>();
        rendererComponent = GetComponent<Renderer>();

        calculatevectorDir();
        MoveVectorArrow();

    }
    void FixedUpdate()
    {
        
        if (rendererComponent.isVisible)
        {
            dist = Vector3.Distance(vectorField.mainCamera.transform.position, transform.position);


            if (dist<=50)
            calculatevectorDir();
            MoveVectorArrow();
        }
    }

    void MoveVectorArrow()
    {
        vectorCoordinnate.Normalize();
        vectorCoordinnate *= VectorLength;
        vectorDir.SetPosition(1, transform.localPosition);
        arrowTip.SetPosition(0, transform.localPosition + vectorCoordinnate);
        vectorDir.SetPosition(0, transform.localPosition + vectorCoordinnate);
        arrowTip.SetPosition(1, transform.localPosition + vectorCoordinnate + vectorCoordinnate / 10);
        
       
    }

    // Update is called once per frame


    private void calculatevectorDir()
    {
        vectorCoordinnate = new Vector3(0, 0, 0);
        
        foreach (CelestialObject planet in vectorField.solarSystem.planets)
        {
            if (planet.vectorFieldOn)
            {
                float distance = Vector3.Distance(transform.position, planet.transform.position);
           
                force = 0.050f * (planet.mass) / Mathf.Pow(distance, 2);
              
                Vector3 direction = (planet.transform.position - transform.position);

                vectorCoordinnate += direction * force;
            }
        }

    }


}

