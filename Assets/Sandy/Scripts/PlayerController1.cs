using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//角色控制器

public class PlayerController1 : MonoBehaviour
{
    private CharacterController characterController;
    public Vector3 moveDirction;  //設置角色移動方向

    [Header("玩家數值")]
    private float Speed;
    [Tooltip("行走速度")] public float walkSpeed;
    [Tooltip("奔跑速度")] public float runSpeed;
    [Tooltip("蹲下奔跑速度")] public float crouchSpeed;


    [Header("按鍵設置")]
    [Tooltip("奔跑按鍵")] public KeyCode runInputName = KeyCode.LeftShift;
    [Tooltip("跳躍按鍵")] public KeyCode jumpInputName = KeyCode.Space;
    [Tooltip("下蹲按鍵")] public KeyCode crouchInputName = KeyCode.LeftControl;

    [Header("玩家動作判斷")]
    public MovementState state;
    public bool isWalk;
    public bool isRun;
    //public bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        walkSpeed = 5f;
        runSpeed = 6f;
        crouchSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    //角色移動
    public void Moving()
    {
        /*這裡如果想讓移動急停就用GetAxisRaw
          不需要急停可以用GetAxis*/
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        isRun = Input.GetKey(runInputName);
        isWalk = (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0) ? true : false;
        //玩家移動的水平軸跟縱軸都大於0就是行走
        if (isRun)
        {
            state = MovementState.running;
            Speed = runSpeed;
        }
        else
        {
            state = MovementState.walking;
            Speed = walkSpeed;
        }


        //設置角色移動方向(防止斜向速度變快)
        moveDirction = (transform.right * h + transform.forward * v).normalized;
        characterController.Move(moveDirction * Speed * Time.deltaTime);
    }

    public enum MovementState
    {
        walking,
        running,
        crouching,
        idle

    }
}
