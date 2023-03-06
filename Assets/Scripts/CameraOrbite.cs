using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbite : MonoBehaviour
{
    public float sensitivity = 2f;
    public float zoomSpeed = 2f;
    public float speed = 20.0f;

    private float speedH = 2.0f;
    private float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float dist = 10f;
    private float longitude = 0f;
    private float latitude = 0f;

    public float distance = -50f;

    private Transform target;
    private Transform previousTarget;
    private bool isOrbiting = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                target = hit.transform;
                isOrbiting = true;
                longitude = transform.eulerAngles.y;
                latitude = transform.eulerAngles.x;
                dist = distance;
            }
            else
            {
                isOrbiting = false;
            }
        }
        else if (Input.GetMouseButtonUp(1) && target != null)
        {
            target = null;
            isOrbiting = false;
        }

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            transform.position += transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed + transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

            if (Input.GetKeyDown(KeyCode.LeftShift))
                speed = 50.0f;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                speed = 20.0f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void LateUpdate()
    {
        if (isOrbiting)
        {
            if (target == null)
                return;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            dist -= scroll * zoomSpeed;

            latitude += Input.GetAxis("Vertical") * sensitivity / 4;
            longitude -= Input.GetAxis("Horizontal") * sensitivity / 4;

            latitude = Mathf.Clamp(latitude, -90f, 90f);

            Quaternion rotation = Quaternion.Euler(latitude, longitude, 0f);
            Vector3 position = rotation * new Vector3(0f, 0f, -dist) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}

