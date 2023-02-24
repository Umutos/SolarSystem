using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    public float speed = 20.0f;

    private float speedH = 2.0f;
    private float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            transform.position += transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed + transform.forward *Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

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
}
