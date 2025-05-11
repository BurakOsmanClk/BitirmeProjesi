using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f; //hassasiyet

    float xRotation = 0f;
    float yRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Imleci ortaya kilitler
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; //yukari asagi dondurme

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //bakis acisini kisitlar

        yRotation += mouseX; // saga sola dondurme

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f); //birlestirme
    }
}
