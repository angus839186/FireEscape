using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCamera;

    private float xRotation = 0f;

    public float xSensitivity;
    public float ySensitivity;


    public void playerRotate(Vector2 rotateInput)
    {
        float mouseX = rotateInput.x;
        float mouseY = rotateInput.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
