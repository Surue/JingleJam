using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, LevelManager.IPausedListener {
    SpriteRenderer spriteRenderer;

    GroundChecker groundChecker;
    HitDetector hitDetector;

    [Header("Snow accumulation")]
    [SerializeField] float snowAccumulationFactor = 1;
    [SerializeField] float maxSnowValue = 10;
    float snowAccumulated = 0;

    [Header("Health")] [SerializeField] float healthReductionOnHit = 0.5f;

    bool isPaused = false;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        hitDetector = GetComponentInChildren<HitDetector>();
        groundChecker = GetComponentInChildren<GroundChecker>();
    }

    void Update() {
        if(isPaused) return;
        
        if (groundChecker.IsTouchingSnow) {
            spriteRenderer.color = Color.white;

            snowAccumulated += Time.deltaTime * snowAccumulationFactor;
        } else {
            spriteRenderer.color = Color.red;
        }

        TestDead();

        transform.localScale = Vector3.one * (1 + snowAccumulated);

        if (snowAccumulated > maxSnowValue) {
            snowAccumulated = maxSnowValue;
        }
    }
    
    

    void TestDead() {
        if (hitDetector.HasHit || groundChecker.HasHit) {
            snowAccumulated -= healthReductionOnHit;
            if (snowAccumulated < 0) {
                snowAccumulated = 0;
            }
            LevelManager.GameManager.PlayerDied();
        }
    }

    public void OnPaused() {
        isPaused = true;
    }

    public void OnUnpaused() {
        isPaused = false;
    }
}
