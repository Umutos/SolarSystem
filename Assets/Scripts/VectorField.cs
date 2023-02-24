using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorField : MonoBehaviour
{
    public int range = 32;
    public int step = 1;
    public GameObject vectorPoint;
    void Start()
    {

        SpawnVectorPoint();
    }

   
    void Update()
    {
        
    }

    private void SpawnVectorPoint()
    {
        for(int i = -range/2; i< range / 2; i++)
        {
            for (int j = -range / 2; j < range / 2; j ++)
            {
                for (int k = -range / 2; k < range / 2; k ++)
                {
                    Instantiate(vectorPoint, new Vector3(transform.position.x+(i*step), transform.position.y+ (j * step), transform.position.z+ (k * step)), Quaternion.identity);
                }
            }
        }
       
    }


}
