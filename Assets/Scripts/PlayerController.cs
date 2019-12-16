using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float speed = 1;

    [Header("Jump Settings")]
    [SerializeField] AnimationCurve curve;
    [SerializeField] float height = 1;
    [SerializeField] float duration = 1;
    [SerializeField] float fallSpeed = 1;

    float jumpBeginTime;
    float jumpBeginYPosition;
    bool isJumping = false;

    void Update() {
        bool jump = Input.GetButtonDown("Jump");
        
        if (jump) {
            StartJump();
        }
    }

    void FixedUpdate() {
        MoveRight();
        DoJump();
    }

    void MoveRight() {
        transform.position = new Vector2(
            Mathf.Lerp(
                transform.position.x,
                transform.position.x + speed,
                Time.fixedDeltaTime),
            transform.position.y);
    }

    void StartJump() {
        if(isJumping) return;
        
        jumpBeginTime = Time.time;
        jumpBeginYPosition = transform.position.y;
        
        isJumping = true;
    }

    void DoJump() {
        if(!isJumping) return;
        
        transform.position = new Vector2(transform.position.x, jumpBeginYPosition + curve.Evaluate((Time.fixedTime - jumpBeginTime) * (1 / duration)));

        if (0 == curve.Evaluate((Time.fixedTime - jumpBeginTime) * (1 / duration))) {
            isJumping = false;
        }
    }
}
