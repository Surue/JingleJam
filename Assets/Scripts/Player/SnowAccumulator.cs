using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowAccumulator : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    GroundChecker groundChecker;

    [SerializeField] float speedAccumulation = 1;
    float snowAccumulated = 0;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        groundChecker = GetComponentInChildren<GroundChecker>();
    }

    void Update() {
        if (groundChecker.IsTouchingSnow) {
            spriteRenderer.color = Color.white;

            snowAccumulated += Time.deltaTime * speedAccumulation;
        } else {
            spriteRenderer.color = Color.red;
        }

        transform.localScale = Vector3.one * (1 + snowAccumulated);
    }
}
