﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {
    bool isGrounded = false;
    bool isTouchingSnow = false;

    [SerializeField] ParticleSystem particleSystem;
    Rigidbody2D body;
    
    public bool IsGrounded => isGrounded;
    public bool IsTouchingSnow => isTouchingSnow;

    bool isFalling = false;

    void Start() {
        body = transform.parent.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        isFalling = body.velocity.y < -1;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground") &&
            other.gameObject.layer != LayerMask.NameToLayer("Obstacle")) return;
        
        if (isFalling) {
            particleSystem.Play();
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
            isTouchingSnow = true;
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground") ||
            other.gameObject.layer != LayerMask.NameToLayer("Obstacle")) {
            isGrounded = false;
            isTouchingSnow = false;
        }
    }
}