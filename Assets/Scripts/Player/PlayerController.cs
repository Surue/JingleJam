using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, LevelManager.IPausedListener {
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    GroundChecker groundChecker;
    HitDetector hitDetector;

    GameManager gameManager;

    [SerializeField] BrokenPlayer prefabBrokenPlayer;

    [Header("Player Settings")]
    [SerializeField] float speed = (110.0f / 60.0f) * 4.0f;
    [SerializeField] float jumpHeight = 2.3f;
    [SerializeField] float gravityScale = 6;

    float initialSpeed;
    
    bool isGrounded = false;
    float jumpForce;

    bool isPaused = false;

    bool hasHit = false;

    public bool HasHit => hasHit;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = gravityScale;

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        groundChecker = GetComponentInChildren<GroundChecker>();
        hitDetector = GetComponentInChildren<HitDetector>();
        
        jumpForce = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y * rigidBody.gravityScale);
        
        LevelManager.Instance.AddPauseListener(this);

        initialSpeed = speed;
    }
    
    void FixedUpdate() {
        if(isPaused) return;
    
        GroundCheck();
        Jump();
        MoveRight();

        if (Input.GetKeyDown(KeyCode.A)) {
            LevelManager.GameManager.Victory();
            
        }
    }

    void LateUpdate() {
        hasHit = false;
    }

    void MoveRight() {
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
    }

    void Jump() {
        if(!isGrounded || !Input.GetButton("Jump")) return;
        
        rigidBody.velocity = new Vector2(speed, jumpForce);
    }

    public void AddJump(float force) {
        rigidBody.velocity = new Vector2(speed, force);
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
    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            hasHit = true;
        }
    }

    public void Die() {
        Instantiate(prefabBrokenPlayer, transform.position, Quaternion.identity);

        spriteRenderer.enabled = false;
        rigidBody.Sleep();

        speed = 0;
    }

    public void Respawn() {
        spriteRenderer.enabled = true;
        rigidBody.WakeUp();

        speed = initialSpeed;
    }
}
