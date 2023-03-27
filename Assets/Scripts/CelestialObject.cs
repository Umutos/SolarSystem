using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{

    public float mass;
    public string planetName;
    public bool vectorFieldOn = true;
    public bool fieldLineOn = false;

    public Vector3 velocity;
    public Vector3 pos;
    private Vector3 acc;
    private Vector3 new_acc;

    [SerializeField] private FieldLine fieldLine;

    void Start()
    {
        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!MainMenu.OnPause)
        {
            Vector3 new_pos = pos + velocity * Time.fixedDeltaTime + acc * (Time.fixedDeltaTime * Time.fixedDeltaTime * 0.5f);
            Vector3 new_vel = velocity + (acc + new_acc) * (Time.fixedDeltaTime * 0.5f);
            pos = new_pos;
            velocity = new_vel;
            acc = new_acc;
            new_acc = Vector3.zero;

            transform.SetPositionAndRotation(pos, Quaternion.identity);
        }

        if (fieldLineOn)
        {
            fieldLine.lineRenderer.enabled = true;
            fieldLine.DrawFieldLine(GetInstanceID(), pos, mass);
        }
        else
        {
            fieldLine.lineRenderer.enabled = false;
        }
    }

   public void AddForce(Vector3 forceToAdd)
   {
        new_acc += forceToAdd * Time.fixedDeltaTime / mass;
   }
}
