using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour {
    bool hasHit = false;
    public bool HasHit => hasHit;

    void LateUpdate() {
        hasHit = false;
    }

    public void SetHasHitToFalse() {
        hasHit = false;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            other.gameObject.layer == LayerMask.NameToLayer("Obstacle") ) {
            hasHit = true;
        }
    }
}
