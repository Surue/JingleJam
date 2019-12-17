using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D rigidBody;
    GroundChecker groundChecker;

    [Header("Player Settings")]
    [SerializeField] float speed = 110.0f / 60.0f;
    [SerializeField] float jumpHeight = 1;
    bool isGrounded = false;
    float jumpForce;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        jumpForce = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y * rigidBody.gravityScale);
    }
    
    void Update() {
        GroundCheck();
        Jump();
        MoveRight();
    }

    void MoveRight() {
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
    }

    void Jump() {
        if(!isGrounded || !Input.GetButtonDown("Jump")) return;
        
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    void GroundCheck() {
        isGrounded = groundChecker.IsGrounded;
    }
}
