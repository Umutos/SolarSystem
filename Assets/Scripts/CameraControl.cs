using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public float sensitivity = 2f;
    public float zoomSpeed = 2f;
    public float speed = 20.0f;

    private float speedH = 2.0f;
    private float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float longitude = 0f;
    private float latitude = 0f;
    private float firstDistance = 0f;
    public  float distance = -50f;

    private GraphicRaycaster m_Raycaster;
    bool firstClick;

    [HideInInspector] public Transform target;
    [HideInInspector] public bool isOrbiting = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        m_Raycaster = GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        StartCam();
        GodCam(); 
    }

    void LateUpdate()
    {
        CameraOrbite();
    }

    private void StartCam()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                target        = hit.transform;
                isOrbiting    = true;
                longitude     = transform.eulerAngles.y;
                latitude      = transform.eulerAngles.x;
                firstDistance = distance;
            }
            else
                isOrbiting    = false;
        }

        Vector3 movement = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        transform.position += movement * Time.deltaTime * speed;
    }

    private void GodCam()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            yaw   += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            if (Input.GetKeyDown(KeyCode.LeftShift))
                speed = 50.0f;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                speed = 20.0f;
        }

        if (Input.GetMouseButtonUp(1))
            Cursor.lockState = CursorLockMode.None;
    }

    private void CameraOrbite()
    {
        if (isOrbiting)
        {
            if (target == null)
                return;

            float scroll   = Input.GetAxis("Mouse ScrollWheel");
            firstDistance -= scroll * zoomSpeed;

            latitude  += Input.GetAxis("Vertical") * sensitivity / 4;
            longitude -= Input.GetAxis("Horizontal") * sensitivity / 4;

            latitude   = Mathf.Clamp(latitude, -90f, 90f);

            Quaternion rotation = Quaternion.Euler(latitude, longitude, 0f);
            Vector3 position    = rotation * new Vector3(0f, 0f, -firstDistance) + target.position;

            transform.rotation  = rotation;
            transform.position  = position;

            yaw   = transform.eulerAngles.y;
            pitch = transform.eulerAngles.x;
        }
    }
}

