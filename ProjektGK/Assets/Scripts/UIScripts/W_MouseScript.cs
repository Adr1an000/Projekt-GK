//author: Dawid Musialik

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_MouseScript : MonoBehaviour
{
    public float mouseSensivity = 3f;
    public Transform playerBody;
    public bool allowRotation = true;

    float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!allowRotation) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
    }
}
