using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbite : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
    public float sensitivity = 2f;
    public float zoomSpeed = 2f;

    private float longitude = 0f;
    private float latitude = 0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for CameraOrbit script!");
            return;
        }

        Vector3 direction = transform.position - target.position;
        longitude = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
        latitude = Mathf.Rad2Deg * Mathf.Asin(direction.y / direction.magnitude);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                target = hit.transform;
                //distance = Vector3.Distance(transform.position, target.position);
            }
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;

        latitude += Input.GetAxis("Vertical") * sensitivity / 4;
        longitude -= Input.GetAxis("Horizontal") * sensitivity / 4;

        latitude = Mathf.Clamp(latitude, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(latitude, longitude, 0f);
        Vector3 position = rotation * new Vector3(0f, 0f, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}

