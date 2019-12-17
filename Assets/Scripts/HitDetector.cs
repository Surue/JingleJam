using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour {
    public bool hasHit = false;

    void SetHasHitToFalse() {
        hasHit = false;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            hasHit = true;
        }
    }
}
