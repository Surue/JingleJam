using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] Rigidbody2D rigidBody;
    
    [Header("Player Settings")]
    [SerializeField] float speed = 110.0f / 60.0f;
    [SerializeField] float jumpHeight = 1;
    bool isJumping = false;

    float jumpForce;
    
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpForce = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y * rigidBody.gravityScale);
    }
    
    void Update() {
        Jump();
        MoveRight();
    }

    void MoveRight() {
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
    }

    void Jump() {
        if(isJumping || !Input.GetButtonDown("Jump")) return;
        
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

        isJumping = true;
    }

    void GrounCheck() {
        if(!isJumping) return;
    }
}
