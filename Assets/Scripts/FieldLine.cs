using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLine : MonoBehaviour
{
    public CelestialObject currentObject;
    public SolarSystem solarSystem;
    public int pointNumber = 10;

    Vector3 acc;
    Vector3 vel;
    Vector3 pos;
    List<Vector3> points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateLine()
    {
        pos = currentObject.transform.position;
        vel = currentObject.velocity;
        acc = Vector3.zero;

        for (int i = 0; i < pointNumber; i++)
        {
            foreach (CelestialObject curObject in solarSystem.planets)
            {
                if (curObject.GetInstanceID() == currentObject.GetInstanceID()) continue;

                float distance = Vector3.Distance(curObject.transform.position, pos);
                float force = 0.10f * (curObject.mass * currentObject.mass) / Mathf.Pow(distance, 2);
                Vector3 direction = (pos - curObject.transform.position);
                acc += direction * force;
            }
        }
    }
}
