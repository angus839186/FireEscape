using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攝像機的旋轉
//角色左右旋轉控制實現左右移動
//攝像機上下旋轉控制視線上下移動

public class MouseLook : MonoBehaviour
{
    [Tooltip("視野靈敏度")] public float mouseSenstivity = 400f;
    private Transform playerBody; //玩家位置
    private float yRotation = 0f; //攝像機上下旋轉的數值

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.GetComponentInParent<PlayerController1>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime; //左右
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime; //上下

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -60f, 60f); //限制上下旋轉的範圍
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f); //攝像機上下旋轉
        playerBody.Rotate(Vector3.up * mouseX); //角色左右移動
    }
}
