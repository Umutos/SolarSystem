using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{

    public float mass;
    public Vector3 velocity;
    public string planetName;
         

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!MainMenu.OnPause)
            transform.Translate(velocity * Time.deltaTime+(velocity* Time.deltaTime * Time.deltaTime)/2.0f);
    }

   public void AddForce(Vector3 forceToAdd)
    {

        velocity += forceToAdd * Time.deltaTime / mass;
    }
}
