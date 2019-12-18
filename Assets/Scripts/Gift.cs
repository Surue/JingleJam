using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour {
    PlayerController playerController;

    [SerializeField] float height;
    float force;
    
    // Start is called before the first frame update
    void Start() {
        playerController = LevelManager.PlayerController;
    }

    // Update is called once per frame
    void Update() {
        force = Mathf.Sqrt(2 * height * -Physics.gravity.y * playerController.GetComponent<Rigidbody2D>().gravityScale);

    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            
            if(playerController.transform.position.y > transform.position.y + 0.75 )
            playerController.AddJump(force);
        }
    }
}
