using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 720.0f; // 每秒旋转的角度
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 获取输入
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveZ = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveZ = -1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
        }

        // 移动方向
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        if (move != Vector3.zero)
        {
            // 计算目标旋转
            Quaternion targetRotation = Quaternion.LookRotation(move);
            // 平滑旋转到目标方向
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 计算角色的前进方向
            Vector3 forwardMove = transform.forward * speed * Time.deltaTime;
            controller.Move(forwardMove);
            animator.SetBool("isMoving", true);
        }
        else{
             animator.SetBool("isMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("isAttack");
        }
        else{
            animator.SetTrigger("unAttack");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("isTrick");
        }
        else{
            animator.SetTrigger("unTrick");
        }
    }
}
