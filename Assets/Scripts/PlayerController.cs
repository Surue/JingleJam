using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] Rigidbody2D rigidBody;
    
    [Header("Player Settings")]
    [SerializeField] float speed = 1;

    [Header("Jump Settings")]
    [SerializeField] float jumpHeight = 1;
    bool isJumping = false;

    float bestHeight = 0;
    float jumpForce;
    
    

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
        
        MoveRight();

        Debug.Log(transform.position.y);
        if (bestHeight < transform.position.y)
            bestHeight = transform.position.y;

        if (Input.GetKeyDown("c"))
            PrintBestHeight();
    }

    void MoveRight() {
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
    }

    void Jump() {
        //if(isJumping) return;
        
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y * rigidBody.gravityScale));

        isJumping = true;
    }

    void PrintBestHeight() {
        Debug.Log("Best height : " + bestHeight);
    }
}
