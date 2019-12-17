using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {
    public bool isGrounded = false;
    LayerMask groundLayer;

    public void SetGroundLayer(LayerMask layer) {
        groundLayer = layer;
        Debug.Log("I AM SET : " + layer);
    }
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == groundLayer) {
            isGrounded = true;
            Debug.Log("I AM ON THE GROUND FUCKERS!!");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == groundLayer) {
            isGrounded = false;
            Debug.Log("I AM GOING TO GO TO HEAVEN");
        }
    }
}