using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {
    bool isGrounded = false;

    public bool IsGrounded => isGrounded;

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = false;
        }
    }
}