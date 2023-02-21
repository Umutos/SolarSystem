using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{


     public float mass;
    public Vector3 velocity;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime+(velocity* Time.deltaTime * Time.deltaTime)/2.0f);
    }

   public void AddForce(Vector3 forceToAdd)
    {

        velocity += forceToAdd * Time.deltaTime / mass;
    }
}
