using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, LevelManager.IPausedListener {
    Rigidbody2D rigidBody;
    GroundChecker groundChecker;
    HitDetector hitDetector;

    [Header("Player Settings")]
    [SerializeField] float speed = (110.0f / 60.0f) * 2.0f;
    [SerializeField] float jumpHeight = 3;

    [SerializeField] float gravityScale = 6;
    bool isGrounded = false;
    float jumpForce;

    bool isPaused = false;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = gravityScale;
        groundChecker = GetComponentInChildren<GroundChecker>();
        hitDetector = GetComponent<HitDetector>();
        jumpForce = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y * rigidBody.gravityScale);
        Debug.Log("Jump force : " + jumpForce);
        
        LevelManager.Instance.AddPauseListener(this);
    }
    
    void Update() {
        if(isPaused) return;
    
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

    public void OnPaused() {
        isPaused = true;
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 0;
    }

    public void OnUnpaused() {
        isPaused = false;
        rigidBody.gravityScale = gravityScale;
    }
}
