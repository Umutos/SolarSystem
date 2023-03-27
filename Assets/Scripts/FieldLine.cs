using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLine : MonoBehaviour
{
    public int pointNumber = 10;
    public bool isActivated = false;

    public LineRenderer lineRenderer;

    List<Vector3> points = new List<Vector3>();

    private SolarSystem solarSystem;

    // Start is called before the first frame update
    void Start()
    {
        solarSystem = FindObjectOfType<SolarSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawFieldLine(float id, Vector3 currentPos, float mass)
    {
        points.Clear();

        Vector3 acc = Vector3.zero;
        Vector3 pos = currentPos;
        points.Add(pos);

        for (int i = 0; i < pointNumber; i++)
        {
            foreach (CelestialObject curObject in solarSystem.planets)
            {
                if (curObject.GetInstanceID() == id) continue;

                float distance = Vector3.Distance(curObject.transform.position, pos);
                float force = 0.10f * (curObject.mass * mass) / Mathf.Pow(distance, 2);
                Vector3 direction = (pos - curObject.transform.position);
                acc += direction * force;
            }

            pos -= acc.normalized;

            points.Add(pos);
        }

        lineRenderer.positionCount = pointNumber + 1;
        lineRenderer.SetPositions(points.ToArray());
    }
}
